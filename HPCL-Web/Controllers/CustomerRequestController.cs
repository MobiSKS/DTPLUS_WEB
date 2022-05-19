using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.CustomerRequest;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class CustomerRequestController : Controller
    {
        private readonly ICustomerRequestService _customerRequestService;

        public CustomerRequestController(ICustomerRequestService customerRequestService)
        {
            _customerRequestService = customerRequestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ConfigureSMSAlertstoMultipleMobileNumber()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ConfigureSMSAlertstoMultipleMobileNumber(GetSmsAlertForMultipleMobileDetailReq entity)
        {
            var searchList = await _customerRequestService.GetSmsAlertForMultipleMobileDetail(entity);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateSmsAlertForMultipleMobileDetail(string customerDetailForSmsAlert)
        {
            var reasonList = await _customerRequestService.UpdateSmsAlertForMultipleMobileDetail(customerDetailForSmsAlert);
            return Json(new { reasonList = reasonList });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteSmsAlertForMultipleMobileDetail(string CustomerID, string MobileNo)
        {
            var reasonList = await _customerRequestService.DeleteSmsAlertForMultipleMobileDetail(CustomerID,MobileNo);
            return Json(new { reasonList = reasonList });
        }
    }
}
