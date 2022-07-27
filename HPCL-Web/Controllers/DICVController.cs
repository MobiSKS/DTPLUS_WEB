using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.DICV;
using HPCL.Common.Models.ViewModel.DICV;
using HPCL.Common.Resources;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class DICVController : Controller
    {
        private readonly IDICVService _dicvService;
        private readonly ICommonActionService _commonActionService;
        public DICVController(IDICVService dicvService, ICommonActionService commonActionService)
        {
            _dicvService = dicvService;
            _commonActionService = commonActionService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> DICVDealerEnrollment()
        {
            var modals = await _dicvService.DICVDealerEnrollment();
            return View(modals);
        }
        [HttpPost]
        public async Task<JsonResult> DICVDealerEnrollment(string str)
        {
            var result = await _dicvService.InsertDICVDealerEnrollment(str);
            return Json(new { result = result });
        }
        [HttpPost]
        public async Task<JsonResult> SearchDICVDealer(string dealerCode, string dtpCode, string OfficerType)
        {
            var searchList = await _dicvService.SearchDICVDealer(dealerCode, dtpCode, OfficerType);
            return Json(new { searchList = searchList });
        }
        [HttpPost]
        public async Task<JsonResult> DICVDealerEnrollmentUpdate(string getAllData)
        {
            var result = await _dicvService.DICVDealerEnrollmentUpdate(getAllData);
            return Json(new { result = result });
        }
        [HttpPost]
        public async Task<JsonResult> CheckDICVDealerCodeExists(string DealerCode)
        {
            var responseData = await _dicvService.CheckDICVDealerCodeExists(DealerCode);

            ModelState.Clear();

            if (responseData.Internel_Status_Code.ToString() == Constants.SuccessInternelStatusCode)
            {
                return Json(responseData);
            }
            else
            {
                return Json("Failed to load Dealer Details");
            }
        }
        public async Task<IActionResult> DICVDealerOTCCardRequest()
        {
            var modals = await _dicvService.DICVDealerOTCCardRequest();
            return View(modals);
        }
        [HttpPost]
        public async Task<JsonResult> CheckDICVDealerBalanceQty(string DealerCode)
        {
            var responseData = await _dicvService.CheckDICVDealerBalanceQty(DealerCode);

            ModelState.Clear();

            if (responseData.Internel_Status_Code.ToString() == Constants.SuccessInternelStatusCode)
            {
                return Json(responseData);
            }
            else
            {
                return Json("Failed to load Dealer Details");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DICVDealerOTCCardRequest(DICVOTCCardRequestModel model)
        {
            model = await _dicvService.DICVDealerOTCCardRequest(model);

            if (model.Internel_Status_Code == 1000)
            {
                return RedirectToAction("SuccessRedirectDICVOTCCardRequest", new { Message = model.Remarks });
            }

            return View(model);
        }

        public async Task<IActionResult> SuccessRedirectDICVOTCCardRequest(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }
        public async Task<IActionResult> DICVCustomerEnrollment()
        {
            var modals = await _dicvService.DICVCustomerEnrollment();
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> DICVCustomerEnrollment(DICVCustomerEnrollmentModel customerModel)
        {

            customerModel = await _dicvService.DICVCustomerEnrollment(customerModel);

            if (customerModel.Internel_Status_Code == 1000)
            {
                return RedirectToAction("SuccessRedirectDICVCustomerEnrollment", new { Message = customerModel.Remarks });
            }

            return View(customerModel);
        }
        public async Task<IActionResult> SuccessRedirectDICVCustomerEnrollment(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }
        public async Task<IActionResult> GetDICVCustomerOTCCardPartialView([FromBody] List<DICVCardEntryDetails> objCardDetails)
        {
            var modals = await _dicvService.GetDICVCustomerOTCCardPartialView(objCardDetails);
            return PartialView("~/Views/DICV/_DICVCustomerOTCCardVehicleDetailsTbl.cshtml", modals);
        }
        [HttpPost]
        public async Task<JsonResult> GetAvailableDICVOTCCardForDealer(string DealerCode)
        {
            List<DICVOTCCardDetails> lstCardDetails = await _dicvService.GetAvailableDICVOTCCardForDealer(DealerCode);

            if (lstCardDetails != null)
            {
                return Json(new { lstCardDetails = lstCardDetails });
            }
            else
            {
                return Json("Failed to load Card Details");
            }
        }
        [HttpPost]
        public async Task<JsonResult> GetDICVSalesExeEmpId(string dealerCode)
        {
            var addonOTCCardMapping = await _dicvService.GetDICVSalesExeEmpId(dealerCode);

            return Json(addonOTCCardMapping);
        }
        public async Task<IActionResult> DICVHotlistAndReactivate()
        {
            var model = await _dicvService.DICVHotlistAndReactivate();

            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> GetReasonListForEntities(string EntityTypeId)
        {
            var sortedtList = await _dicvService.GetReasonListForEntities(EntityTypeId);

            ModelState.Clear();
            return Json(sortedtList);
        }
        [HttpPost]
        public async Task<IActionResult> ApplyHotlistorReactivate([FromBody] DICVHotlistorReactivateViewModel HotlistorReactivateResponseModel)
        {
            var result = await _dicvService.ApplyHotlistorReactivate(HotlistorReactivateResponseModel);
            return Json(result);
        }
        public async Task<IActionResult> DICVResetPasswordByMo()
        {
            var custMdl = new DICVCustomerResetPassword();

            return View(custMdl);
        }
        [HttpPost]
        public async Task<JsonResult> GetDICVCommunicationEmailResetPassword(string CustomerId)
        {
            var responseData = await _dicvService.GetDICVCommunicationEmailResetPassword(CustomerId);
            ModelState.Clear();
            return Json(responseData);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateDICVCommunicationEmailResetPassword(string CustomerId, string AlternateEmailId)
        {
            var result = await _dicvService.UpdateDICVCommunicationEmailResetPassword(CustomerId, AlternateEmailId);
            return Json(new { result = result });
        }

    }
}
