using HPCL.Common.Models.RequestModel.TerminalManagement;
using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using HPCL.Common.Models.ViewModel.Terminal;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class TerminalManagementController : Controller
    {

        private readonly ITerminalManagement _TerminalService;
        private readonly ICommonActionService _commonActionService;

        public TerminalManagementController(ITerminalManagement ViewServices, ICommonActionService commonActionService)
        {
            _TerminalService = ViewServices;
            _commonActionService = commonActionService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetAllStatusValue(ManageTerminalRequest entity)
        {
            var searchList = await _TerminalService.GetAllStatusValue(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        public async Task<IActionResult> ManageTerminal()
        {
            ManageTerminalModel MangeMdl = new ManageTerminalModel();
            MangeMdl = await _TerminalService.ManageTerminal();

            return View(MangeMdl);
        }
        public async Task<IActionResult> RegenerateIac()
        {
            return View();
        }
        public async Task<IActionResult> TerminalInstallationRequest()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> TerminalInstallationRequest(TerminalManagement entity)
        {
            var searchList = await _TerminalService.TerminalInstallationRequest(entity);

            //var objMerchantList = Tuple.Item1;
            //var searchList = Tuple.Item2;
            //var resMsg = Tuple.Item3;

            ModelState.Clear();
            return Json(new
            {
                //objMerchantList = objMerchantList,
                searchList = searchList
            });
        }

        [HttpPost]
        public async Task<JsonResult> AddJustification(string objInsertTerminal)
        {
            var reason = await _TerminalService.AddJustification(objInsertTerminal);

            ModelState.Clear();
            return Json(reason);
        }

        public async Task<IActionResult> UnblockTerminal()
        {
            return View();
        }
        public async Task<IActionResult> TerminalInstallationRequestClose()
        {
            TerminalManagementRequestViewModel terminalManagementRequestViewModel = new TerminalManagementRequestViewModel();
            var ZonalOfficeslist = await _commonActionService.GetZonalOfficeList();
            terminalManagementRequestViewModel.ZonalOffices.AddRange(ZonalOfficeslist);
            return View(terminalManagementRequestViewModel);
        }
        public async Task<IActionResult> SearchTerminalInstallationRequestClose(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId)
        {
            var modals = await _TerminalService.TerminalInstallationRequestClose(ZonalOfficeId, RegionalOfficeId, FromDate, ToDate, MerchantId, TerminalId);
            return PartialView("~/Views/TerminalManagement/_TerminalInstallationRequestClosetbl.cshtml", modals);
        }
        [HttpPost]
        public async Task<IActionResult> SubmitTerminalRequestClose([FromBody] TerminalManagementRequestModel TerminalManagementRequestModel)
        {
            var result = await _TerminalService.SubmitTerminalRequestClose(TerminalManagementRequestModel);
            return Json(result);
        }

        public async Task<IActionResult> ViewTerminalDeinstallationRequestStatus()
        {
            TerminalDeinstallationRequestViewModel terminalDeinstallationRequestViewModel = new TerminalDeinstallationRequestViewModel();
            var ZonalOfficeslist = await _commonActionService.GetZonalOfficeList();
            terminalDeinstallationRequestViewModel.ZonalOffices.AddRange(ZonalOfficeslist);
            return View(terminalDeinstallationRequestViewModel);
        }

        
        public async Task<IActionResult> SearchTerminalDeinstallationRequestStatus(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId)
        {
            var modals = await _TerminalService.ViewTerminalDeinstallationRequestStatus(ZonalOfficeId, RegionalOfficeId, FromDate, ToDate, MerchantId, TerminalId);
            return PartialView("~/Views/TerminalManagement/_TerminalDeinstallationStatusTbl.cshtml", modals);
        }

        public async Task<IActionResult> ViewTerminalInstallationRequestStatus()
        {
            TerminalManagementRequestViewModel terminalManagementRequestViewModel = new TerminalManagementRequestViewModel();
            var ZonalOfficeslist = await _commonActionService.GetZonalOfficeList();
            terminalManagementRequestViewModel.ZonalOffices.AddRange(ZonalOfficeslist);
            return View(terminalManagementRequestViewModel);
        }
        
        public async Task<IActionResult> SeacrhTerminalInstallationRequestStatus(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId)
        {
            var modals = await _TerminalService.ViewTerminalInstallationRequestStatus(ZonalOfficeId, RegionalOfficeId, FromDate, ToDate, MerchantId, TerminalId);
            return PartialView("~/Views/TerminalManagement/_TerminalInstallationStatusTbl.cshtml", modals);
        }

        public async Task<IActionResult> ViewTerminalMerchantMappingStatus()
        {
            return View();
        }

        public async Task<IActionResult> ManageAutomationIntegrationStatus()
        {
            return View();
        }
        public async Task<IActionResult> ProblematicDeInstalledToDeInstalled()
        {
            TerminalDeinstallationRequestViewModel terminalDeinstallationRequestViewModel=new TerminalDeinstallationRequestViewModel();
            var ZonalOfficeslist = await _commonActionService.GetZonalOfficeList();
            terminalDeinstallationRequestViewModel.ZonalOffices.AddRange(ZonalOfficeslist);
            return View(terminalDeinstallationRequestViewModel);
        }
        
        public async Task<IActionResult> SearchProblematicDeInstalledToDeInstalled(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId)
        {
            
            var modals = await _TerminalService.ProblematicDeInstalledToDeInstalled(ZonalOfficeId, RegionalOfficeId, FromDate, ToDate, MerchantId, TerminalId);
            return PartialView("~/Views/TerminalManagement/_ProblematicDeinstalledTbl.cshtml", modals);
        }
        [HttpPost]
        public async Task<IActionResult> SubmitProblematicDeinstalltoDeinstall([FromBody] TerminalDeinstallationCloseModel deInstall)
        {
            var result = await _TerminalService.SubmitProblematicDeinstalltoDeinstall(deInstall);
            return Json(result);
        }

        public async Task<IActionResult> TerminalDeInstallationRequest(TerminalDeinstallationRequestViewModel terminalReq)
        {
            var modals = await _TerminalService.TerminalDeInstallationRequest(terminalReq);
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> SubmitDeinstallRequest([FromBody] TerminalDeinstallationRequestUpdateModel TerminalDeinstallationRequestUpdate)
        {
            var result = await _TerminalService.SubmitDeinstallRequest(TerminalDeinstallationRequestUpdate);
            return Json(result);
        }
        public async Task<IActionResult> TerminalDeInstallationRequestClose()
        {
            TerminalDeinstallationRequestViewModel terminalDeinstallationRequestViewModel = new TerminalDeinstallationRequestViewModel();
            var ZonalOfficeslist = await _commonActionService.GetZonalOfficeList();
            terminalDeinstallationRequestViewModel.ZonalOffices.AddRange(ZonalOfficeslist);
            return View(terminalDeinstallationRequestViewModel);
        }
        
        public async Task<IActionResult> SearchTerminalDeInstallationRequestClose(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId)
        {
            
               var modals = await _TerminalService.TerminalDeInstallationRequestClose(ZonalOfficeId, RegionalOfficeId, FromDate, ToDate, MerchantId, TerminalId);
            return PartialView("~/Views/TerminalManagement/_TerminalDeinstallationCloseTbl.cshtml", modals);
        }
        [HttpPost]
        public async Task<IActionResult> SubmitDeinstallationRequestClose([FromBody] TerminalDeinstallationCloseModel TerminalDeinstallationClose)
        {
            var result = await _TerminalService.SubmitDeinstallationRequestClose(TerminalDeinstallationClose);
            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> TerminalInstallationRequestApproval(TerminalApprovalReq entity)
        {
            var approvalList = await _TerminalService.GetTerminalInstallationReqApproval(entity);
            return Json(new { approvalList = approvalList });
        }

        [HttpPost]
        public async Task<JsonResult> TerminalInstallationRequestApprovalClick(string ObjMerchantTerminalInsertInput, string remark)
        {
            var reason = await _TerminalService.DoApprovalTerminal(ObjMerchantTerminalInsertInput, remark);
            return Json(reason);
        }

        [HttpPost]
        public async Task<JsonResult> TerminalInstallationRequestRejectClick(string ObjMerchantTerminalInsertInput, string remark)
        {
            var reason = await _TerminalService.DoRejectTerminal(ObjMerchantTerminalInsertInput, remark);
            return Json(reason);
        }
        public IActionResult TerminalInstallationRequestApproval()
        {
            return View();
        }

        public IActionResult Approval()
        {
            return View();
        }

        #region Search Terminal
        public async Task<IActionResult> SearchTerminal()
        {
            var modals = await _TerminalService.SearchTerminal();
            return View(modals);
        }

        public async Task<IActionResult> SearchTerminalDetails(string terminalId, string merchantId, string terminalType, string issueDate)
        {
            var modals = await _TerminalService.SearchTerminalDetails(terminalId, merchantId, terminalType, issueDate);
            return PartialView("~/Views/TerminalManagement/_SearchResultForTerminalTable.cshtml", modals);
        }
        #endregion
    }
}
