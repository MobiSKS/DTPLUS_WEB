﻿using HPCL.Common.Helper;
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
                    UserIp = CommonBase.userip,
                    ZonalOfficeId = entity.ZonalOfficeId,
                    RegionalOfficeId = entity.RegionalOfficeId,
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
                    ZonalOfficeId = entity.ZonalOfficeId,
                    RegionalOfficeId = entity.RegionalOfficeId,
                    MerchantId = _httpContextAccessor.HttpContext.Session.GetString("UserId")
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

        public async Task<TerminalManagementRequestViewModel> TerminalInstallationRequestClose(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId)
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
                UserIp = CommonBase.userip,
                FromDate = terminalReq.FromDate,
                ToDate = terminalReq.ToDate,
                MerchantId = MerchantId,
                TerminalId = TerminalId,
                ZonalOfficeId = ZonalOfficeId != "0" ? ZonalOfficeId : "",
                RegionalOfficeId = RegionalOfficeId != "0" ? RegionalOfficeId : ""
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
        public async Task<TerminalManagementRequestViewModel> ViewTerminalInstallationRequestStatus(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId)
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
                UserIp = CommonBase.userip,
                FromDate = terminalReq.FromDate,
                ToDate = terminalReq.ToDate,
                MerchantId = MerchantId,
                TerminalId = TerminalId,
                ZonalOfficeId = ZonalOfficeId != "0" ? ZonalOfficeId : "",
                RegionalOfficeId = RegionalOfficeId != "0" ? RegionalOfficeId : ""
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
                UserIp = CommonBase.userip,
                MerchantId = terminalReq.MerchantId != null ? terminalReq.MerchantId : "",
                TerminalId = terminalReq.TerminalId != null ? terminalReq.TerminalId : "",
                ZonalOfficeId = terminalReq.ZonalOfficeId != "0" ? terminalReq.ZonalOfficeId : "",
                RegionalOfficeId = terminalReq.RegionalOfficeId != "0" ? terminalReq.RegionalOfficeId : ""
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
            terminalReq.ZonalOffices.AddRange(await _commonActionService.GetZonalOfficeList());
            //terminalReq.Reasons.AddRange(await _commonActionService.GetTerminalRequestCloseReason());
            return terminalReq;
        }

        public async Task<string> SubmitDeinstallRequest([FromBody] TerminalDeinstallationRequestUpdateModel deInstallationRequest)
        {
            var deInstallationRequestForms = new TerminalDeinstallationRequestUpdateModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                Comments = deInstallationRequest.Comments,
                DeinstallationType=deInstallationRequest.DeinstallationType,
                MerchantId=deInstallationRequest.MerchantId,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjUpdateTerminalDeInstalRequest = deInstallationRequest.ObjUpdateTerminalDeInstalRequest
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(deInstallationRequestForms), Encoding.UTF8, "application/json");
            var deInstallResponse = await _requestService.CommonRequestService(requestContent, WebApiUrl.updateterminaldeinstalrequest);
            JObject deInstallResponseObj = JObject.Parse(JsonConvert.DeserializeObject(deInstallResponse).ToString());

            if (deInstallResponseObj["Status_Code"].ToString() == "200")
            {
                var deInstallResponseJarr = deInstallResponseObj["Data"].Value<JArray>();
                List<SuccessResponse> deInstallResponseList = deInstallResponseJarr.ToObject<List<SuccessResponse>>();
                return deInstallResponseList.First().Reason.ToString();
            }
            else
            {
                return deInstallResponseObj["Message"].ToString();
            }
        }

        public async Task<TerminalDeinstallationRequestViewModel> TerminalDeInstallationRequestClose(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId)
        {
            //string fromDate = "", toDate = "";
            TerminalDeinstallationRequestViewModel terminalReq=new TerminalDeinstallationRequestViewModel();
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
                UserIp = CommonBase.userip,
                FromDate = terminalReq.FromDate,
                ToDate = terminalReq.ToDate,
                MerchantId = MerchantId,
                TerminalId = TerminalId,
                ZonalOfficeId = ZonalOfficeId != "0" ? ZonalOfficeId : "",
                RegionalOfficeId = RegionalOfficeId != "0" ? RegionalOfficeId : ""
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


        public async Task<string> SubmitDeinstallationRequestClose([FromBody] TerminalDeinstallationCloseModel TerminalDeinstallationClose)
        {
            var deInstallationCloseForms = new TerminalDeinstallationCloseModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                Status = TerminalDeinstallationClose.Status,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                Comments= TerminalDeinstallationClose.Comments,
                ObjMerchantTerminalMapInput = TerminalDeinstallationClose.ObjMerchantTerminalMapInput
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(deInstallationCloseForms), Encoding.UTF8, "application/json");
            var deInstallCloseResponse = await _requestService.CommonRequestService(requestContent, WebApiUrl.terminaldeinstalupdaterequestclose);
            JObject closeResponseObj = JObject.Parse(JsonConvert.DeserializeObject(deInstallCloseResponse).ToString());

            if (closeResponseObj["Status_Code"].ToString() == "200")
            {
                var closeResponseJarr = closeResponseObj["Data"].Value<JArray>();
                List<SuccessResponse> closeResponseList = closeResponseJarr.ToObject<List<SuccessResponse>>();
                return closeResponseList.First().Reason.ToString();
            }
            else
            {
                return closeResponseObj["Message"].ToString();
            }
        }

        public async Task<TerminalDeinstallationRequestViewModel> ViewTerminalDeinstallationRequestStatus(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId)
        {
            //string fromDate = "", toDate = "";
            TerminalDeinstallationRequestViewModel terminalReq=new TerminalDeinstallationRequestViewModel();
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
                UserIp = CommonBase.userip,
                FromDate = terminalReq.FromDate,
                ToDate = terminalReq.ToDate,
                MerchantId = MerchantId,
                TerminalId = TerminalId,
                ZonalOfficeId = ZonalOfficeId != "0" ? ZonalOfficeId : "",
                RegionalOfficeId = RegionalOfficeId != "0" ? RegionalOfficeId : ""
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
                UserIp = CommonBase.userip,
                FromDate =fromDate,
                ToDate = toDate,
                Category = entity.Category,
                ZonalOfficeId = entity.ZonalOfficeId,
                RegionalOfficeId = entity.RegionalOfficeId
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

        public async Task<string> DoApprovalTerminal(string ObjMerchantTerminalInsertInput, string remark)
        {
            ObjMerchantTerminalInsertInput[] arrs = JsonConvert.DeserializeObject<ObjMerchantTerminalInsertInput[]>(ObjMerchantTerminalInsertInput);

            var forms = new TerminalApprovalReject
            {
                UserAgent = CommonBase.useragent,
                UserIp= CommonBase.userip,
                UserId= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                Remark = remark,
                Action = "Approve",
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
                Action = "Reject",
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
        public async Task<TerminalDeinstallationRequestViewModel> ProblematicDeInstalledToDeInstalled(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId)
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
                UserIp = CommonBase.userip,
                FromDate = terminalReq.FromDate,
                ToDate = terminalReq.ToDate,
                MerchantId = MerchantId,
                TerminalId = TerminalId,
                ZonalOfficeId = ZonalOfficeId != "0" ? ZonalOfficeId : "",
                RegionalOfficeId = RegionalOfficeId != "0" ? RegionalOfficeId : ""
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

        public async Task<string> SubmitProblematicDeinstalltoDeinstall([FromBody] TerminalDeinstallationCloseModel deInstall)
        {
            var deInstallForms = new TerminalDeinstallationCloseModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                Remark = deInstall.Remark,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjTerminalProblematicDeinstalledToDeinstalled = deInstall.ObjTerminalProblematicDeinstalledToDeinstalled
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(deInstallForms), Encoding.UTF8, "application/json");
            var deInstallResponse = await _requestService.CommonRequestService(requestContent, WebApiUrl.insertproblematicdeinstalledtodeinstalled);
            JObject deInstallResponseObj = JObject.Parse(JsonConvert.DeserializeObject(deInstallResponse).ToString());

            if (deInstallResponseObj["Status_Code"].ToString() == "200")
            {
                var deInstallResponseJarr = deInstallResponseObj["Data"].Value<JArray>();
                List<SuccessResponse> deInstalleResponseList = deInstallResponseJarr.ToObject<List<SuccessResponse>>();
                return deInstalleResponseList.First().Reason.ToString();
            }
            else
            {
                return deInstallResponseObj["Message"].ToString();
            }
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
                UserIp = CommonBase.userip,
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
        public async Task<List<ManageTerminalResponse>> GetAllStatusValue(ManageTerminalRequest entity)
        {
            var searchBody = new ManageTerminalRequest();
            if (entity.MerchantId != null)
            {
                searchBody = new ManageTerminalRequest
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    //StatusFlag = entity.StatusFlag,
                    TerminalId = "",
                    MerchantId = "",
                    DeploymentStatus = ""

                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new ManageTerminalRequest
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    //StatusFlag = -1,
                    TerminalId = "",
                    MerchantId = "",
                    DeploymentStatus = ""
                };
            }
            else
            {
                searchBody = new ManageTerminalRequest
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    TerminalId = "",
                    MerchantId = "",
                    DeploymentStatus = ""
                };
            }
            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ManageTerminalUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<ManageTerminalResponse> searchList = jarr.ToObject<List<ManageTerminalResponse>>();
            return searchList;
        }
        public async Task<ManageTerminalModel> ManageTerminal()
        {
            ManageTerminalModel custModel = new ManageTerminalModel();

            return custModel;
        }
    }
}