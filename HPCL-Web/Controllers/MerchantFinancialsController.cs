using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.MerchantFinancial;
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
            MerchantDeltaReportModel merchantDeltaReportModel = new MerchantDeltaReportModel();
            return View(merchantDeltaReportModel);
        }
        public async Task<IActionResult> ReFuelCardPaymentConfirmation()
        {
            return View();
        }
     
        public async Task<IActionResult> ViewEarningBreakUp()
        {
            return View();
        }

        public IActionResult ViewUploadMerchantCautionLimit()
        {
            GetUploadMerchantCautionLimit getUploadMerchantCautionLimit=new GetUploadMerchantCautionLimit();
            return View(getUploadMerchantCautionLimit);
        }

        [HttpPost]
        public async Task<JsonResult> ViewUploadMerchantCautionLimit(GetUploadMerchantCautionLimit entity)
        {
            var searchList = await _merchantFinancialService.ViewUploadMerchantCautionLimit(entity);
            return Json(new { searchList = searchList });
        }

        public IActionResult SettlementDetails()
        {
            GetMerchantSettlementDetails getMerchantSettlementDetails = new GetMerchantSettlementDetails();
            return View(getMerchantSettlementDetails);
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

        public IActionResult TransactionDetails()
        {
            GetTransactionDetails getTransactionDetails = new GetTransactionDetails();
            return View(getTransactionDetails);
        }

        [HttpPost]
        public async Task<JsonResult> TransactionDetails(GetTransactionDetails entity)
        {
            var searchList = await _merchantFinancialService.GetTransactionlDetails(entity);
            return Json(new { searchList = searchList });
        }

        public async Task<IActionResult> SettlementBatchDetails(string terminalId, int batchId)
        {
            ViewBag.TerminalId = terminalId;
            ViewBag.BatchId = batchId;

            var batchDetails = await _merchantFinancialService.GetBatchDetails(terminalId, batchId);
            return View(batchDetails);
        }
        [HttpPost]
        public async Task<JsonResult> GetMerchantDeltaReport(string MerchantId, string TerminalId,string FromDate,string ToDate)
        {
            var batchDetails = await _merchantFinancialService.GetMerchantDeltaReport(MerchantId, TerminalId,FromDate,ToDate);
            return Json(batchDetails);
        }

        public async Task<IActionResult> ERPReloadSaleEarningDetails(MerchantERPReloadSaleEarningModel Model)
        {
            var modals = await _merchantFinancialService.ERPReloadSaleEarningDetails(Model);
            return View(modals);
        }
        public async Task<IActionResult> ReceivablePayableDetails(MerchantReceivablePayableModel Model)
        {
            var modals = await _merchantFinancialService.ReceivablePayableDetails(Model);
            return View(modals);
        }
    }
}
