﻿using HPCL.Common.Models.ViewModel.HDFCBankCreditPouch;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class HDFCBankCreditPouchController : Controller
    {
        private readonly IHDFCBankCreditPouchService _hDFCBankCreditPouchService;

        public HDFCBankCreditPouchController(IHDFCBankCreditPouchService hDFCBankCreditPouchService)
        {
            _hDFCBankCreditPouchService = hDFCBankCreditPouchService;
        }
        public IActionResult Index()
        {
            return View();  
        }

        public IActionResult ExceptionRequestToAddCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ExceptionRequestToAddCustomer(CustomerDetailsReq entity)
        {
            var searchList = await _hDFCBankCreditPouchService.GetCustomerDetails(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> CheckPlan(string amount)
        {
            var searchList = await _hDFCBankCreditPouchService.GetPlan(amount);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> EnrollExceptionRequest(EnrollExceptionRequest entity)
        {
            var reasonList = await _hDFCBankCreditPouchService.InsertExceptionRequest(entity);
            ModelState.Clear();
            return Json(new { reasonList = reasonList });
        }

        public IActionResult RequestApproval()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> RequestApproval(SearchRequestApprovalClone entity)
        {
            var searchList = await _hDFCBankCreditPouchService.SearchRequestApproval(entity);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }
    }
}
