using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.COMCOManager;
using HPCL.Common.Models.ViewModel.COMCOManager;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class COMCOManagerController : Controller
    {
        private readonly ICOMCOManagerService _comCOManagerService;
        private readonly ICommonActionService _commonActionService;
        public COMCOManagerController(ICOMCOManagerService comCOManagerService, ICommonActionService commonActionService)
        {
            _comCOManagerService = comCOManagerService;
            _commonActionService = commonActionService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> COMCOCustomerMapping(COMCOCustomerMappingViewModel model, string reset, string success, string error, string CustomerID, string MerchantID)
        {
            var searchResult = await _comCOManagerService.COMCOCustomerMapping(model);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            ViewBag.SuccessMessage = success;
            ViewBag.ErrorMessage = error;

            return View(searchResult);
        }
        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> UpdateCOMCOMapCustomer([FromBody] UpdateCOMCOMapCustomerRequest model)
        {
            var updateKycResponse = await _comCOManagerService.UpdateCOMCOMapCustomer(model);

            return Json(new { customer = updateKycResponse });
        }
        public async Task<IActionResult> ViewMappedCreditCustomers(ViewMappedCreditCustomersModel model, string reset, string success, string error, string UserName, string Status, string FromDate, string ToDate)
        {
            var searchResult = await _comCOManagerService.ViewMappedCreditCustomers(model);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            ViewBag.SuccessMessage = success;
            ViewBag.ErrorMessage = error;
            if (!String.IsNullOrEmpty(reset))
            {
                model.FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
                model.ToDate = DateTime.Now.ToString("dd-MM-yyyy");
            }
            return View(searchResult);
        }
        public async Task<IActionResult> RequestToSetCreditLimit()
        {
            var modals = await _comCOManagerService.RequestToSetCreditLimit();
            return View(modals);
        }

        [HttpPost]
        public async Task<IActionResult> RequestToSetCreditLimit(RequestToSetCreditLimitModel model)
        {
            model = await _comCOManagerService.RequestToSetCreditLimit(model);

            if (model.Internel_Status_Code == 1000)
            {
                return RedirectToAction("SuccessRedirectSetCreditLimit", new { Message = model.Remarks });
            }

            return View(model);
        }
        public async Task<IActionResult> SuccessRedirectSetCreditLimit(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }

    }
}
