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
        public async Task<IActionResult> COMCOCustomerMapping(COMCOCustomerMappingViewModel model, string reset, string success, string error, string CustomerID, string CardNo)
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

    }
}
