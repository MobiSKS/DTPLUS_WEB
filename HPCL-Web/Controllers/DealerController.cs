using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.Dealer;
using HPCL.Common.Models.ViewModel.Dealer;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
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
     
        public async Task<IActionResult> DealerCreditMapping(DealerCreditMappingViewModel entity,string reset,string CustomerID, string success, string error)
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
            ViewBag.CustomerID = String.IsNullOrEmpty(entity.CustomerID)?"0": entity.CustomerID;
            return View(response);
        }
 
        public IActionResult MapDealerForCreditSale(string CustomerID,string NoofEntries)
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
            var result = await _dealerService.MerchantMapEnableorDisable(customerID,merchantID, actionID);
            string succesMsg = "", errorMsg = "";
            if (result[0].Status == 1)
                succesMsg = result[0].Reason;
            else if (result[0].Status == 0)
                errorMsg = result[0].Reason;
            return RedirectToAction("DealerCreditMapping", "Dealer", new { CustomerID = customerID , success = succesMsg, error = errorMsg });
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
        public async Task<IActionResult> GetCreditSaleStatement(string CustomerID, string MerchantID, string FromDate,string ToDate)
        {
            var searchList = await _dealerService.GetCreditSaleStatement(CustomerID, MerchantID, FromDate,ToDate);
            return PartialView("~/Views/Dealer/_DealerCreditSaleStatementTbl.cshtml", searchList);
        }


    }
}
