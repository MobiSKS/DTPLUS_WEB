using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.Approvals;
using HPCL.Common.Models.ResponseModel.Approvals;
using HPCL.Common.Models.ViewModel.Approvals;
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
    public class ApprovalService : IApprovalService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;

        public ApprovalService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }

        public async Task<TerminalDeInstallationRequestApprovalRequestModal> TerminalDeInstallationRequestApproval()
        {
            TerminalDeInstallationRequestApprovalRequestModal terminalDeInstallationRequestApprovalRequestModal = new TerminalDeInstallationRequestApprovalRequestModal();
            terminalDeInstallationRequestApprovalRequestModal.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
            terminalDeInstallationRequestApprovalRequestModal.ZoneMdl.AddRange(await _commonActionService.GetZonalOfficeListbySBUtype("1"));
            return terminalDeInstallationRequestApprovalRequestModal;
        }
        public async Task<TerminalDeInstallationRequestApprovalWithRemark> GetTerminalsForApproval(string zonalOfcID, string regionalOfcID, string fromDate, string toDate, string merchantId, string terminalId, string SBUTypeId)
        {
            TerminalDeInstallationRequestApprovalWithRemark getTerminalDeInstallationRequestApprovalReponseModals = new TerminalDeInstallationRequestApprovalWithRemark();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(fromDate))
            {
                fromDate = await _commonActionService.changeDateFormat(fromDate);
                toDate = await _commonActionService.changeDateFormat(toDate);
            }
            else
            {
                fromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                toDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var terminalDetailsForDeInstallationApprovalForms = new GetTerminalDeInstallationRequestApprovalRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FromDate = fromDate,
                ToDate = toDate,
                ZonalOfficeId = string.IsNullOrEmpty(zonalOfcID) || zonalOfcID == "0" ? "" : zonalOfcID,
                RegionalOfficeId = string.IsNullOrEmpty(regionalOfcID) || regionalOfcID == "0" ? "" : regionalOfcID,
                MerchantId = string.IsNullOrEmpty(merchantId) ? "" : merchantId,
                TerminalId = string.IsNullOrEmpty(terminalId) ? "" : terminalId,
                SBUTypeId=SBUTypeId
            };

            StringContent terminalDetailsForDeInstallationApprovalContent = new StringContent(JsonConvert.SerializeObject(terminalDetailsForDeInstallationApprovalForms), Encoding.UTF8, "application/json");

            var terminalDetailsForDeInstallationApprovalResponse = await _requestService.CommonRequestService(terminalDetailsForDeInstallationApprovalContent, WebApiUrl.getTerminalDeInstallationRequestApproval);

            JObject terminalDetailsForDeInstallationApprovalObj = JObject.Parse(JsonConvert.DeserializeObject(terminalDetailsForDeInstallationApprovalResponse).ToString());
            getTerminalDeInstallationRequestApprovalReponseModals = JsonConvert.DeserializeObject<TerminalDeInstallationRequestApprovalWithRemark>(terminalDetailsForDeInstallationApprovalResponse);
            var terminalDetailsForDeInstallationApprovalJarr = terminalDetailsForDeInstallationApprovalObj["Data"].Value<JArray>();
            List<GetTerminalDeInstallationRequestApprovalReponseModal> getTerminalDeInstallationRequestApprovalReponselst = terminalDetailsForDeInstallationApprovalJarr.ToObject<List<GetTerminalDeInstallationRequestApprovalReponseModal>>();
            getTerminalDeInstallationRequestApprovalReponseModals.TerminalDeInstallationRequestApprovalTbl.AddRange(getTerminalDeInstallationRequestApprovalReponselst);
            return getTerminalDeInstallationRequestApprovalReponseModals;
        }

        public async Task<List<string>> TerminalDeInstallRequestApprovalRejection([FromBody] TerminalDeInstallationApprovalSubmit approvalRejectionMdl)
        {
            var approvalRejectionTerminalReqForms = new TerminalDeInstallationApprovalSubmit
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                Action = approvalRejectionMdl.Action,
                Remark = approvalRejectionMdl.Remark,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjTerminalDeInstallationInsertInput = approvalRejectionMdl.ObjTerminalDeInstallationInsertInput
            };

            StringContent approvalRejectionTerminalReqContent = new StringContent(JsonConvert.SerializeObject(approvalRejectionTerminalReqForms), Encoding.UTF8, "application/json");
            var approvalRejectionTerminalReqResponse = await _requestService.CommonRequestService(approvalRejectionTerminalReqContent, WebApiUrl.updateTerminalDeInstallationRequestApproval);
            JObject approvalRejectionTerminalReqObj = JObject.Parse(JsonConvert.DeserializeObject(approvalRejectionTerminalReqResponse).ToString());


            var approvalRejectionTerminalReqJarr = approvalRejectionTerminalReqObj["Data"].Value<JArray>();
            List<SuccessResponse> approvalRejectionTerminalReqLst = approvalRejectionTerminalReqJarr.ToObject<List<SuccessResponse>>();
            List<string> MessageList = new List<string>();
            MessageList.Add(Convert.ToString(approvalRejectionTerminalReqLst[0].Status));
            foreach (var approvalRejectionTerminalReq in approvalRejectionTerminalReqLst)
            {
                MessageList.Add(approvalRejectionTerminalReq.Reason);
            }
            return MessageList;

        }

        public async Task<TerminalDeInstallationRequestAuthorizationRequestModal> TerminalDeInstallationRequestAuthorization()
        {
            TerminalDeInstallationRequestAuthorizationRequestModal terminalDeInstallationRequestAuthorizationRequestModal = new TerminalDeInstallationRequestAuthorizationRequestModal();
            terminalDeInstallationRequestAuthorizationRequestModal.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
            terminalDeInstallationRequestAuthorizationRequestModal.ZoneMdl.AddRange(await _commonActionService.GetZonalOfficeListbySBUtype("1"));
            return terminalDeInstallationRequestAuthorizationRequestModal;
        }
        public async Task<TerminalDeInstallationRequestAuthorizationWithRemark> GetTerminalsForAuthorization(string zonalOfcID, string regionalOfcID, string fromDate, string toDate, string merchantId, string terminalId, string SBUTypeId)
        {
            TerminalDeInstallationRequestAuthorizationWithRemark getTerminalDeInstallationRequestAuthorizationReponseModals = new TerminalDeInstallationRequestAuthorizationWithRemark();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(fromDate))
            {
                fromDate = await _commonActionService.changeDateFormat(fromDate);
                toDate = await _commonActionService.changeDateFormat(toDate);
            }
            else
            {
                fromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                toDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var terminalDetailsForDeInstallationAuthorizationForms = new GetTerminalDeInstallationRequestAuthorizationRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FromDate = fromDate,
                ToDate = toDate,
                ZonalOfficeId = string.IsNullOrEmpty(zonalOfcID) || zonalOfcID == "0" ? "" : zonalOfcID,
                RegionalOfficeId = string.IsNullOrEmpty(regionalOfcID) || regionalOfcID == "0" ? "" : regionalOfcID,
                MerchantId = string.IsNullOrEmpty(merchantId) ? "" : merchantId,
                TerminalId = string.IsNullOrEmpty(terminalId) ? "" : terminalId,
                SBUTypeId=SBUTypeId
            };

            StringContent terminalDetailsForDeInstallationAuthorizationContent = new StringContent(JsonConvert.SerializeObject(terminalDetailsForDeInstallationAuthorizationForms), Encoding.UTF8, "application/json");

            var terminalDetailsForDeInstallationAuthorizationResponse = await _requestService.CommonRequestService(terminalDetailsForDeInstallationAuthorizationContent, WebApiUrl.getTerminalDeInstallationRequestAuthorization);
            getTerminalDeInstallationRequestAuthorizationReponseModals = JsonConvert.DeserializeObject<TerminalDeInstallationRequestAuthorizationWithRemark>(terminalDetailsForDeInstallationAuthorizationResponse);
            JObject terminalDetailsForDeInstallationAuthorizationObj = JObject.Parse(JsonConvert.DeserializeObject(terminalDetailsForDeInstallationAuthorizationResponse).ToString());
            var terminalDetailsForDeInstallationAuthorizationJarr = terminalDetailsForDeInstallationAuthorizationObj["Data"].Value<JArray>();
            List<GetTerminalDeInstallationRequestAuthorizationReponseModal> terminalDetailsForDeInstallationAuthorizationReponselst = terminalDetailsForDeInstallationAuthorizationJarr.ToObject<List<GetTerminalDeInstallationRequestAuthorizationReponseModal>>();
            getTerminalDeInstallationRequestAuthorizationReponseModals.TerminalDeInstallationRequestAuthorizationTbl.AddRange(terminalDetailsForDeInstallationAuthorizationReponselst);
            return getTerminalDeInstallationRequestAuthorizationReponseModals;
        }

        public async Task<List<string>> TerminalDeInstallRequestApprovalRejectionAuth([FromBody] TerminalDeInstallationAuthorizationSubmit approvalRejectionMdl)
        {
            var approvalRejectionTerminalReqForms = new TerminalDeInstallationAuthorizationSubmit
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                Action = approvalRejectionMdl.Action,
                Remark = approvalRejectionMdl.Remark,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjTerminalDeInstallationAuthorizationInput = approvalRejectionMdl.ObjTerminalDeInstallationAuthorizationInput
            };

            StringContent approvalRejectionTerminalReqContent = new StringContent(JsonConvert.SerializeObject(approvalRejectionTerminalReqForms), Encoding.UTF8, "application/json");
            var approvalRejectionTerminalReqResponse = await _requestService.CommonRequestService(approvalRejectionTerminalReqContent, WebApiUrl.updateTerminalDeInstallationRequestAuthorization);
            JObject approvalRejectionTerminalReqObj = JObject.Parse(JsonConvert.DeserializeObject(approvalRejectionTerminalReqResponse).ToString());


            var approvalRejectionTerminalReqJarr = approvalRejectionTerminalReqObj["Data"].Value<JArray>();
            List<SuccessResponse> approvalRejectionTerminalReqLst = approvalRejectionTerminalReqJarr.ToObject<List<SuccessResponse>>();
            List<string> MessageList = new List<string>();
            MessageList.Add(Convert.ToString(approvalRejectionTerminalReqLst[0].Status));

            foreach (var approvalRejectionTerminalReq in approvalRejectionTerminalReqLst)
            {
                MessageList.Add(approvalRejectionTerminalReq.Reason);
            }
            return MessageList;
        }
    }
}
