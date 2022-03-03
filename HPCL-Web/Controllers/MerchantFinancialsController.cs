using HPCL.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class MerchantFinancialsController : Controller
    {
        public async Task<IActionResult> BatchDetails()
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
        public async Task<IActionResult> SettlementDetails()
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

    }
}
