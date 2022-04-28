

using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.ApplicationFormDataEntry;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public async Task<IActionResult> AddAddOnCards()
        {
            var model = await _applicationFormDataEntryService.AddAddOnCards();

            return View(model);
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
        public async Task<IActionResult> GetAddOnCardsPartialView(string str)
        {
            var modals = await _applicationFormDataEntryService.GetAddOnCardsPartialView(str);
            return PartialView("~/Views/ApplicationFormDataEntry/_AddAddOnCardsTbl.cshtml", modals);
        }
        public async Task<IActionResult> CustomerAddCardVehicleTbl(string str)
        {
            var modals = await _applicationFormDataEntryService.CustomerAddCardVehicleTbl(str);
            return PartialView("~/Views/ApplicationFormDataEntry/_AddAddOnCardVehicleDetailsTable.cshtml", modals);
        }

        [HttpPost]
        public async Task<IActionResult> AddAddOnCards(AddAddOnCard addAddOnCard)
        {
            var Model = await _applicationFormDataEntryService.AddAddOnCards(addAddOnCard);

            if (Model.StatusCode == 1000)
            {
                return RedirectToAction("SuccessAddOnCardRedirect", new { Message = Model.Reason });
            }
           
            return View(Model);
        }
        public async Task<IActionResult> SuccessAddOnCardRedirect(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }

    }
}
