using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.Officers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HPCL.Service.Interfaces;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class OfficerController : Controller
    {
        private readonly IOfficerServices _officerService;

        public OfficerController(IOfficerServices officerServices)
        {
            _officerService = officerServices;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(string officerType, string location, string reason)
        {
            var modals = await _officerService.Details(officerType, location);

            ViewBag.OfficerType = officerType;
            ViewBag.Location = location;
            ViewBag.AddUpdateMessage = reason;

            return View(modals);
        }
        public async Task<IActionResult> Create()
        {
            var modals = await _officerService.Create();
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> Create(OfficerModel ofcrMdl)
        {
            var reason = await _officerService.Create(ofcrMdl);
            return RedirectToAction("Details", "Officer", new { reason = reason });
        }
        public async Task<IActionResult> EditOfficer(int officerID)
        {
            var modals = await _officerService.EditOfficer(officerID);
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> EditOfficer(OfficerModel ofcrMdl)
        {
            var reason = await _officerService.EditOfficer(ofcrMdl);
            return RedirectToAction("Details", "Officer", new { reason = reason });
        }
        public async Task<IActionResult> EditLocation(int officerID)
        {
            var modals = await _officerService.EditLocation(officerID);
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> EditLocation(OfficerLocationModel ofcrLocationMdl)
        {
            var Tuple = await _officerService.EditLocation(ofcrLocationMdl);

            ViewBag.Message = Tuple.Item1;
            var modals = Tuple.Item2;

            return View(modals);
        }
        public async Task<IActionResult> Delete(string officerID)
        {
            var reason = await _officerService.Delete(officerID);
            return RedirectToAction("Details", "Officer", new { reason = reason });
        }
        public async Task<IActionResult> DeleteLocation(string userName, int zonalID, int regionalID, int officerID)
        {
            var reason = await _officerService.DeleteLocation(userName, zonalID, regionalID);
            return RedirectToAction("EditLocation", "Officer", new { OfficerID = officerID });
        }
        public async Task<IActionResult> OfficerDetails()
        {
            var modals = await _officerService.OfficerDetails();
            return View(modals);
        }
        public async Task<IActionResult> GetOfficerDetailsTable(string zonalOfcID, string regionalOfcID, string stateID, string districtID)
        {
           var modals = await _officerService.GetOfficerDetailsTable(zonalOfcID, regionalOfcID, stateID, districtID);
            return PartialView("~/Views/Officer/_OfficerDetailsTable.cshtml", modals);
        }
    }
}
