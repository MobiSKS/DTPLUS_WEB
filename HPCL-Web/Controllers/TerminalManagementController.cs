﻿using HPCL.Common.Models.RequestModel.TerminalManagement;
using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPCL.Common.Models.ViewModel.Terminal;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class TerminalManagementController : Controller
    {

        private readonly ITerminalManagement _TerminalService;

        public TerminalManagementController(ITerminalManagement ViewServices)
        {
            _TerminalService = ViewServices;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> ManageTerminal()
        {
            return View();
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
            var Tuple = await _TerminalService.TerminalInstallationRequest(entity);

            var objMerchantList = Tuple.Item1;
            var searchList = Tuple.Item2;


            ModelState.Clear();
            return Json(new
            {
                objMerchantList = objMerchantList,
                searchList = searchList,

            });
        }

        [HttpPost]
        public async Task<JsonResult> AddJustification(TerminalManagement entity)
        {
            var reason = await _TerminalService.AddJustification(entity);

            ModelState.Clear();
            return Json(reason);
        }
        public async Task<IActionResult> UnblockTerminal()
        {
            return View();
        }

        public async Task<IActionResult> TerminalInstallationRequestClose(TerminalManagementRequestViewModel terminalReq)
        {
            var modals = await _TerminalService.TerminalInstallationRequestClose(terminalReq);
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> SubmitTerminalRequestClose([FromBody] TerminalManagementRequestModel TerminalManagementRequestModel)
        {
            var result = await _TerminalService.SubmitTerminalRequestClose(TerminalManagementRequestModel);
            return Json(result);
        }


        public async Task<IActionResult> ViewTerminalDeinstallationRequestStatus(TerminalDeinstallationRequestViewModel terminalReq)
        {
            var modals = await _TerminalService.ViewTerminalDeinstallationRequestStatus(terminalReq);
            return View(modals);
        }


        public async Task<IActionResult> ViewTerminalInstallationRequestStatus(TerminalManagementRequestViewModel terminalReq)
        {
            var modals = await _TerminalService.ViewTerminalInstallationRequestStatus(terminalReq);
            return View(modals);
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
            return View();
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

        public async Task<IActionResult> TerminalDeInstallationRequestClose(TerminalDeinstallationRequestViewModel terminalReq)
        {
            var modals = await _TerminalService.TerminalDeInstallationRequestClose(terminalReq);
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> SubmitDeinstallationRequestClose([FromBody] TerminalDeinstallationCloseModel TerminalDeinstallationClose)
        {
            var result = await _TerminalService.SubmitDeinstallationRequestClose(TerminalDeinstallationClose);
            return Json(result);
        }
        
    }
}
