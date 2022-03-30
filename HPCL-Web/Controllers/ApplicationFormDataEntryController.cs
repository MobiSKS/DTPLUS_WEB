

using HPCL.Common.Helper;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class ApplicationFormDataEntryController : Controller
    {
        private readonly IApplicationFormDataEntryService _applicationFormDataEntryService;
        public ApplicationFormDataEntryController(IApplicationFormDataEntryService applicationFormDataEntryService)
        {
            _applicationFormDataEntryService = applicationFormDataEntryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddAddOnCards()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetFormName(string customerId)
        {
            var searchResult = await _applicationFormDataEntryService.GetCustomerName(customerId);
            return Json(new { searchResult = searchResult });
        }

        [HttpPost]
        public async Task<JsonResult> CheckAddOnForm(string formNumber)
        {
            var searchResult = await _applicationFormDataEntryService.CheckAddOnForm(formNumber);
            return Json(new { searchResult = searchResult });
        }
    }
}
