using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.TerminalManagement;
using HPCL.Common.Models.ResponseModel.TerminalManagementResponse;
using HPCL.Common.Models.ViewModel;
using HPCL.Common.Models.ViewModel.Terminal;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
   public class TerminalManagementService :ITerminalManagement
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private bool flag;
        private readonly ICommonActionService _commonActionService;
        public TerminalManagementService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }

        public async Task<Tuple<List<ObjMerchantDetail>, List<ObjTerminalDetail>>> TerminalInstallationRequest(TerminalManagement entity)
        {
            var searchBody = new TerminalManagement();

            if (entity.MerchantId != null)
            {
                searchBody = new TerminalManagement
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    MerchantId = entity.MerchantId,
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Merchant")
            {
                searchBody = new TerminalManagement
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    MerchantId = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.TerminalInstallationRequestUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var searchRes = obj["Data"].Value<JObject>();
            var ObjMerchant = searchRes["ObjMerchantDetail"].Value<JArray>();
            var ObjTerminal = searchRes["ObjTerminalDetail"].Value<JArray>();

            List<ObjMerchantDetail> objMerchantList = ObjMerchant.ToObject<List<ObjMerchantDetail>>();
            List<ObjTerminalDetail> searchList = ObjTerminal.ToObject<List<ObjTerminalDetail>>();
            return Tuple.Create(objMerchantList,searchList);
        }

        public async Task<string> AddJustification(string objInsertTerminal)
        {
            List<AddJustification> arrs = JsonConvert.DeserializeObject<List<AddJustification>>(objInsertTerminal);

            var reqBody = new AddJustification
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                MerchantId = arrs[0].MerchantId,
                TerminalIssuanceType = arrs[0].TerminalIssuanceType,
                TerminalTypeRequested = arrs[0].TerminalTypeRequested,
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                Justification = arrs[0].Justification
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.InsertInstallationRequestUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var updateRes = obj["Data"].Value<JArray>();
            List<SuccessResponse> updateResponse = updateRes.ToObject<List<SuccessResponse>>();
            return updateResponse[0].Reason;
        }
        public async Task<TerminalManagementRequestViewModel> TerminalInstallationRequestClose(TerminalManagementRequestViewModel terminalReq)
        {
            //TerminalManagementRequestViewModel TerminalManagementResponseData = new TerminalManagementRequestViewModel();
            string fromDate = "", toDate = "";
            terminalReq.ZonalOffices.AddRange(await _commonActionService.GetZonalOfficeList());
            if (!string.IsNullOrEmpty(terminalReq.FromDate) && !string.IsNullOrEmpty(terminalReq.FromDate))
            {
                string[] fromDateArr = terminalReq.FromDate.Split("-");
                string[] toDateArr = terminalReq.ToDate.Split("-");

                fromDate = fromDateArr[2] + "-" + fromDateArr[1] + "-" + fromDateArr[0];
                toDate = toDateArr[2] + "-" + toDateArr[1] + "-" + toDateArr[0];

            }
            else
            {

                return terminalReq;
            }
            var TerminalManagementRequest = new TerminalManagementRequestViewModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FromDate = fromDate,
                ToDate = toDate,
                MerchantId = terminalReq.MerchantId,
                TerminalId = terminalReq.TerminalId,
                ZonalOfficeId = terminalReq.ZonalOfficeId != "0" ? terminalReq.ZonalOfficeId : "",
                RegionalOfficeId = terminalReq.RegionalOfficeId != "0" ? terminalReq.RegionalOfficeId : ""
            };
            StringContent ResponseContent = new StringContent(JsonConvert.SerializeObject(TerminalManagementRequest), Encoding.UTF8, "application/json");

            var TerminalRequestResponse = await _requestService.CommonRequestService(ResponseContent, WebApiUrl.searchforterminalinstallationrequestclose);

            JObject TerminalReqObj = JObject.Parse(JsonConvert.DeserializeObject(TerminalRequestResponse).ToString());
            var TerminalReqObjJarr = TerminalReqObj["Data"].Value<JArray>();
            List<TerminalManagementRequestDetailsModel> TerminalInstallReqList = TerminalReqObjJarr.ToObject<List<TerminalManagementRequestDetailsModel>>();
            terminalReq.TerminalManagementRequestDetails.AddRange(TerminalInstallReqList);
            terminalReq.Reasons.AddRange(await _commonActionService.GetTerminalRequestCloseReason());
            return terminalReq;
        }
        public async Task<string> SubmitTerminalRequestClose([FromBody] TerminalManagementRequestModel terminalManagementRequestModel)
        {
            var terminalRequestCloseForms = new TerminalManagementRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ReasonId = terminalManagementRequestModel.ReasonId,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjMerchantTerminalInstallationRequestCloseDetail = terminalManagementRequestModel.ObjMerchantTerminalInstallationRequestCloseDetail
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(terminalRequestCloseForms), Encoding.UTF8, "application/json");
            var closeRequestResponse = await _requestService.CommonRequestService(requestContent, WebApiUrl.updateterminalinstallationrequestclose);
            JObject closeRequestResponseObj = JObject.Parse(JsonConvert.DeserializeObject(closeRequestResponse).ToString());

            if (closeRequestResponseObj["Status_Code"].ToString() == "200")
            {
                var closeRequestResponseJarr = closeRequestResponseObj["Data"].Value<JArray>();
                List<SuccessResponse> closeRequestResponseList = closeRequestResponseJarr.ToObject<List<SuccessResponse>>();
                return closeRequestResponseList.First().Reason.ToString();
            }
            else
            {
                return closeRequestResponseObj["Message"].ToString();
            }
        }
        public async Task<TerminalManagementRequestViewModel> ViewTerminalInstallationRequestStatus(TerminalManagementRequestViewModel terminalReq)
        {
          
            string fromDate = "", toDate = "";
            terminalReq.ZonalOffices.AddRange(await _commonActionService.GetZonalOfficeList());
            if (!string.IsNullOrEmpty(terminalReq.FromDate) && !string.IsNullOrEmpty(terminalReq.FromDate))
            {
                string[] fromDateArr = terminalReq.FromDate.Split("-");
                string[] toDateArr = terminalReq.ToDate.Split("-");

                fromDate = fromDateArr[2] + "-" + fromDateArr[1] + "-" + fromDateArr[0];
                toDate = toDateArr[2] + "-" + toDateArr[1] + "-" + toDateArr[0];

            }
            else
            {

                return terminalReq;
            }
            var TerminalManagementRequest = new TerminalManagementRequestViewModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FromDate = fromDate,
                ToDate = toDate,
                MerchantId = terminalReq.MerchantId,
                TerminalId = terminalReq.TerminalId,
                ZonalOfficeId = terminalReq.ZonalOfficeId != "0" ? terminalReq.ZonalOfficeId : "",
                RegionalOfficeId = terminalReq.RegionalOfficeId != "0" ? terminalReq.RegionalOfficeId : ""
            };
            StringContent ResponseContent = new StringContent(JsonConvert.SerializeObject(TerminalManagementRequest), Encoding.UTF8, "application/json");

            var TerminalRequestResponse = await _requestService.CommonRequestService(ResponseContent, WebApiUrl.viewterminalinstallationrequeststatus);

            JObject TerminalReqObj = JObject.Parse(JsonConvert.DeserializeObject(TerminalRequestResponse).ToString());
            var TerminalReqObjJarr = TerminalReqObj["Data"].Value<JArray>();
            List<TerminalManagementRequestDetailsModel> TerminalInstallReqList = TerminalReqObjJarr.ToObject<List<TerminalManagementRequestDetailsModel>>();
            terminalReq.TerminalManagementRequestDetails.AddRange(TerminalInstallReqList);
            terminalReq.Reasons.AddRange(await _commonActionService.GetTerminalRequestCloseReason());
            return terminalReq;
        }

        public async Task<List<TerminalApprovalReqResponse>> GetTerminalInstallationReqApproval(TerminalApprovalReq entity)
        {
            var reqBody = new TerminalApprovalReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FromDate = entity.FromDate,
                ToDate = entity.ToDate,
                Category = entity.Category,
                ZonalOfficeId = entity.ZonalOfficeId,
                RegionalOfficeId = entity.RegionalOfficeId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetTerminalInstallReqAppUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var updateRes = obj["Data"].Value<JArray>();
            List<TerminalApprovalReqResponse> approvalList = updateRes.ToObject<List<TerminalApprovalReqResponse>>();
            return approvalList;
        }

        public async Task<string> DoApprovalTerminal(string ObjMerchantTerminalInsertInput, string remark)
        {
            ObjMerchantTerminalInsertInput[] arrs = JsonConvert.DeserializeObject<ObjMerchantTerminalInsertInput[]>(ObjMerchantTerminalInsertInput);

            var forms = new TerminalApprovalReject
            {
                UserAgent = CommonBase.useragent,
                UserIp= CommonBase.userip,
                UserId= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                Remark = remark,
                Action = "4",
                ModifiedBy= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjMerchantTerminalInsertInput = arrs
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ApproveRejectTerminalUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();

            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            return responseMsg[0].Reason;
        }

        public async Task<string> DoRejectTerminal(string ObjMerchantTerminalInsertInput, string remark)
        {
            ObjMerchantTerminalInsertInput[] arrs = JsonConvert.DeserializeObject<ObjMerchantTerminalInsertInput[]>(ObjMerchantTerminalInsertInput);

            var forms = new TerminalApprovalReject
            {
                UserAgent = CommonBase.useragent,
                UserIp= CommonBase.userip,
                UserId= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                Remark = remark,
                Action = "13",
                ModifiedBy= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjMerchantTerminalInsertInput = arrs
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ApproveRejectTerminalUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();

            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            return responseMsg[0].Reason;
        }
    }
}
