﻿using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.MerchantFinancials;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class MerchantFinancialsController : Controller
    {
        private readonly IMerchantFinancialService _merchantFinancialService;

        public MerchantFinancialsController(IMerchantFinancialService merchantFinancialService)
        {
            _merchantFinancialService = merchantFinancialService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ERPReloadSaleEarningDetails()
        {
            return View();
        }
        public async Task<IActionResult> MerchantAccountStatement()
        {
            return View();
        }
        public async Task<IActionResult> MerchantAccountStatementRequest()
        {
            return View();
        }
        public async Task<IActionResult> MerchantDeltaReport()
        {
            return View();
        }
        public async Task<IActionResult> ReceivablePayableDetails()
        {
            return View();
        }
        public async Task<IActionResult> ReFuelCardPaymentConfirmation()
        {
            return View();
        }
     
        public async Task<IActionResult> TransactionDetails()
        {
            return View();
        }
        public async Task<IActionResult> ViewEarningBreakUp()
        {
            return View();
        }

        public IActionResult ViewUploadMerchantCautionLimit()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ViewUploadMerchantCautionLimit(GetUploadMerchantCautionLimit entity)
        {
            var searchList = await _merchantFinancialService.ViewUploadMerchantCautionLimit(entity);
            return Json(new { searchList = searchList });
        }

        public IActionResult SettlementDetails()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SettlementDetails(GetMerchantSettlementDetails entity)
        {
            var searchList = await _merchantFinancialService.SettlementDetails(entity);
            return Json(new { searchList = searchList });
        }
        public async Task<IActionResult> BatchDetails()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetBatchDetails(string terminalId, int batchId)
        {
            var batchDetails = await _merchantFinancialService.GetBatchDetails(terminalId, batchId);
            return Json(batchDetails);
        }

        public async Task<IActionResult> TerminalDetails(string terminalId)
        {
            var terminalDetails = await _merchantFinancialService.GetTerminalDetails(terminalId);
            return View(terminalDetails);
        }
    }
}
