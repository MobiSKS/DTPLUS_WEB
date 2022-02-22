using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class TerminalManagementController : Controller
    {
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
        public async Task<IActionResult> UnblockTerminal()
        {
            return View();
        }

        public async Task<IActionResult> TerminalInstallationRequestClose()
        {
            return View();
        }

        public async Task<IActionResult> ViewTerminalDeinstallationRequestStatus()
        {
            return View();
        }

        public async Task<IActionResult> ViewTerminalInstallationRequestStatus()
        {
            return View();
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

        public async Task<IActionResult> TerminalDeInstallationRequest()
        {
            return View();
        }

        public async Task<IActionResult> TerminalDeInstallationRequestClose()
        {
            return View();
        }
    }
}
