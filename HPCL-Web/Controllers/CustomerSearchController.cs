using HPCL.Common.Models.ViewModel.CustomerSearch;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class CustomerSearchController : Controller
    {
        private readonly ICustomerSearchService _customerSearchService;

        public CustomerSearchController(ICustomerSearchService customerSearchService)
        {
            _customerSearchService = customerSearchService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ControlCardPinReset()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ControlCardPinReset(ControlCardPinResetReq entity)
        {
            var searchList = await _customerSearchService.CCPinReset(entity);
            return Json(new { searchList = searchList });
        }


        public async Task<IActionResult> SearchCustomer()
        {
            var modals = await _customerSearchService.SearchCustomer();
            return View(modals);
        }
        public async Task<IActionResult> SearchCustomerDetails(string customerId, string mobileNo, string formNumber, string nameonCard, string CustomerName, string communicationStateId, string communicationCityName)
        {
            var modals = await _customerSearchService.SearchCustomerDetails(customerId, mobileNo, formNumber, nameonCard, CustomerName, communicationStateId, communicationCityName);
            return PartialView("~/Views/CustomerSearch/_SearchResultForCustomerTable.cshtml", modals);
        }
    }
}
