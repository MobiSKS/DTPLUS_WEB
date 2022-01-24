using HPCL_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class OfficerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> EditOfficer()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditOfficer(OfficerModel ofcrMdl)
        {

            return View();
        }

        public async Task<IActionResult> EditLocation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditLocation(OfficerLocationModel OfcrLocation)
        {
            return View();
        }
    }
}
