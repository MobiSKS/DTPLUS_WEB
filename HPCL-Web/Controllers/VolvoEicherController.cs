using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ResponseModel.CustomerManage;
using HPCL.Common.Models.ResponseModel.VolvoEicher;
using HPCL.Common.Models.ViewModel.VolvoEicher;
using HPCL.Common.Resources;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class VolvoEicherController : Controller
    {
        private readonly IVolvoEicherService _volvoEicherService;
        private readonly ICommonActionService _commonActionService;
        public VolvoEicherController(IVolvoEicherService volvoEicherService, ICommonActionService commonActionService)
        {
            _volvoEicherService = volvoEicherService;
            _commonActionService = commonActionService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ViewVEDealerOTCDCardStatus()
        {
            VEDealerOTCCardStatusViewModel modals = new VEDealerOTCCardStatusViewModel();
            return View(modals);
        }

        public async Task<IActionResult> GetVEDealerCardStatus(string DealerCode)
        {
            var modals = await _volvoEicherService.GetVEDealerCardStatus(DealerCode);
            return PartialView("~/Views/VolvoEicher/_VEDealerCardStatusTbl.cshtml", modals);
        }
        [HttpPost]
        public async Task<JsonResult> BindCustomerDetailsForSearch(string CustomerId, string NameOnCard)
        {
            var customerCardInfo = await _volvoEicherService.BindCustomerDetailsForSearch(CustomerId, NameOnCard);
            ModelState.Clear();
            return Json(customerCardInfo);
        }
        public async Task<IActionResult> ManageProfile()
        {
            var custMdl = await _volvoEicherService.ManageProfile();

            return View(custMdl);
        }
        public IActionResult VEDealerEnrollment()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> VEDealerEnrollment(string str)
        {
            var result = await _volvoEicherService.InsertVEDealerEnrollment(str);
            return Json(new { result = result });
        }

        [HttpPost]
        public async Task<JsonResult> SearchVEDealer(string dealerCode, string dtpCode)
        {
            var searchList = await _volvoEicherService.SearchVEDealer(dealerCode, dtpCode);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> VEDealerEnrollmentUpdate(string getAllData)
        {
            var result = await _volvoEicherService.VEDealerEnrollmentUpdate(getAllData);
            return Json(new { result = result });
        }

        [HttpPost]
        public async Task<JsonResult> CheckVEDealerCodeExists(string DealerCode)
        {
            var responseData = await _volvoEicherService.CheckVEDealerCodeExists(DealerCode);

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

        public async Task<IActionResult> VEDealerOTCCardRequest()
        {
            var modals = await _volvoEicherService.VEDealerOTCCardRequest();
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> VEDealerOTCCardRequest(VEOTCCardRequestModel model)
        {
            model = await _volvoEicherService.VEDealerOTCCardRequest(model);

            if (model.Internel_Status_Code == 1000)
            {
                return RedirectToAction("SuccessRedirectVEDealerOTCCardRequest", new { Message = model.Remarks });
            }

            return View(model);
        }

        public async Task<IActionResult> SuccessRedirectVEDealerOTCCardRequest(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ResetVEDealerPassword(string UserName)
        {
            var result = await _volvoEicherService.ResetVEDealerPassword(UserName);
            return Json(new { result = result });
        }
        public async Task<IActionResult> ViewVEDealerUnmappedOTCCardDetails()
        {
            ViewVEDealerOTCCardDetailsModel Model = new ViewVEDealerOTCCardDetailsModel();
            Model = await _volvoEicherService.ViewVEDealerUnmappedOTCCardDetails();

            return View(Model);
        }
        public async Task<IActionResult> GetViewVEOTCCardDealerAllocation(string DealerCode, string CardNo)
        {
            var modals = await _volvoEicherService.GetViewVEOTCCardDealerAllocation(DealerCode, CardNo);
            return PartialView("~/Views/VolvoEicher/_VEOTCCardsDealerAllocationTable.cshtml", modals);
        }
        public async Task<IActionResult> CreateMultipleOTCCard()
        {
            var modals = await _volvoEicherService.CreateMultipleOTCCard();
            return View(modals);
        }
        [HttpPost]
        public async Task<JsonResult> GetVESalesExecutiveEmpId(string dealerCode)
        {
            var model = await _volvoEicherService.GetVESalesExecutiveEmpId(dealerCode);

            return Json(model);
        }
        public async Task<IActionResult> GetMultipleOTCCardPartialView([FromBody] List<VECardEntryDetails> objCardDetails)
        {
            var modals = await _volvoEicherService.GetMultipleOTCCardPartialView(objCardDetails);
            return PartialView("~/Views/VolvoEicher/_MultipleOTCCardVehicleDetailsTbl.cshtml", modals);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMultipleOTCCard(VECardCreationModel model)
        {

            model = await _volvoEicherService.CreateMultipleOTCCard(model);

            if (model.Internel_Status_Code == 1000)
            {
                return RedirectToAction("SuccessRedirectVECustomer", new { Message = model.Remarks });
            }

            return View(model);
        }
        public async Task<IActionResult> SuccessRedirectVECustomer(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetAvailableVEOTCCardForDealer(string DealerCode)
        {
            List<VEOTCCardResponse> lstCardDetails = await _volvoEicherService.GetAvailableVEOTCCardForDealer(DealerCode);

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
        public async Task<JsonResult> CardDetailsForSearch(String CustomerId, String CustomerTypeId)
        {
            var customerCardInfo = await _volvoEicherService.CardDetailsForSearch(CustomerId, CustomerTypeId);
            ModelState.Clear();
            return Json(customerCardInfo);
        }
        public async Task<IActionResult> ExistingCustomerCardMap()
        {
            var modals = await _volvoEicherService.ExistingCustomerCardMap();
            return View(modals);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateVECustomerProfile(string str)
        {
            var result = await _volvoEicherService.UpdateVECustomerProfile(str);
            return Json(new { result = result });
        }
        [HttpPost]
        public async Task<JsonResult> GetVEAddonOTCCardMappingCustomerDetails(string customerId)
        {
            var searchResult = await _volvoEicherService.GetVEAddonOTCCardMappingCustomerDetails(customerId);
            return Json(new { searchResult = searchResult });
        }
        public async Task<IActionResult> GetVEAddonOTCCardCustomerDetailsPartialView([FromBody] List<VEAddonOTCCardDetails> objCardDetails)
        {
            var modals = await _volvoEicherService.GetVEAddonOTCCardCustomerDetailsPartialView(objCardDetails);
            return PartialView("~/Views/VolvoEicher/_VEAddonOTCCardCustomerDetailsTbl.cshtml", modals);
        }
        public async Task<IActionResult> GetVEAddonOTCCardAddCardsPartialView([FromBody] List<VEAddonOTCCardDetails> objCardDetails)
        {
            var modals = await _volvoEicherService.GetVEAddonOTCCardAddCardsPartialView(objCardDetails);
            return PartialView("~/Views/VolvoEicher/_VEAddonOTCCardVehicleDetailsTbl.cshtml", modals);
        }
        [HttpPost]
        public async Task<IActionResult> ExistingCustomerCardMap(VEAddonOTCCardMapping addAddOnCard)
        {
            var Model = await _volvoEicherService.ExistingCustomerCardMap(addAddOnCard);

            if (Model.StatusCode == 1000)
            {
                return RedirectToAction("SuccessExistingCustomerCardMap", new { Message = Model.Reason });
            }

            return View(Model);
        }
        public async Task<IActionResult> SuccessExistingCustomerCardMap(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }
        public async Task<ActionResult> GetVECardDispatchDetails(string CustomerID)
        {
            var model = await _volvoEicherService.GetVECardDispatchDetails(CustomerID);
            return PartialView("~/Views/VolvoEicher/_ViewVECardDispatchDetails.cshtml", model);
        }

    }
}
