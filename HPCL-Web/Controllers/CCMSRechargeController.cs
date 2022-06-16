﻿using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.CCMSRecharge;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class CCMSRechargeController : Controller
    {
        private readonly ICCMSRechargeService _cCMSRechargeService;

        public CCMSRechargeController(ICCMSRechargeService cCMSRechargeService)
        {
            _cCMSRechargeService = cCMSRechargeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CCMSRechargePage()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CCMSRechargePage(string mobNo, string customerId)
        {
            var searchList = await _cCMSRechargeService.GetDetailsByMObNoCust(mobNo, customerId);

            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> RedirectToPG(string customerId, string mobNo, string controlCardNo, string amount)
        {
            var redirectDetails = await _cCMSRechargeService.RedirectToPG(customerId, mobNo, controlCardNo, amount);

            return Json(new { redirectDetails = redirectDetails });
        }

        public IActionResult RedirectToPaymentGateway(string inputTxtValues)
        {
            RedirectToPGResponseData arrs = JsonConvert.DeserializeObject<RedirectToPGResponseData>(inputTxtValues);

            HttpContext.Session.SetString("pgurl",arrs.response.apiurl);
            HttpContext.Session.SetString("reqhash", arrs.response.request_Hash);
            HttpContext.Session.SetString("accesscode", arrs.response.accessCode);

            return View(arrs);
        }
    }
}
