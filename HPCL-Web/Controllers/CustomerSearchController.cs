using HPCL.Common.Models.ViewModel.CustomerSearch;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class CustomerSearchController : Controller
    {
        private readonly ICustomerSearchService _customerSearchService;
        private readonly ICommonActionService _commonActionService;

        public CustomerSearchController(ICustomerSearchService customerSearchService,ICommonActionService commonActionService)
        {
            _customerSearchService = customerSearchService;
            _commonActionService = commonActionService;
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
     
            modals.RetailOutletStates.AddRange(await _commonActionService.GetStateList());
            return View(modals);
        }
        public async Task<IActionResult> SearchCustomerDetails(string customerId, string mobileNo, string formNumber, string nameonCard, string CustomerName, string communicationStateId, string communicationCityName)
        {
            var modals = await _customerSearchService.SearchCustomerDetails(customerId, mobileNo, formNumber, nameonCard, CustomerName, communicationStateId, communicationCityName);

            //BasicSearchModel obj=new BasicSearchModel 
            //{
            //RetailOutletStateName = await _customerSearchService.SearchCustomerDetails();

        //}


            return PartialView("~/Views/CustomerSearch/_SearchResultForCustomerTable.cshtml", modals);
        }
    }
}
