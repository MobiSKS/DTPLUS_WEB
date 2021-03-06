using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.DICV;
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
        public async Task<IActionResult> DICVAddOrEditMobile(string CustomerId)
        {
            ViewBag.CustomerId = CustomerId;
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> SearchCardMapping(DICVViewCardDetails viewCardDetails)
        {
            var searchList = await _dicvService.SearchCardMapping(viewCardDetails);

            ModelState.Clear();
            return Json(searchList);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCards(DICVUpdateMobileandFastagNoInCard[] limitArray)
        {
            var reason = await _dicvService.UpdateCards(limitArray);

            ModelState.Clear();
            return Json(reason);
        }
        [HttpPost]
        public async Task<JsonResult> AddCardMappingDetails(DICVViewCardDetails viewCardDetails)
        {
            var searchList = await _dicvService.AddCardMappingDetails(viewCardDetails);

            ModelState.Clear();
            return Json(searchList);
        }
        [HttpPost]
        public async Task<JsonResult> ResetDICVDealerPassword(string UserName)
        {
            var result = await _dicvService.ResetDICVDealerPassword(UserName);
            return Json(new { result = result });
        }
        [HttpPost]
        public async Task<JsonResult> EnableDisableDICVDealer(string DealerCode, string OfficerType, string EnableDisableFlag)
        {
            var result = await _dicvService.EnableDisableDICVDealer(DealerCode, OfficerType, EnableDisableFlag);
            return Json(new { result = result });
        }
        public IActionResult DICVManageCards()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> DICVManageCards(DICVCustomerCards entity, string editFlag)
        {
            var searchList = await _dicvService.DICVManageCards(entity, editFlag);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }
        [HttpPost]
        public async Task<JsonResult> DICVViewCardDetails(string CardId)
        {
            var searchList = await _dicvService.DICVViewCardDetails(CardId);

            ModelState.Clear();
            return Json(new
            {
                searchList = searchList
            });
        }
        public async Task<IActionResult> DICVCardlessMapping(string cardNumber, string mobileNumber, string LimitTypeName, string CCMSReloadSaleLimitValue)
        {
            var editMobBody = await _dicvService.DICVCardlessMapping(cardNumber, mobileNumber, LimitTypeName, CCMSReloadSaleLimitValue);
            return View(editMobBody);
        }

        [HttpPost]
        public async Task<JsonResult> DICVCardlessMappingUpdate(string mobNoNew, string crdNo)
        {
            var updateResponse = await _dicvService.DICVCardlessMappingUpdate(mobNoNew, crdNo);
            ModelState.Clear();

            return Json(new { updateResponse = updateResponse });
        }
        public async Task<IActionResult> ViewDICVDealerOTCCardStatus()
        {
            var model = await _dicvService.ViewDICVDealerOTCCardStatus();

            return View(model);
        }
        public async Task<IActionResult> GetDICVDealerOTCCardStatus(string DealerCode, string CardNo)
        {
            var modals = await _dicvService.GetDICVDealerOTCCardStatus(DealerCode, CardNo);
            return PartialView("~/Views/DICV/_DICVDealerOTCCardStatusTable.cshtml", modals);
        }
        public async Task<IActionResult> DICVManageProfile()
        {
            var custMdl = await _dicvService.DICVManageProfile();

            return View(custMdl);
        }
        [HttpPost]
        public async Task<JsonResult> BindCustomerDetailsForSearch(string CardNo, string Email, string CustomerId, string MobileNo)
        {
            var customerCardInfo = await _dicvService.BindCustomerDetailsForSearch(CardNo, Email, CustomerId, MobileNo);
            ModelState.Clear();
            return Json(customerCardInfo);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateDICVCustomerProfile(string str)
        {
            var result = await _dicvService.UpdateDICVCustomerProfile(str);
            return Json(new { result = result });
        }
        public async Task<IActionResult> DICVBalanceInfo()
        {
            var model = await _dicvService.DICVBalanceInfo();
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerBalanceInfo(string CustomerID)
        {
            var customerBalanceResponse = await _dicvService.GetCustomerBalanceInfo(CustomerID);

            return Json(customerBalanceResponse);
        }
        public IActionResult DICVCustomerTransactionDetails()
        {
            var model = new DICVCustomerTransactionViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetCustomerTransactionDetails(string CustomerID, string CardNo, string MobileNo, string FromDate, string ToDate)
        {
            var models = await _dicvService.GetCustomerTransactionDetails(CustomerID, CardNo, MobileNo, FromDate, ToDate);
            return PartialView("~/Views/DICV/_DICVCustomerTransactionTblView.cshtml", models);
        }
        public async Task<IActionResult> DICVCustomerAdvancedSearch()
        {
            var model = await _dicvService.DICVCustomerAdvancedSearch();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetDICVAdvancedSearch(string str)
        {
            var models = await _dicvService.GetJCBAdvancedSearch(str);
            return PartialView("~/Views/DICV/_DICVCustomerAdvancedSearchTblView.cshtml", models);
        }

    }
}
