using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.Customer;
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

        public IActionResult CardRenewalRequest()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CardRenewalRequest(GetCardRenwalRequestList entity)
        {
            var searchList = await _customerRequestService.GetCardRenwalRequest(entity);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateCardRenwalRequest(string CustomerId, string updatePostArray)
        {
            var reasonList = await _customerRequestService.UpdateCardRenwalRequest(CustomerId, updatePostArray);
            return Json(new { reasonList = reasonList });
        }

        public IActionResult ConfigureSMSAlerts()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ConfigureSMSAlerts(GetConfigureSmsAlerts entity)
        {
            var searchList = await _customerRequestService.GetSmsAlertsConfigure(entity);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateSmsAlertsConfigure(string CustomerId, string SmsAlertList)
        {
            var reasonList = await _customerRequestService.UpdateSmsAlertsConfigure(CustomerId, SmsAlertList);
            return Json(new { reasonList = reasonList });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateDndSmsAlertsConfigure(string CustomerId)
        {
            var reasonList = await _customerRequestService.UpdateDndSmsAlertsConfigure(CustomerId);
            return Json(new { reasonList = reasonList });
        }

        public IActionResult HotlistCardsPermanently()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> HotlistCardsPermanently(GetHotlistCardsPermanently entity)
        {
            var searchList = await _customerRequestService.HotlistCardsPermanently(entity);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> UpdatePermanentlyHotlistCards(string CustomerId, string cardsList)
        {
            var reasonList = await _customerRequestService.UpdatePermanentlyHotlistCards(CustomerId, cardsList);
            return Json(new { reasonList = reasonList });
        }

        public async Task<IActionResult> ConfigureEmailAlerts()
        {
            ConfigureEmailAlertViewModel modals = new ConfigureEmailAlertViewModel();
            return View(modals);
        }

        [HttpPost]
        public async Task<IActionResult> ConfigureEmailAlerts(ConfigureEmailAlertViewModel reqModel)
        {
            ConfigureEmailAlertViewModel modals = new ConfigureEmailAlertViewModel();
            if (reqModel != null)
            {
                if (reqModel.CustomerID != null)
                    modals = await _customerRequestService.ConfigureEmailAlerts(reqModel.CustomerID);
            }
            return View(modals);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateConfigureEmailAlert([FromBody] ConfigureEmailAlertRequest reqModel)
        {
            var modals = await _customerRequestService.UpdateConfigureEmailAlert(reqModel);
            return Json(modals);
        }

        public IActionResult ApproveCardRenewRequest()
        {
            ApproveCardRenwalRequestReq entity = new ApproveCardRenwalRequestReq();
            return View(entity);
        }

        [HttpPost]
        public async Task<JsonResult> ApproveCardRenewRequest(ApproveCardRenwalRequestReq entity)
        {
            var searchList = await _customerRequestService.GetApproveCardRenwalRequest(entity);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateApproveCardRenwalRequest(string actionType, string appRejValues)
        {
            var reasonList = await _customerRequestService.UpdateApproveCardRenwalRequest(actionType, appRejValues);
            return Json(new { reasonList = reasonList });
        }
    }
}
