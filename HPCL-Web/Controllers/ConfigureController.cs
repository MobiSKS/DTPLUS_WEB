using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.Configure;
using HPCL.Common.Models.ViewModel.Configure;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class ConfigureController : Controller
    {
        private readonly IConfigureService _configureService;
        private readonly ICommonActionService _commonActionService;
        public ConfigureController(IConfigureService configureService, ICommonActionService commonActionService)
        {
            _configureService = configureService;
            _commonActionService = commonActionService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ConfCardPhone(SMSAlertstoIndividualCardUsersModel model, string reset, string success, string error, string CustomerID, string CardNo)
        {
            var searchResult = await _configureService.ConfCardPhone(model);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            ViewBag.SuccessMessage = success;
            ViewBag.ErrorMessage = error;

            return View(searchResult);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteSMSAlertsToIndividualCardUsers(string customerID, string cardNo, string mobileNo)
        {
            var updateKycResponse = await _configureService.DeleteSMSAlertsToIndividualCardUsers(customerID, cardNo, mobileNo);

            return Json(new { customer = updateKycResponse });
        }

        [HttpPost]
        public async Task<JsonResult> SubmitSMSAlertstoIndividualCardUsers([FromBody] UpdateSMSAlertstoIndividualCardUsersRequest model)
        {
            var updateKycResponse = await _configureService.SubmitSMSAlertstoIndividualCardUsers(model);

            return Json(new { customer = updateKycResponse });
        }
    }
}
