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
            terminalDeInstallationRequestApprovalRequestModal.ZoneMdl.AddRange(await _commonActionService.GetZonalOfficeList());
            return terminalDeInstallationRequestApprovalRequestModal;
        }
        public async Task<TerminalDeInstallationRequestApprovalWithRemark> GetTerminalsForApproval(string zonalOfcID, string regionalOfcID, string fromDate, string toDate, string merchantId, string terminalId)
        {
            TerminalDeInstallationRequestApprovalWithRemark getTerminalDeInstallationRequestApprovalReponseModals = new TerminalDeInstallationRequestApprovalWithRemark();
            var terminalDetailsForDeInstallationApprovalForms = new GetTerminalDeInstallationRequestApprovalRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FromDate = fromDate,
                ToDate = toDate,
                ZonalOfficeId = string.IsNullOrEmpty(zonalOfcID) || zonalOfcID == "0" ? "" : zonalOfcID,
                RegionalOfficeId = string.IsNullOrEmpty(regionalOfcID) || regionalOfcID == "0" ? "" : regionalOfcID,
                MerchantId = string.IsNullOrEmpty(merchantId) ? "" : merchantId,
                TerminalId = string.IsNullOrEmpty(terminalId) ? "" : terminalId
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

        public async Task<string> TerminalDeInstallRequestApprovalRejection([FromBody] TerminalDeInstallationApprovalSubmit approvalRejectionMdl)
        {
            var approvalRejectionTerminalReqForms = new TerminalDeInstallationApprovalSubmit
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                Action = approvalRejectionMdl.Action,
                Remark = approvalRejectionMdl.Remark,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                ObjTerminalDeInstallationInsertInput = approvalRejectionMdl.ObjTerminalDeInstallationInsertInput
            };

            StringContent approvalRejectionTerminalReqContent = new StringContent(JsonConvert.SerializeObject(approvalRejectionTerminalReqForms), Encoding.UTF8, "application/json");
            var approvalRejectionTerminalReqResponse = await _requestService.CommonRequestService(approvalRejectionTerminalReqContent, WebApiUrl.updateTerminalDeInstallationRequestApproval);
            JObject approvalRejectionTerminalReqObj = JObject.Parse(JsonConvert.DeserializeObject(approvalRejectionTerminalReqResponse).ToString());

            if (approvalRejectionTerminalReqObj["Status_Code"].ToString() == "200")
            {
                var approvalRejectionTerminalReqJarr = approvalRejectionTerminalReqObj["Data"].Value<JArray>();
                List<SuccessResponse> approvalRejectionTerminalReqLst = approvalRejectionTerminalReqJarr.ToObject<List<SuccessResponse>>();
                return approvalRejectionTerminalReqLst.First().Reason.ToString();
            }
            else
            {
                return approvalRejectionTerminalReqObj["Message"].ToString();
            }
        }

        public async Task<TerminalDeInstallationRequestAuthorizationRequestModal> TerminalDeInstallationRequestAuthorization()
        {
            TerminalDeInstallationRequestAuthorizationRequestModal terminalDeInstallationRequestAuthorizationRequestModal = new TerminalDeInstallationRequestAuthorizationRequestModal();
            terminalDeInstallationRequestAuthorizationRequestModal.ZoneMdl.AddRange(await _commonActionService.GetZonalOfficeList());
            return terminalDeInstallationRequestAuthorizationRequestModal;
        } 
        public async Task<TerminalDeInstallationRequestAuthorizationWithRemark> GetTerminalsForAuthorization(string zonalOfcID, string regionalOfcID, string fromDate, string toDate, string merchantId, string terminalId)
        {
            TerminalDeInstallationRequestAuthorizationWithRemark getTerminalDeInstallationRequestAuthorizationReponseModals = new TerminalDeInstallationRequestAuthorizationWithRemark();
            var terminalDetailsForDeInstallationAuthorizationForms = new GetTerminalDeInstallationRequestAuthorizationRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FromDate = fromDate,
                ToDate = toDate,
                ZonalOfficeId = string.IsNullOrEmpty(zonalOfcID) || zonalOfcID == "0" ? "" : zonalOfcID,
                RegionalOfficeId = string.IsNullOrEmpty(regionalOfcID) || regionalOfcID == "0" ? "" : regionalOfcID,
                MerchantId = string.IsNullOrEmpty(merchantId) ? "" : merchantId,
                TerminalId = string.IsNullOrEmpty(terminalId) ? "" : terminalId
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

        public async Task<string> TerminalDeInstallRequestApprovalRejectionAuth([FromBody] TerminalDeInstallationAuthorizationSubmit approvalRejectionMdl)
        {
            var approvalRejectionTerminalReqForms = new TerminalDeInstallationAuthorizationSubmit
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                Action = approvalRejectionMdl.Action,
                Remark = approvalRejectionMdl.Remark,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                ObjTerminalDeInstallationAuthorizationInput = approvalRejectionMdl.ObjTerminalDeInstallationAuthorizationInput
            };

            StringContent approvalRejectionTerminalReqContent = new StringContent(JsonConvert.SerializeObject(approvalRejectionTerminalReqForms), Encoding.UTF8, "application/json");
            var approvalRejectionTerminalReqResponse = await _requestService.CommonRequestService(approvalRejectionTerminalReqContent, WebApiUrl.updateTerminalDeInstallationRequestAuthorization);
            JObject approvalRejectionTerminalReqObj = JObject.Parse(JsonConvert.DeserializeObject(approvalRejectionTerminalReqResponse).ToString());

            if (approvalRejectionTerminalReqObj["Status_Code"].ToString() == "200")
            {
                var approvalRejectionTerminalReqJarr = approvalRejectionTerminalReqObj["Data"].Value<JArray>();
                List<SuccessResponse> approvalRejectionTerminalReqLst = approvalRejectionTerminalReqJarr.ToObject<List<SuccessResponse>>();
                return approvalRejectionTerminalReqLst.First().Reason.ToString();
            }
            else
            {
                return approvalRejectionTerminalReqObj["Message"].ToString();
            }
        }
    }
}
