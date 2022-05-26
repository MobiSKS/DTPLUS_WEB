using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.CustomerFinancial;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class CustomerFinancialController : Controller
    {
        private readonly ICustomerFinancialService _customerFinancialService;

        public CustomerFinancialController(ICustomerFinancialService customerFinancialService)
        {
            _customerFinancialService = customerFinancialService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CardToCardBalanceTransfer()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CardToCardBalanceTransfer(BalanceTransferSearchModel entity)
        {
            var searchList = await _customerFinancialService.SearchCardToCardTransfer(entity);
            return Json(new { searchList = searchList });
        }

        public IActionResult CCMSToCardBalanceTransfer()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CCMSToCardBalanceTransfer(BalanceTransferSearchModel entity)
        {
            var searchList = await _customerFinancialService.SearchCCMSToCardTransfer(entity); 
            return Json(new { searchList = searchList });
        }

        public IActionResult CardToCCMSBalanceTransfer()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CardToCCMSBalanceTransfer(BalanceTransferSearchModel entity)
        {
            var searchList = await _customerFinancialService.SearchCardToCCMSTransfer(entity);
            return Json(new { searchList = searchList });
        }

        public IActionResult CustomerTransactionDetails()
        {
            CustomerTransactionViewModel model = new CustomerTransactionViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomerTransactionDetails(string CustomerID, string CardNo,string MobileNo,string FromDate,string ToDate)
        {

            var models=await _customerFinancialService.GetCustomerTransactionDetails(CustomerID, CardNo, MobileNo, FromDate, ToDate);
            return PartialView("~/Views/CustomerFinancial/_CustomerTransactionTblView.cshtml", models);
            //return Json(models);
        }

        public IActionResult ViewAccountStatement()
        {
            GetViewAccountStatement getViewAccountStatement=new GetViewAccountStatement();
            return View(getViewAccountStatement);
        }

        [HttpPost]
        public async Task<JsonResult> ViewAccountStatement(GetViewAccountStatement entity)
        {
            var searchList = await _customerFinancialService.ViewAccountStatement(entity);
            return Json(new { searchList = searchList});
        }

        [HttpPost]
        public async Task<JsonResult> CCMSToCardBalanceTransferAmount(string customerId, string ccmsToCardTransfer)
        {
            var reasonList = await _customerFinancialService.CCMSToCardAmtTransfer(customerId, ccmsToCardTransfer);
            return Json(new { reasonList = reasonList });
        }

        [HttpPost]
        public async Task<JsonResult> CardToCardBalanceTransferAmount(string customerId, string cardToCardTransfer)
        {
            var reasonList = await _customerFinancialService.CardToCardAmtTransfer(customerId, cardToCardTransfer);
            return Json(new { reasonList = reasonList });
        }

        [HttpPost]
        public async Task<JsonResult> CardToCCMSBalanceTransferAmount(string customerId, string cardToCCMSTransfer)
        {
            var reasonList = await _customerFinancialService.CardToCCMSAmtTransfer(customerId, cardToCCMSTransfer);
            return Json(new { reasonList = reasonList });
        }

        public IActionResult CardToCardAmountTransferViaExcel(string CustomerId)
        {
            AmountTransferExcel uploadExcel = new AmountTransferExcel();
            uploadExcel.CustomerId = CustomerId;
            return View(uploadExcel);
        }
    }
}
