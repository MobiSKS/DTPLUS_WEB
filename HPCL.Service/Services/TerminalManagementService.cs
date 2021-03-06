using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.TerminalManagement;
using HPCL.Common.Models.ResponseModel.TerminalManagementResponse;
using HPCL.Common.Models.ViewModel;
using HPCL.Common.Models.ViewModel.Merchant;
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
    public class TerminalManagementService : ITerminalManagement
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

        public async Task<TerminalInstallationRequestResponse> TerminalInstallationRequest([FromBody] TerminalManagement entity)
        {
            var searchBody = new TerminalManagement();

            if (entity.MerchantId != null)
            {
                searchBody = new TerminalManagement
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    ZonalOfficeId = entity.ZonalOfficeId=="0"?null:entity.ZonalOfficeId,
                    RegionalOfficeId = entity.RegionalOfficeId,
                    MerchantId = entity.MerchantId,
                    SBUTypeId=entity.SBUTypeId,
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Merchant")
            {
                searchBody = new TerminalManagement
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    ZonalOfficeId = entity.ZonalOfficeId == "0" ? null : entity.ZonalOfficeId,
                    RegionalOfficeId = entity.RegionalOfficeId,
                    MerchantId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    SBUTypeId=entity.SBUTypeId,
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.TerminalInstallationRequestUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            //var searchRes = obj["Data"].Value<JObject>();
            //var ObjMerchant = searchRes["ObjMerchantDetail"].Value<JArray>();
            //var ObjTerminal = searchRes["ObjTerminalDetail"].Value<JArray>();

            TerminalInstallationRequestResponse res = obj.ToObject<TerminalInstallationRequestResponse>();

            //List<ObjMerchantDetail> objMerchantList = ObjMerchant.ToObject<List<ObjMerchantDetail>>();
            //List<ObjTerminalDetail> searchList = ObjTerminal.ToObject<List<ObjTerminalDetail>>();
            //return Tuple.Create(objMerchantList, searchList);

            return res;
        }

        public async Task<List<string>> AddJustification(string objInsertTerminal)
        {
            List<AddJustification> arrs = JsonConvert.DeserializeObject<List<AddJustification>>(objInsertTerminal);

            var reqBody = new AddJustification
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
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
            List<string> MessageList = new List<string>();
            MessageList.Add(Convert.ToString(updateResponse[0].Status));
            MessageList.Add(updateResponse[0].Reason);
            return MessageList;
        }

        public async Task<TerminalManagementRequestViewModel> TerminalInstallationRequestClose(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId,string SBUTypeId)
        {
            //string fromDate = "", toDate = "";
            TerminalManagementRequestViewModel terminalReq = new TerminalManagementRequestViewModel();
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(FromDate))
            {
                terminalReq.FromDate = await _commonActionService.changeDateFormat(FromDate);
                terminalReq.ToDate = await _commonActionService.changeDateFormat(ToDate);
            }
            else
            {
                terminalReq.FromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                terminalReq.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var TerminalManagementRequest = new TerminalManagementModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FromDate = terminalReq.FromDate,
                ToDate = terminalReq.ToDate,
                MerchantId = MerchantId,
                TerminalId = TerminalId,
                ZonalOfficeId = ZonalOfficeId != "0" ? ZonalOfficeId : "",
                RegionalOfficeId = RegionalOfficeId != "0" ? RegionalOfficeId : "",
                SBUTypeId=SBUTypeId
            };
            StringContent ResponseContent = new StringContent(JsonConvert.SerializeObject(TerminalManagementRequest), Encoding.UTF8, "application/json");

            var TerminalRequestResponse = await _requestService.CommonRequestService(ResponseContent, WebApiUrl.searchforterminalinstallationrequestclose);

            JObject TerminalReqObj = JObject.Parse(JsonConvert.DeserializeObject(TerminalRequestResponse).ToString());
            terminalReq = JsonConvert.DeserializeObject<TerminalManagementRequestViewModel>(TerminalRequestResponse);
            var TerminalReqObjJarr = TerminalReqObj["Data"].Value<JArray>();
            List<TerminalManagementRequestDetailsModel> TerminalInstallReqList = TerminalReqObjJarr.ToObject<List<TerminalManagementRequestDetailsModel>>();
            terminalReq.TerminalManagementRequestDetails.AddRange(TerminalInstallReqList);
            terminalReq.ZonalOffices.AddRange(await _commonActionService.GetZonalOfficeList());
            terminalReq.Reasons.AddRange(await _commonActionService.GetTerminalRequestCloseReason());
            return terminalReq;
        }
        public async Task<List<string>> SubmitTerminalRequestClose([FromBody] TerminalManagementRequestModel terminalManagementRequestModel)
        {
            var terminalRequestCloseForms = new TerminalManagementRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                ReasonId = terminalManagementRequestModel.ReasonId,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjMerchantTerminalInstallationRequestCloseDetail = terminalManagementRequestModel.ObjMerchantTerminalInstallationRequestCloseDetail
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(terminalRequestCloseForms), Encoding.UTF8, "application/json");
            var closeRequestResponse = await _requestService.CommonRequestService(requestContent, WebApiUrl.updateterminalinstallationrequestclose);
            JObject closeRequestResponseObj = JObject.Parse(JsonConvert.DeserializeObject(closeRequestResponse).ToString());


            var closeRequestResponseJarr = closeRequestResponseObj["Data"].Value<JArray>();
            List<SuccessResponse> closeRequestResponseList = closeRequestResponseJarr.ToObject<List<SuccessResponse>>();
            List<string> MessageList = new List<string>();
            MessageList.Add(Convert.ToString(closeRequestResponseList[0].Status));
            MessageList.Add(closeRequestResponseList[0].Reason);
            return MessageList;

        }
        public async Task<TerminalManagementRequestViewModel> ViewTerminalInstallationRequestStatus(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId,string SBUTypeId)
        {
            TerminalManagementRequestViewModel terminalReq = new TerminalManagementRequestViewModel();
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(FromDate))
            {
                terminalReq.FromDate = await _commonActionService.changeDateFormat(FromDate);
                terminalReq.ToDate = await _commonActionService.changeDateFormat(ToDate);
            }
            else
            {
                terminalReq.FromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                terminalReq.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var TerminalManagementRequest = new TerminalManagementModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FromDate = terminalReq.FromDate,
                ToDate = terminalReq.ToDate,
                MerchantId = MerchantId,
                TerminalId = TerminalId,
                ZonalOfficeId = ZonalOfficeId != "0" ? ZonalOfficeId : "",
                RegionalOfficeId = RegionalOfficeId != "0" ? RegionalOfficeId : "",
                SBUTypeId = SBUTypeId
            };
            StringContent ResponseContent = new StringContent(JsonConvert.SerializeObject(TerminalManagementRequest), Encoding.UTF8, "application/json");

            var TerminalRequestResponse = await _requestService.CommonRequestService(ResponseContent, WebApiUrl.viewterminalinstallationrequeststatus);

            JObject TerminalReqObj = JObject.Parse(JsonConvert.DeserializeObject(TerminalRequestResponse).ToString());
            terminalReq = JsonConvert.DeserializeObject<TerminalManagementRequestViewModel>(TerminalRequestResponse);
            var TerminalReqObjJarr = TerminalReqObj["Data"].Value<JArray>();
            List<TerminalManagementRequestDetailsModel> TerminalInstallReqList = TerminalReqObjJarr.ToObject<List<TerminalManagementRequestDetailsModel>>();
            terminalReq.TerminalManagementRequestDetails.AddRange(TerminalInstallReqList);
            terminalReq.Reasons.AddRange(await _commonActionService.GetTerminalRequestCloseReason());
            terminalReq.ZonalOffices.AddRange(await _commonActionService.GetZonalOfficeList());
            return terminalReq;
        }
        public async Task<TerminalDeinstallationRequestViewModel> TerminalDeInstallationRequest(TerminalDeinstallationRequestViewModel terminalReq)
        {

            TerminalDeinstallationRequestViewModel ResponseModel = new TerminalDeinstallationRequestViewModel();

            var TerminalManagementRequest = new TerminalManagementModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                MerchantId = terminalReq.MerchantId != null ? terminalReq.MerchantId : "",
                TerminalId = terminalReq.TerminalId != null ? terminalReq.TerminalId : "",
                ZonalOfficeId = terminalReq.ZonalOfficeId != "0" ? terminalReq.ZonalOfficeId : "",
                RegionalOfficeId = terminalReq.RegionalOfficeId != "0" ? terminalReq.RegionalOfficeId : "",
                SBUTypeId=terminalReq.SBUTypeId
            };
            StringContent ResponseContent = new StringContent(JsonConvert.SerializeObject(TerminalManagementRequest), Encoding.UTF8, "application/json");

            var TerminalRequestResponse = await _requestService.CommonRequestService(ResponseContent, WebApiUrl.getterminaldeinstallationrequest);
            ResponseModel = JsonConvert.DeserializeObject<TerminalDeinstallationRequestViewModel>(TerminalRequestResponse);

            JObject TerminalReqObj = JObject.Parse(JsonConvert.DeserializeObject(TerminalRequestResponse).ToString());
            var TerminalReqObjJarr = TerminalReqObj["Data"].Value<JObject>();
            var merchantDetails = TerminalReqObjJarr["ObjMerchantDeinstallationDetail"].Value<JArray>();
            var terminalDetails = TerminalReqObjJarr["ObjTerminalDeinstallationDetail"].Value<JArray>();
            List<MerchantDeinstallationDetail> MerchantDeInstallReqList =
                merchantDetails.ToObject<List<MerchantDeinstallationDetail>>();
            List<TerminalDeinstallationDetail> TerminalDeInstallReqList =
               terminalDetails.ToObject<List<TerminalDeinstallationDetail>>();
            terminalReq.ObjMerchantDeinstallationDetail.AddRange(MerchantDeInstallReqList);
            terminalReq.ObjTerminalDeinstallationDetail.AddRange(TerminalDeInstallReqList);
            terminalReq.Message = ResponseModel.Message;
            // terminalReq.ZonalOffices.AddRange(await _commonActionService.GetZonalOfficeList());
            //terminalReq.Reasons.AddRange(await _commonActionService.GetTerminalRequestCloseReason());
            return terminalReq;
        }

        public async Task<List<string>> SubmitDeinstallRequest([FromBody] TerminalDeinstallationRequestUpdateModel deInstallationRequest)
        {
            var deInstallationRequestForms = new TerminalDeinstallationRequestUpdateModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                Comments = deInstallationRequest.Comments,
                DeinstallationType = deInstallationRequest.DeinstallationType,
                MerchantId = deInstallationRequest.MerchantId,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjUpdateTerminalDeInstalRequest = deInstallationRequest.ObjUpdateTerminalDeInstalRequest
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(deInstallationRequestForms), Encoding.UTF8, "application/json");
            var deInstallResponse = await _requestService.CommonRequestService(requestContent, WebApiUrl.updateterminaldeinstalrequest);
            JObject deInstallResponseObj = JObject.Parse(JsonConvert.DeserializeObject(deInstallResponse).ToString());


            var deInstallResponseJarr = deInstallResponseObj["Data"].Value<JArray>();
            List<SuccessResponse> deInstallResponseList = deInstallResponseJarr.ToObject<List<SuccessResponse>>();
            List<string> MessageList = new List<string>();
            MessageList.Add(Convert.ToString(deInstallResponseList[0].Status));
            MessageList.Add(deInstallResponseList[0].Reason);
            return MessageList;

        }

        public async Task<TerminalDeinstallationRequestViewModel> TerminalDeInstallationRequestClose(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId, string SBUTypeId)
        {
            //string fromDate = "", toDate = "";
            TerminalDeinstallationRequestViewModel terminalReq = new TerminalDeinstallationRequestViewModel();
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(FromDate))
            {
                terminalReq.FromDate = await _commonActionService.changeDateFormat(FromDate);
                terminalReq.ToDate = await _commonActionService.changeDateFormat(ToDate);
            }
            else
            {
                terminalReq.FromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                terminalReq.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var TerminalManagementRequest = new TerminalManagementModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FromDate = terminalReq.FromDate,
                ToDate = terminalReq.ToDate,
                MerchantId = MerchantId,
                TerminalId = TerminalId,
                ZonalOfficeId = ZonalOfficeId != "0" ? ZonalOfficeId : "",
                RegionalOfficeId = RegionalOfficeId != "0" ? RegionalOfficeId : "",
                SBUTypeId=SBUTypeId,
            };
            StringContent ResponseContent = new StringContent(JsonConvert.SerializeObject(TerminalManagementRequest), Encoding.UTF8, "application/json");

            var TerminalRequestResponse = await _requestService.CommonRequestService(ResponseContent, WebApiUrl.getterminaldeinstallationrequestclose);

            JObject TerminalReqObj = JObject.Parse(JsonConvert.DeserializeObject(TerminalRequestResponse).ToString());
            terminalReq = JsonConvert.DeserializeObject<TerminalDeinstallationRequestViewModel>(TerminalRequestResponse);
            var TerminalReqObjJarr = TerminalReqObj["Data"].Value<JArray>();
            List<TerminalDeinstallationRequestDetailsViewModel> TerminalDeInstallReqList = TerminalReqObjJarr.ToObject<List<TerminalDeinstallationRequestDetailsViewModel>>();
            terminalReq.TerminalDeinstallationRequestDetails.AddRange(TerminalDeInstallReqList);
            terminalReq.Reasons.AddRange(await _commonActionService.GetTerminalRequestCloseReason());
            terminalReq.ZonalOffices.AddRange(await _commonActionService.GetZonalOfficeList());
            return terminalReq;
        }


        public async Task<List<string>> SubmitDeinstallationRequestClose([FromBody] TerminalDeinstallationCloseModel TerminalDeinstallationClose)
        {
            var deInstallationCloseForms = new TerminalDeinstallationCloseModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                Status = TerminalDeinstallationClose.Status,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                Comments = TerminalDeinstallationClose.Comments,
                ObjMerchantTerminalMapInput = TerminalDeinstallationClose.ObjMerchantTerminalMapInput
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(deInstallationCloseForms), Encoding.UTF8, "application/json");
            var deInstallCloseResponse = await _requestService.CommonRequestService(requestContent, WebApiUrl.terminaldeinstalupdaterequestclose);
            JObject closeResponseObj = JObject.Parse(JsonConvert.DeserializeObject(deInstallCloseResponse).ToString());


            var closeResponseJarr = closeResponseObj["Data"].Value<JArray>();
            List<SuccessResponse> closeResponseList = closeResponseJarr.ToObject<List<SuccessResponse>>();
            List<string> MessageList = new List<string>();
            MessageList.Add(Convert.ToString(closeResponseList[0].Status));
            MessageList.Add(closeResponseList[0].Reason);
            return MessageList;

        }

        public async Task<TerminalDeinstallationRequestViewModel> ViewTerminalDeinstallationRequestStatus(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId, string SBUTypeId)
        {
            //string fromDate = "", toDate = "";
            TerminalDeinstallationRequestViewModel terminalReq = new TerminalDeinstallationRequestViewModel();
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(FromDate))
            {
                terminalReq.FromDate = await _commonActionService.changeDateFormat(FromDate);
                terminalReq.ToDate = await _commonActionService.changeDateFormat(ToDate);
            }
            else
            {
                terminalReq.FromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                terminalReq.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var TerminalManagementRequest = new TerminalManagementModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FromDate = terminalReq.FromDate,
                ToDate = terminalReq.ToDate,
                MerchantId = MerchantId,
                TerminalId = TerminalId,
                ZonalOfficeId = ZonalOfficeId != "0" ? ZonalOfficeId : "",
                RegionalOfficeId = RegionalOfficeId != "0" ? RegionalOfficeId : "",
                SBUTypeId = SBUTypeId
            };
            StringContent ResponseContent = new StringContent(JsonConvert.SerializeObject(TerminalManagementRequest), Encoding.UTF8, "application/json");

            var TerminalRequestResponse = await _requestService.CommonRequestService(ResponseContent, WebApiUrl.viewterminaldeinstallationrequeststatus);

            JObject TerminalReqObj = JObject.Parse(JsonConvert.DeserializeObject(TerminalRequestResponse).ToString());
            terminalReq = JsonConvert.DeserializeObject<TerminalDeinstallationRequestViewModel>(TerminalRequestResponse);
            var TerminalReqObjJarr = TerminalReqObj["Data"].Value<JArray>();
            List<TerminalDeinstallationRequestDetailsViewModel> TerminalInstallReqList = TerminalReqObjJarr.ToObject<List<TerminalDeinstallationRequestDetailsViewModel>>();
            terminalReq.TerminalDeinstallationRequestDetails.AddRange(TerminalInstallReqList);
            terminalReq.Reasons.AddRange(await _commonActionService.GetTerminalRequestCloseReason());
            terminalReq.ZonalOffices.AddRange(await _commonActionService.GetZonalOfficeList());
            return terminalReq;
        }

        public async Task<TerminalApprovalReqResponse> GetTerminalInstallationReqApproval(TerminalApprovalReq entity)
        {
            TerminalApprovalReqResponse TerminalApprovalReqResponse = new TerminalApprovalReqResponse();
            string fromDate = "", toDate = "";
            if (!string.IsNullOrEmpty(entity.FromDate) && !string.IsNullOrEmpty(entity.FromDate))
            {
                fromDate = await _commonActionService.changeDateFormat(entity.FromDate);
                toDate = await _commonActionService.changeDateFormat(entity.ToDate);
            }
            else
            {
                fromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                toDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var reqBody = new TerminalApprovalReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FromDate = fromDate,
                ToDate = toDate,
                Category = entity.Category,
                ZonalOfficeId = entity.ZonalOfficeId == "0" ? "" : entity.ZonalOfficeId,
                RegionalOfficeId = entity.RegionalOfficeId == "0" ? "" : entity.RegionalOfficeId,
                SBUTypeId= entity.SBUTypeId,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetTerminalInstallReqAppUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            TerminalApprovalReqResponse = JsonConvert.DeserializeObject<TerminalApprovalReqResponse>(response);
            var updateRes = obj["Data"].Value<JArray>();
            List<TerminalApprovalReqDetails> approvalList = updateRes.ToObject<List<TerminalApprovalReqDetails>>();
            TerminalApprovalReqResponse.TerminalApprovalReqDetails.AddRange(approvalList);
            return TerminalApprovalReqResponse;
        }

        public async Task<List<string>> DoApprovalTerminal(string ObjMerchantTerminalInsertInput, string remark)
        {
            ObjMerchantTerminalInsertInput[] arrs = JsonConvert.DeserializeObject<ObjMerchantTerminalInsertInput[]>(ObjMerchantTerminalInsertInput);

            var forms = new TerminalApprovalReject
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                Remark = remark,
                Action = "Approve",
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjMerchantTerminalInsertInput = arrs
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ApproveRejectTerminalUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();

            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            List<string> MessageList = new List<string>();
            MessageList.Add(Convert.ToString(responseMsg[0].Status));
            MessageList.Add(responseMsg[0].Reason);
            return MessageList;
        }

        public async Task<List<string>> DoRejectTerminal(string ObjMerchantTerminalInsertInput, string remark)
        {
            ObjMerchantTerminalInsertInput[] arrs = JsonConvert.DeserializeObject<ObjMerchantTerminalInsertInput[]>(ObjMerchantTerminalInsertInput);

            var forms = new TerminalApprovalReject
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                Remark = remark,
                Action = "Reject",
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjMerchantTerminalInsertInput = arrs
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ApproveRejectTerminalUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();

            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            List<string> MessageList = new List<string>();
            MessageList.Add(Convert.ToString(responseMsg[0].Status));
            MessageList.Add(responseMsg[0].Reason);
            return MessageList;
        }
        public async Task<TerminalDeinstallationRequestViewModel> ProblematicDeInstalledToDeInstalled(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId, string SBUTypeId)
        {
            //string fromDate = "", toDate = "";
            TerminalDeinstallationRequestViewModel terminalReq = new TerminalDeinstallationRequestViewModel();
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(FromDate))
            {
                terminalReq.FromDate = await _commonActionService.changeDateFormat(FromDate);
                terminalReq.ToDate = await _commonActionService.changeDateFormat(ToDate);
            }
            else
            {
                terminalReq.FromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                terminalReq.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var deInstallForms = new TerminalManagementModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FromDate = terminalReq.FromDate,
                ToDate = terminalReq.ToDate,
                MerchantId = MerchantId,
                TerminalId = TerminalId,
                ZonalOfficeId = ZonalOfficeId != "0" ? ZonalOfficeId : "",
                RegionalOfficeId = RegionalOfficeId != "0" ? RegionalOfficeId : "",
                SBUTypeId = SBUTypeId 
            };
            StringContent ResponseContent = new StringContent(JsonConvert.SerializeObject(deInstallForms), Encoding.UTF8, "application/json");

            var deInstallResponse = await _requestService.CommonRequestService(ResponseContent, WebApiUrl.getproblematicdeinstalledtodeinstalled);

            JObject deInstallReqObj = JObject.Parse(JsonConvert.DeserializeObject(deInstallResponse).ToString());
            terminalReq = JsonConvert.DeserializeObject<TerminalDeinstallationRequestViewModel>(deInstallResponse);
            var deInstallObjJarr = deInstallReqObj["Data"].Value<JArray>();
            List<TerminalDeinstallationRequestDetailsViewModel> TerminalDeInstallReqList = deInstallObjJarr.ToObject<List<TerminalDeinstallationRequestDetailsViewModel>>();
            terminalReq.TerminalDeinstallationRequestDetails.AddRange(TerminalDeInstallReqList);
            terminalReq.Reasons.AddRange(await _commonActionService.GetTerminalRequestCloseReason());
            terminalReq.ZonalOffices.AddRange(await _commonActionService.GetZonalOfficeList());
            return terminalReq;
        }

        public async Task<List<string>> SubmitProblematicDeinstalltoDeinstall([FromBody] TerminalDeinstallationCloseModel deInstall)
        {
            var deInstallForms = new TerminalDeinstallationCloseModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                Remark = deInstall.Remark,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjTerminalProblematicDeinstalledToDeinstalled = deInstall.ObjTerminalProblematicDeinstalledToDeinstalled
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(deInstallForms), Encoding.UTF8, "application/json");
            var deInstallResponse = await _requestService.CommonRequestService(requestContent, WebApiUrl.insertproblematicdeinstalledtodeinstalled);
            JObject deInstallResponseObj = JObject.Parse(JsonConvert.DeserializeObject(deInstallResponse).ToString());


            var deInstallResponseJarr = deInstallResponseObj["Data"].Value<JArray>();
            List<SuccessResponse> deInstalleResponseList = deInstallResponseJarr.ToObject<List<SuccessResponse>>();
            List<string> MessageList = new List<string>();
            MessageList.Add(Convert.ToString(deInstalleResponseList[0].Status));
            MessageList.Add(deInstalleResponseList[0].Reason);
            return MessageList;

        }

        public async Task<SearchTerminalModel> SearchTerminal()
        {
            SearchTerminalModel searchTerminalModel = new SearchTerminalModel();
            return searchTerminalModel;
        }

        public async Task<List<SearchTerminalDetailsResponseModal>> SearchTerminalDetails(string terminalId, string merchantId, string terminalType, string issueDate)
        {
            var searchDetailsTableForms = new SearchTerminalModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                MerchantId = string.IsNullOrEmpty(merchantId) ? "" : merchantId,
                TerminalId = string.IsNullOrEmpty(terminalId) ? "" : terminalId,
                TerminalType = string.IsNullOrEmpty(terminalType) || terminalType == "0" ? "" : terminalType,
                IssueDate = string.IsNullOrEmpty(issueDate) ? "" : issueDate
            };

            StringContent searchDetailsTableContent = new StringContent(JsonConvert.SerializeObject(searchDetailsTableForms), Encoding.UTF8, "application/json");

            var searchDetailsTableResponse = await _requestService.CommonRequestService(searchDetailsTableContent, WebApiUrl.searchTerminal);

            JObject searchDetailsTableObj = JObject.Parse(JsonConvert.DeserializeObject(searchDetailsTableResponse).ToString());
            var searchDetailsTableJarr = searchDetailsTableObj["Data"].Value<JArray>();
            List<SearchTerminalDetailsResponseModal> searchDetailsTableModels = searchDetailsTableJarr.ToObject<List<SearchTerminalDetailsResponseModal>>();
            return searchDetailsTableModels;
        }
        public async Task<ManageTerminalResponse> GetAllStatusValue(string MerchantId, string TerminalId, string Status)
        {
            var searchBody = new ManageTerminalRequest();

            searchBody = new ManageTerminalRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                //StatusFlag = entity.StatusFlag,
                TerminalId = TerminalId,
                MerchantId = MerchantId,
                DeploymentStatus = Status == "-1" ? null : Status

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ManageTerminalUrl);

            ManageTerminalResponse searchList = JsonConvert.DeserializeObject<ManageTerminalResponse>(response);
            return searchList;
        }
        public async Task<ManageTerminalModel> ManageTerminal()
        {
            ManageTerminalModel custModel = new ManageTerminalModel();
            custModel.StatusModals.AddRange(await _commonActionService.GetStatusTypeforTerminal());
            return custModel;
        }
        public async Task<MerchantModel> GetMerchantSummaryData(string ERPCode)
        {
            char flag = 'N';
            MerchantModel merchantModel = new MerchantModel();
            if (!string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Session.GetString("ERPCode")))
            {
                merchantModel.ERPCode = _httpContextAccessor.HttpContext.Session.GetString("ERPCode");
            }


            var MerchantFormDetails = new Dictionary<string, string>
                    {
                        {"Useragent", CommonBase.useragent},
                        { "Userip", _httpContextAccessor.HttpContext.Session.GetString("IpAddress")},
                        {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
                        { "ErpCode", ERPCode}
                    };
            MerchantGetDetailsModel merchantDetailsModel = new MerchantGetDetailsModel();
            StringContent MerchantFormDetailsContent = new StringContent(JsonConvert.SerializeObject(MerchantFormDetails), Encoding.UTF8, "application/json");

            var merchantRejectedResponse = await _requestService.CommonRequestService(MerchantFormDetailsContent, WebApiUrl.getMerchantbyERPCode);

            JObject merchantRejectedObj = JObject.Parse(JsonConvert.DeserializeObject(merchantRejectedResponse).ToString());
            var merchantRejectedJarr = merchantRejectedObj["Data"].Value<JArray>();
            List<MerchantGetDetailsModel> dtls = merchantRejectedJarr.ToObject<List<MerchantGetDetailsModel>>();
            merchantDetailsModel = dtls.First();
            merchantModel.MerchantID = merchantDetailsModel.MerchantId;
            merchantModel.ERPCode = merchantDetailsModel.ErpCode;
            merchantModel.RetailOutletName = merchantDetailsModel.RetailOutletName;
            merchantModel.MerchantType = merchantDetailsModel.MerchantTypeName;
            merchantModel.DealerName = merchantDetailsModel.DealerName;
            merchantModel.MappedMerchantID = merchantDetailsModel.MappedMerchantId;
            merchantModel.DealerMobileNumber = merchantDetailsModel.DealerMobileNo;
            merchantModel.OutletCategory = merchantDetailsModel.OutletCategoryName;
            merchantModel.PreHighwayNumber = merchantDetailsModel.HighwayNo1;
            merchantModel.HighwayNumber = merchantDetailsModel.HighwayNo2;
            merchantModel.HighwayName = merchantDetailsModel.HighwayName;
            merchantModel.SBUType = merchantDetailsModel.SBUName;
            merchantModel.LPG_CNGSale = merchantDetailsModel.LPGCNGSale;
            merchantModel.PANCardNumber = merchantDetailsModel.PancardNumber;
            merchantModel.GSTNumber = merchantDetailsModel.GSTNumber;

            merchantModel.Retail_Outlet_Address1 = merchantDetailsModel.RetailOutletAddress1;
            merchantModel.Retail_Outlet_Address2 = merchantDetailsModel.RetailOutletAddress2;
            merchantModel.Retail_Outlet_Address3 = merchantDetailsModel.RetailOutletAddress3;
            if (!(String.IsNullOrEmpty(merchantDetailsModel.RetailOutletAddress3)) && !(String.IsNullOrEmpty(merchantDetailsModel.RetailOutletAddress2)))
                merchantModel.Retail_Outlet_Address1 = merchantDetailsModel.RetailOutletAddress1 + "," + merchantDetailsModel.RetailOutletAddress2 + "," + merchantDetailsModel.RetailOutletAddress3;
            else if (String.IsNullOrEmpty(merchantDetailsModel.RetailOutletAddress3) && !(String.IsNullOrEmpty(merchantDetailsModel.RetailOutletAddress2)))
                merchantModel.Retail_Outlet_Address1 = merchantDetailsModel.RetailOutletAddress1 + "," + merchantDetailsModel.RetailOutletAddress2;
            else if (String.IsNullOrEmpty(merchantDetailsModel.RetailOutletAddress3) && String.IsNullOrEmpty(merchantDetailsModel.RetailOutletAddress2))
                merchantModel.Retail_Outlet_Address1 = merchantDetailsModel.RetailOutletAddress1;
            merchantModel.Retail_Outlet_Location = merchantDetailsModel.RetailOutletLocation;
            merchantModel.Retail_Outlet_City = merchantDetailsModel.RetailOutletCity;
            merchantModel.Retail_Outlet_State = merchantDetailsModel.RetailOutletStateName;
            merchantModel.Retail_Outlet_District = merchantDetailsModel.RetailOutletDistrictName;
            if (!(String.IsNullOrEmpty(merchantDetailsModel.RetailOutletStateName)))
                merchantModel.Retail_Outlet_District = merchantDetailsModel.RetailOutletDistrictName + "," + merchantDetailsModel.RetailOutletStateName;
            merchantModel.Retail_DistictID = merchantDetailsModel.RetailOutletDistrictId;
            merchantModel.Retail_Outlet_Pin = merchantDetailsModel.RetailOutletPinNumber;
            merchantModel.ZonalOffice = merchantDetailsModel.ZonalOfficeName;
            merchantModel.ZonalOfficeID = merchantDetailsModel.ZonalOfficeId;
            merchantModel.RegionalOffice = merchantDetailsModel.RegionalOfficeName;
            merchantModel.RegionalOfcID = merchantDetailsModel.RegionalOfficeId;
            merchantModel.SalesArea = merchantDetailsModel.SalesAreaName;
            merchantModel.SalesAreaID = merchantDetailsModel.SalesAreaId;
            merchantModel.FName = merchantDetailsModel.ContactPersonNameFirstName;
            merchantModel.MName = merchantDetailsModel.ContactPersonNameMiddleName;
            merchantModel.LName = merchantDetailsModel.ContactPersonNameLastName;
            merchantModel.Mobile = merchantDetailsModel.MobileNo;
            merchantModel.Email = merchantDetailsModel.EmailId;
            merchantModel.Misc = merchantDetailsModel.Mics;
            merchantModel.Comm_Address1 = merchantDetailsModel.CommunicationAddress1;
            merchantModel.Comm_Address2 = merchantDetailsModel.CommunicationAddress2;
            merchantModel.Comm_Address3 = merchantDetailsModel.CommunicationAddress3;
            if (!(String.IsNullOrEmpty(merchantDetailsModel.CommunicationAddress3)) && !(String.IsNullOrEmpty(merchantDetailsModel.CommunicationAddress2)))
                merchantModel.Comm_Address1 = merchantDetailsModel.CommunicationAddress1 + "," + merchantDetailsModel.CommunicationAddress2 + "," + merchantDetailsModel.CommunicationAddress3;
            else if (String.IsNullOrEmpty(merchantDetailsModel.CommunicationAddress3) && !(String.IsNullOrEmpty(merchantDetailsModel.CommunicationAddress2)))
                merchantModel.Comm_Address1 = merchantDetailsModel.CommunicationAddress1 + "," + merchantDetailsModel.CommunicationAddress2;
            else if (String.IsNullOrEmpty(merchantDetailsModel.CommunicationAddress3) && String.IsNullOrEmpty(merchantDetailsModel.CommunicationAddress2))
                merchantModel.Comm_Address1 = merchantDetailsModel.CommunicationAddress1;
            merchantModel.Comm_City = merchantDetailsModel.CommunicationCity;
            merchantModel.Comm_State = merchantDetailsModel.CommunicationStateName;
            merchantModel.Comm_District = merchantDetailsModel.CommunicationDistrictName;
            if (!(String.IsNullOrEmpty(merchantDetailsModel.CommunicationStateName)) && !(String.IsNullOrEmpty(merchantDetailsModel.CommunicationDistrictName)))
                merchantModel.Comm_District = merchantDetailsModel.CommunicationDistrictName + "," + merchantDetailsModel.CommunicationStateName;
            else if (!(String.IsNullOrEmpty(merchantDetailsModel.CommunicationStateName)) && (String.IsNullOrEmpty(merchantDetailsModel.CommunicationDistrictName)))
                merchantModel.Comm_District = merchantDetailsModel.CommunicationStateName;
            else if (String.IsNullOrEmpty(merchantDetailsModel.CommunicationStateName) && !(String.IsNullOrEmpty(merchantDetailsModel.CommunicationDistrictName)))
                merchantModel.Comm_District = merchantDetailsModel.CommunicationDistrictName;
            merchantModel.Comm_DistictID = merchantDetailsModel.CommunicationDistrictId;
            merchantModel.Comm_Pin = merchantDetailsModel.CommunicationPinNumber;
            merchantModel.NumOfLiveTerminals = merchantDetailsModel.NoofLiveTerminals;
            merchantModel.TerminalTypeRequested = merchantDetailsModel.TerminalTypeRequested;
            merchantModel.Retail_Outlet_PhoneOffice = merchantDetailsModel.RetailOutletPhoneNumber;
            merchantModel.Retail_Outlet_Fax = merchantDetailsModel.RetailOutletFax;
            merchantModel.Comm_PhoneNumber = merchantDetailsModel.CommunicationPhoneNumber;
            merchantModel.Comm_Fax = merchantDetailsModel.CommunicationFax;


            return merchantModel;

        }
    }
}