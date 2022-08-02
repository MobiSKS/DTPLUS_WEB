using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.JCB;
using HPCL.Common.Models.ResponseModel.JCB;
using HPCL.Common.Models.ViewModel.JCB;
using HPCL.Common.Resources;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class JCBController : Controller
    {
        private readonly IJCBService _jcbService;
        private readonly ICommonActionService _commonActionService;
        public JCBController(IJCBService jcbService, ICommonActionService commonActionService)
        {
            _jcbService = jcbService;
            _commonActionService = commonActionService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult JCBDealerEnrollment()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> JCBDealerEnrollment(string str)
        {
            var result = await _jcbService.InsertJCBDealerEnrollment(str);
            return Json(new { result = result });
        }

        [HttpPost]
        public async Task<JsonResult> JCBDealerEnrollmentUpdate(string getAllData)
        {
            var result = await _jcbService.JCBDealerEnrollmentUpdate(getAllData);
            return Json(new { result = result });
        }

        [HttpPost]
        public async Task<JsonResult> CheckJCBDealerCodeExists(string DealerCode)
        {
            var responseData = await _jcbService.CheckJCBDealerCodeExists(DealerCode);

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
        public async Task<JsonResult> SearchJCBDealer(string dealerCode, string dtpCode, string OfficerType)
        {
            var searchList = await _jcbService.SearchJCBDealer(dealerCode, dtpCode, OfficerType);
            return Json(new { searchList = searchList });
        }
        public async Task<IActionResult> JCBDealerOTCCardRequest()
        {
            var modals = await _jcbService.JCBDealerOTCCardRequest();
            return View(modals);
        }

        [HttpPost]
        public async Task<JsonResult> CheckJCBDealerBalanceQty(string DealerCode)
        {
            var responseData = await _jcbService.CheckJCBDealerBalanceQty(DealerCode);

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
        public async Task<IActionResult> JCBDealerOTCCardRequest(JCBOTCCardRequestModel model)
        {
            model = await _jcbService.JCBDealerOTCCardRequest(model);

            if (model.Internel_Status_Code == 1000)
            {
                return RedirectToAction("SuccessRedirectJCBOTCCardRequest", new { Message = model.Remarks });
            }

            return View(model);
        }

        public async Task<IActionResult> SuccessRedirectJCBOTCCardRequest(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }
        public async Task<IActionResult> ViewJCBUnmappedOTCCards()
        {
            ViewJCBUnmappedOTCCardsModel Model = new ViewJCBUnmappedOTCCardsModel();
            Model = await _jcbService.ViewJCBUnmappedOTCCards();

            return View(Model);
        }
        public async Task<IActionResult> GetViewJCBOTCCardDealerAllocation(string DealerCode, string CardNo, bool ShowUnmappedCard)
        {
            var modals = await _jcbService.GetViewJCBOTCCardDealerAllocation(DealerCode, CardNo, ShowUnmappedCard);
            return PartialView("~/Views/JCB/_JCBOTCCardsDealerAllocationTable.cshtml", modals);
        }
        public async Task<IActionResult> JCBCustomerEnrollment()
        {
            var modals = await _jcbService.JCBCustomerEnrollment();
            return View(modals);
        }

        [HttpPost]
        public async Task<JsonResult> GetJCBSalesExeEmpId(string dealerCode)
        {
            var addonOTCCardMapping = await _jcbService.GetJCBSalesExeEmpId(dealerCode);

            return Json(addonOTCCardMapping);
        }
        public async Task<IActionResult> GetJCBCustomerOTCCardPartialView([FromBody] List<JCBCardEntryDetails> objCardDetails)
        {
            var modals = await _jcbService.GetJCBCustomerOTCCardPartialView(objCardDetails);
            return PartialView("~/Views/JCB/_JCBCustomerOTCCardVehicleDetailsTbl.cshtml", modals);
        }
        [HttpPost]
        public async Task<JsonResult> GetAvailableJCBOTCCardForDealer(string DealerCode)
        {
            List<JCBOTCCardDetails> lstCardDetails = await _jcbService.GetAvailableJCBOTCCardForDealer(DealerCode);

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
        public async Task<IActionResult> JCBCustomerEnrollment(JCBCustomerEnrollmentModel customerModel)
        {

            customerModel = await _jcbService.JCBCustomerEnrollment(customerModel);

            if (customerModel.Internel_Status_Code == 1000)
            {
                return RedirectToAction("SuccessRedirectJCBCustomerEnrollment", new { Message = customerModel.Remarks });
            }

            return View(customerModel);
        }
        public async Task<IActionResult> SuccessRedirectJCBCustomerEnrollment(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }
        public async Task<IActionResult> JCBManageProfile()
        {
            var custMdl = await _jcbService.JCBManageProfile();

            return View(custMdl);
        }
        [HttpPost]
        public async Task<JsonResult> BindCustomerDetailsForSearch(string CardNo, string Email, string CustomerId, string MobileNo)
        {
            var customerCardInfo = await _jcbService.BindCustomerDetailsForSearch(CardNo, Email, CustomerId, MobileNo);
            ModelState.Clear();
            return Json(customerCardInfo);
        }
        [HttpPost]
        public async Task<JsonResult> CardDetailsForSearch(string CustomerId, string CustomerTypeId)
        {
            var customerCardInfo = await _jcbService.CardDetailsForSearch(CustomerId, CustomerTypeId);
            ModelState.Clear();
            return Json(customerCardInfo);
        }
        public async Task<IActionResult> JCBResetPasswordByMo()
        {
            var custMdl = new JCBCustomerResetPassword();

            return View(custMdl);
        }
        [HttpPost]
        public async Task<JsonResult> GetJCBCommunicationEmailResetPassword(string CustomerId)
        {
            var responseData = await _jcbService.GetJCBCommunicationEmailResetPassword(CustomerId);
            ModelState.Clear();
            return Json(responseData);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateJCBCommunicationEmailResetPassword(string CustomerId, string AlternateEmailId)
        {
            var result = await _jcbService.UpdateJCBCommunicationEmailResetPassword(CustomerId, AlternateEmailId);
            return Json(new { result = result });
        }
        public IActionResult JCBManageCards()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> JCBManageCards(JCBCustomerCards entity, string editFlag)
        {
            var searchList = await _jcbService.JCBManageCards(entity, editFlag);
            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> JCBViewCardDetails(string CardId)
        {
            var searchList = await _jcbService.JCBViewCardDetails(CardId);

            ModelState.Clear();
            return Json(new
            {
                searchList = searchList
            });
        }
        public async Task<IActionResult> JCBCardlessMapping(string cardNumber, string mobileNumber, string LimitTypeName, string CCMSReloadSaleLimitValue)
        {
            var editMobBody = await _jcbService.JCBCardlessMapping(cardNumber, mobileNumber, LimitTypeName, CCMSReloadSaleLimitValue);
            return View(editMobBody);
        }

        [HttpPost]
        public async Task<JsonResult> JCBCardlessMappingUpdate(string mobNoNew, string crdNo)
        {
            var updateResponse = await _jcbService.JCBCardlessMappingUpdate(mobNoNew, crdNo);
            ModelState.Clear();

            return Json(new { updateResponse = updateResponse });
        }
        public async Task<IActionResult> JCBAddOrEditMobile(string CustomerId)
        {
            ViewBag.CustomerId = CustomerId;
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> SearchCardMapping(JCBViewCardDetails viewCardDetails)
        {
            var searchList = await _jcbService.SearchCardMapping(viewCardDetails);

            ModelState.Clear();
            return Json(searchList);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCards(JCBUpdateMobileandFastagNoInCard[] limitArray)
        {
            var reason = await _jcbService.UpdateCards(limitArray);

            ModelState.Clear();
            return Json(reason);
        }
        [HttpPost]
        public async Task<JsonResult> AddCardMappingDetails(JCBViewCardDetails viewCardDetails)
        {
            var searchList = await _jcbService.AddCardMappingDetails(viewCardDetails);

            ModelState.Clear();
            return Json(searchList);
        }
        public async Task<ActionResult> GetJCBDealerCardDispatchDetails(string CustomerID)
        {
            var model = await _jcbService.GetJCBDealerCardDispatchDetails(CustomerID);
            return PartialView("~/Views/JCB/_ViewJCBDealerCardDispatchDetails.cshtml", model);
        }
        [HttpPost]
        public async Task<JsonResult> ResetJCBDealerPassword(string UserName)
        {
            var result = await _jcbService.ResetJCBDealerPassword(UserName);
            return Json(new { result = result });
        }
        public async Task<IActionResult> JCBHotlistAndReactivate()
        {
            var model = await _jcbService.JCBHotlistAndReactivate();

            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> GetReasonListForEntities(string EntityTypeId)
        {
            var sortedtList = await _jcbService.GetReasonListForEntities(EntityTypeId);

            ModelState.Clear();
            return Json(sortedtList);
        }
        [HttpPost]
        public async Task<IActionResult> ApplyHotlistorReactivate([FromBody] JCBHotlistorReactivateViewModel HotlistorReactivateResponseModel)
        {
            var result = await _jcbService.ApplyHotlistorReactivate(HotlistorReactivateResponseModel);
            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> EnableDisableJCBDealer(string DealerCode, string OfficerType, string EnableDisableFlag)
        {
            var result = await _jcbService.EnableDisableJCBDealer(DealerCode, OfficerType, EnableDisableFlag);
            return Json(new { result = result });
        }
        public async Task<IActionResult> ViewJCBDealerOTCCardStatus()
        {
            var model = await _jcbService.ViewJCBDealerOTCCardStatus();

            return View(model);
        }
        public async Task<IActionResult> GetViewJCBDealerOTCCardStatus(string DealerCode, string CardNo)
        {
            var modals = await _jcbService.GetViewJCBDealerOTCCardStatus(DealerCode, CardNo);
            return PartialView("~/Views/JCB/_JCBDealerOTCCardStatusTable.cshtml", modals);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateJCBCustomerProfile(string str)
        {
            var result = await _jcbService.UpdateJCBCustomerProfile(str);
            return Json(new { result = result });
        }
        public async Task<IActionResult> JCBBalanceInfo()
        {
            var model = await _jcbService.JCBBalanceInfo();
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerBalanceInfo(string CustomerID)
        {
            var customerBalanceResponse = await _jcbService.GetCustomerBalanceInfo(CustomerID);

            return Json(customerBalanceResponse);
        }
        public IActionResult JCBCustomerTransactionDetails()
        {
            var model = new JCBCustomerTransactionViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetCustomerTransactionDetails(string CustomerID, string CardNo, string MobileNo, string FromDate, string ToDate)
        {
            var models = await _jcbService.GetCustomerTransactionDetails(CustomerID, CardNo, MobileNo, FromDate, ToDate);
            return PartialView("~/Views/JCB/_JCBCustomerTransactionTblView.cshtml", models);
        }
        public async Task<IActionResult> JCBCustomerAdvancedSearch()
        {
            var model = await _jcbService.JCBCustomerAdvancedSearch();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetJCBAdvancedSearch(string str)
        {
            var models = await _jcbService.GetJCBAdvancedSearch(str);
            return PartialView("~/Views/JCB/_JCBCustomerAdvancedSearchTblView.cshtml", models);
        }

    }
}
