using ClosedXML.Excel;
using ClosedXML.Extensions;
using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.Dealer;
using HPCL.Common.Models.ViewModel.Dealer;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class DealerController : Controller
    {

        private readonly IDealerService _dealerService;
        private readonly ICommonActionService _commonActionService;
        public DealerController(IDealerService dealerService, ICommonActionService commonActionService)
        {
            _dealerService = dealerService;
            _commonActionService = commonActionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DealerCreditMapping(DealerCreditMappingViewModel entity, string reset, string CustomerID, string success, string error)
        {
            if (!string.IsNullOrEmpty(CustomerID))
                entity.CustomerID = CustomerID;
            DealerCreditMappingViewModel response = new DealerCreditMappingViewModel();
            if (entity.CustomerID != null)
            {
                response = await _dealerService.GetDealerCreditMappingDetails(entity.CustomerID);
                response.CustomerID = entity.CustomerID;
            }
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            ViewBag.SuccessMessage = success;
            ViewBag.ErrorMessage = error;
            ViewBag.CustomerID = String.IsNullOrEmpty(entity.CustomerID) ? "0" : entity.CustomerID;
            return View(response);
        }

        public IActionResult MapDealerForCreditSale(string CustomerID, string NoofEntries)
        {
            ViewBag.CustomerID = CustomerID;
            DealerForCreditSaleViewModel dealerForCreditSaleViewModel = new DealerForCreditSaleViewModel();
            dealerForCreditSaleViewModel.CustomerID = CustomerID;
            dealerForCreditSaleViewModel.NoofEntries = NoofEntries;
            return View(dealerForCreditSaleViewModel);
        }
        [HttpPost]
        public async Task<JsonResult> SaveDealerForCreditSale([FromBody] DealerForCreditSaleViewModel dealerForCreditSaleViewModel)
        {
            var result = await _dealerService.SaveDealerForCreditSale(dealerForCreditSaleViewModel);
            return Json(result);
        }
        public async Task<IActionResult> MerchantMapEnableorDisable(string customerID, string merchantID, string actionID)
        {
            var result = await _dealerService.MerchantMapEnableorDisable(customerID, merchantID, actionID);
            string succesMsg = "", errorMsg = "";
            if (result[0].Status == 1)
                succesMsg = result[0].Reason;
            else if (result[0].Status == 0)
                errorMsg = result[0].Reason;
            return RedirectToAction("DealerCreditMapping", "Dealer", new { CustomerID = customerID, success = succesMsg, error = errorMsg });
        }
        [HttpPost]
        public async Task<JsonResult> UpdateDealerCreditmapping([FromBody] DealerRequestModel DealerRequestModel)
        {
            var result = await _dealerService.UpdateDealerCreditmapping(DealerRequestModel);
            return Json(result);
        }
        public IActionResult DealerCreditPaymentInBulk(string CustomerID)
        {
            ViewBag.CustomerID = CustomerID;
            DealerForCreditSaleViewModel dealerForCreditSaleViewModel = new DealerForCreditSaleViewModel();
            dealerForCreditSaleViewModel.CustomerID = CustomerID;
            return View(dealerForCreditSaleViewModel);
        }
        public IActionResult UpdateDealerCreditLimit(string CustomerID)
        {
            ViewBag.CustomerID = CustomerID;
            DealerForCreditSaleViewModel dealerForCreditSaleViewModel = new DealerForCreditSaleViewModel();
            dealerForCreditSaleViewModel.CustomerID = CustomerID;
            return View(dealerForCreditSaleViewModel);
        }
        public async Task<IActionResult> GetCreditSaleStatement(string CustomerID, string MerchantID, string FromDate, string ToDate)
        {
            var searchList = await _dealerService.GetCreditSaleStatement(CustomerID, MerchantID, FromDate, ToDate);
            return PartialView("~/Views/Dealer/_DealerCreditSaleStatementTbl.cshtml", searchList);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateDealerCreditPaymentBulk([FromBody] DealerRequestModel DealerRequestModel)
        {
            var result = await _dealerService.UpdateDealerCreditPaymentBulk(DealerRequestModel);
            return Json(result);
        }
        public async Task<JsonResult> GetDealerCreditPaymentBulk(string CustomerId)
        {
            var result = await _dealerService.GetDealerCreditPaymentBulk(CustomerId);
            return Json(result);
        }
        private ClosedXML.Excel.XLWorkbook GenerateClosedXMLWorkbook(List<LimitTypeModal> sortedtList)
        {
            var wb = new ClosedXML.Excel.XLWorkbook();
            var ws = wb.AddWorksheet("Sheet1");
            ws.Cell("A1").Value = "Merchant ID";
            ws.Cell("B1").Value = "Limit Type";
            ws.Cell("C1").Value = "Limit Amount";

            IXLCell c = ws.Cell(1, 1);
            c.Style.Font.Bold = true;
            IXLCell c1 = ws.Cell(1, 2);
            c1.Style.Font.Bold = true;
            IXLCell c2 = ws.Cell(1, 3);
            c2.Style.Font.Bold = true;

            int i = 999;
            foreach (LimitTypeModal item in sortedtList)
            {
                i = i + 1;
                ws.Cell("Z" + i.ToString()).Value = item.Description;
            }
            ws.Range("B2:B1000").SetDataValidation().List(ws.Range("z1000:z" + i.ToString()), true);
            return wb;
        }

        public async Task<ActionResult> Download()
        {
            List<LimitTypeModal> sortedtList = new List<LimitTypeModal>();
            sortedtList = await _commonActionService.GetCreditCloseLimitType();

            using (var wb = GenerateClosedXMLWorkbook(sortedtList))
            {
                // Add ClosedXML.Extensions in your using declarations
                // or specify the content type:
                return wb.Deliver("UpdateDealerCreditLimit.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
        public async Task<IActionResult> DealerCreditSaleView()
        {
            DealerCreditSaleViewModel dealerCreditSaleViewModel = new DealerCreditSaleViewModel();
            return View(dealerCreditSaleViewModel);
        }
        public async Task<IActionResult> GetDealerCreditSaleView(string CustomerID, string MerchantID, string FromDate, string ToDate)
        {
            var modals = await _dealerService.GetDealerCreditSaleView(CustomerID, MerchantID, FromDate, ToDate);
            return PartialView("~/Views/Dealer/_DealerCreditSaleViewtbl.cshtml", modals);
        }
        public async Task<IActionResult> MerchantDealerCreditSaleView()
        {
            DealerCreditSaleViewModel dealerCreditSaleViewModel = new DealerCreditSaleViewModel();
            return View(dealerCreditSaleViewModel);
        }
        public async Task<IActionResult> GetMerchantDealerCreditSaleView(string CustomerID, string MerchantID, string FromDate, string ToDate)
        {
            var modals = await _dealerService.GetMerchantDealerCreditSaleView(CustomerID, MerchantID, FromDate, ToDate);
            return PartialView("~/Views/Dealer/_MerchantDealerCreditSaleViewtbl.cshtml", modals);
        }
     
        
    }
}
