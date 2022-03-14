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
    }
}
