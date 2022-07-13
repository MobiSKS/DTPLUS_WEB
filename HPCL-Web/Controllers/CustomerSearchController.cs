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
    }
}
