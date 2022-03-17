using HPCL.Common.Helper;
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
    }
}
