using HPCL.Common.Helper;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class ApprovalsController : Controller
    {
        private readonly IApprovalService _approvalServices;
        private readonly ICommonActionService _commonActionService;

        public ApprovalsController(IApprovalService approvalServices, ICommonActionService commonActionService)
        {
            _approvalServices = approvalServices;
            _commonActionService = commonActionService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> TerminalDeInstallationRequestApproval()
        {
            var modals = await _approvalServices.TerminalDeInstallationRequestApproval();
            return View(modals);
        }
        public async Task<IActionResult> GetTerminalsForApproval(string zonalOfcID, string regionalOfcID, string fromDate, string toDate, string merchantId, string terminalId)
        {
            var modals = await _approvalServices.GetTerminalsForApproval(zonalOfcID, regionalOfcID, fromDate, toDate, merchantId, terminalId);
            return PartialView("~/Views/Approvals/_TerminalsForApprovalTable.cshtml", modals);
        }
    }
}
