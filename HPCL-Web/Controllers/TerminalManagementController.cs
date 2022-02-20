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
    }
}
