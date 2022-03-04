using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.Approvals;
using HPCL.Common.Models.ResponseModel.Approvals;
using HPCL.Common.Models.ViewModel.Approvals;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        public async Task<List<GetTerminalDeInstallationRequestApprovalReponseModal>> GetTerminalsForApproval(string zonalOfcID, string regionalOfcID, string fromDate, string toDate, string merchantId, string terminalId)
        {
            List<GetTerminalDeInstallationRequestApprovalReponseModal> getTerminalDeInstallationRequestApprovalReponseModals = new List<GetTerminalDeInstallationRequestApprovalReponseModal>();

            string fDate = "", tDate = "";

            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                string[] fromDateArr = fromDate.Split("-");
                string[] toDateArr = toDate.Split("-");

                fDate = fromDateArr[2] + "-" + fromDateArr[1] + "-" + fromDateArr[0];
                tDate = toDateArr[2] + "-" + toDateArr[1] + "-" + toDateArr[0];
            }
            else
            {
                return getTerminalDeInstallationRequestApprovalReponseModals;
            }

            var terminalDetailsForDeInstallationApprovalForms = new GetTerminalDeInstallationRequestApprovalRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FromDate = fDate,
                ToDate = tDate,
                ZonalOfficeId = string.IsNullOrEmpty(zonalOfcID) ? "" : zonalOfcID,
                RegionalOfficeId = string.IsNullOrEmpty(regionalOfcID) ? "" : regionalOfcID,
                MerchantId = merchantId,
                TerminalId = terminalId
            };

            StringContent terminalDetailsForDeInstallationApprovalContent = new StringContent(JsonConvert.SerializeObject(terminalDetailsForDeInstallationApprovalForms), Encoding.UTF8, "application/json");

            var terminalDetailsForDeInstallationApprovalResponse = await _requestService.CommonRequestService(terminalDetailsForDeInstallationApprovalContent, WebApiUrl.getMerchantByMerchantID);

            JObject terminalDetailsForDeInstallationApprovalObj = JObject.Parse(JsonConvert.DeserializeObject(terminalDetailsForDeInstallationApprovalResponse).ToString());
            var terminalDetailsForDeInstallationApprovalJarr = terminalDetailsForDeInstallationApprovalObj["Data"].Value<JArray>();
            List<GetTerminalDeInstallationRequestApprovalReponseModal> terminalDetailsForDeInstallationApprovalLst = terminalDetailsForDeInstallationApprovalJarr.ToObject<List<GetTerminalDeInstallationRequestApprovalReponseModal>>();

            return getTerminalDeInstallationRequestApprovalReponseModals;
        }
    }
}
