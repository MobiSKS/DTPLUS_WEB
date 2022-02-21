using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class TerminalManagementController : Controller
    {
        public IActionResult ManageTerminal()
        {
            return View();
        }
        public IActionResult RegenerateIac()
        {
            return View();
        }
        public IActionResult TerminalInstallationRequest()
        {
            return View();
        }
        public IActionResult UnblockTerminal()
        {
            return View();
        }

        public IActionResult TerminalInstallationRequestClose()
        {
            return View();
        }

        public IActionResult ViewTerminalDeinstallationRequestStatus()
        {
            return View();
        }

        public IActionResult ViewTerminalInstallationRequestStatus()
        {
            return View();
        }

        public IActionResult ViewTerminalMerchantMappingStatus()
        {
            return View();
        }

        public IActionResult ManageAutomationIntegrationStatus()
        {
            return View();
        }

        public IActionResult ProblematicDeInstalledToDeInstalled()
        {
            return View();
        }

        public IActionResult TerminalDeInstallationRequest()
        {
            return View();
        }

        public IActionResult TerminalDeInstallationRequestClose()
        {
            return View();
        }
    }
}
