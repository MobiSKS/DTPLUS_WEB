using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.AshokLeyLand;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class AshokLeyLandController : Controller
    {
        private readonly IAshokLeyLandService _ashokLeyLandService;
        private readonly ICommonActionService _commonActionService;

        public AshokLeyLandController(IAshokLeyLandService ashokLeyLandService, ICommonActionService commonActionService)
        {
            _ashokLeyLandService = ashokLeyLandService;
            _commonActionService = commonActionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AlEnroll()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> AlEnroll(string str)
        {
            var result = await _ashokLeyLandService.InsertAlEnroll(str);
            return Json(new { result = result });
        }

        [HttpPost]
        public async Task<JsonResult> SearchDealer(string dealerCode, string dtpCode)
        {
            var searchList = await _ashokLeyLandService.SearchDealer(dealerCode, dtpCode);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> AlEnrollUpdate(string getAllData)
        {
            var result = await _ashokLeyLandService.AlEnrollUpdate(getAllData);
            return Json(new { result = result });
        }

        public async Task<IActionResult> DealerOTCCardRequest()
        {
            var modals = await _ashokLeyLandService.DealerOTCCardRequest();
            return View(modals);
        }
        [HttpPost]
        public async Task<IActionResult> DealerOTCCardRequest(ALOTCCardRequestModel alOTCCardRequestModel)
        {

            alOTCCardRequestModel = await _ashokLeyLandService.DealerOTCCardRequest(alOTCCardRequestModel);

            if (alOTCCardRequestModel.Internel_Status_Code == 1000)
            {
                alOTCCardRequestModel.Remarks = "";
                ViewBag.Message = "AL OTC Card request saved successfully";
                return RedirectToAction("SuccessRedirectDealerOTCCardRequest");
            }

            return View(alOTCCardRequestModel);
        }

        public async Task<IActionResult> SuccessRedirectDealerOTCCardRequest()
        {
            return View();
        }
        public async Task<IActionResult> CreateMultipleOTCCard()
        {
            var modals = await _ashokLeyLandService.CreateMultipleOTCCard();
            return View(modals);
        }
        [HttpPost]
        public async Task<JsonResult> GetAvailableAlOTCCardForDealer(string DealerCode)
        {
            List<CardDetails> lstCardDetails = await _ashokLeyLandService.GetAvailableAlOTCCardForDealer(DealerCode);

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
        public async Task<JsonResult> CheckVehicleRegistrationValid(string RegistrationNumber)
        {
            var data = await _commonActionService.CheckVehicleRegistrationValid(RegistrationNumber);
            return new JsonResult(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMultipleOTCCard(AshokLeylandCardCreationModel ashokLeylandCardCreationModel)
        {

            ashokLeylandCardCreationModel = await _ashokLeyLandService.CreateMultipleOTCCard(ashokLeylandCardCreationModel);

            if (ashokLeylandCardCreationModel.Internel_Status_Code == 1000)
            {
                ashokLeylandCardCreationModel.Remarks = "";
                ViewBag.Message = "Ashok Leyland Card Customer saved successfully";
                return RedirectToAction("SuccessRedirectCreateMultipleOTCCard");
            }

            return View(ashokLeylandCardCreationModel);
        }
        public async Task<IActionResult> SuccessRedirectCreateMultipleOTCCard()
        {
            return View();
        }

        public async Task<IActionResult> ViewALOTCCardsDealerMapping()
        {
            ViewALOTCCardDealerMappingModel Model = new ViewALOTCCardDealerMappingModel();
            Model = await _ashokLeyLandService.ViewALOTCCardsDealerMapping();

            return View(Model);
        }

        public async Task<IActionResult> GetViewALOTCCardDealerAllocation(string DealerCode, string CardNo)
        {
            var modals = await _ashokLeyLandService.GetViewALOTCCardDealerAllocation(DealerCode, CardNo);
            return PartialView("~/Views/AshokLeyLand/_ALOTCCardsDealerAllocationTable.cshtml", modals);
        }

        public async Task<IActionResult> AddonOTCCardMapping()
        {
            var modals = new AddonOTCCardMapping();
            modals.Remarks = "";
            modals.Message = "";
            return View(modals);
        }

        [HttpPost]
        public async Task<JsonResult> GetAlAddonOTCCardMappingCustomerDetails(string customerId)
        {
            var searchResult = await _ashokLeyLandService.GetAlAddonOTCCardMappingCustomerDetails(customerId);
            return Json(new { searchResult = searchResult });
        }
        public async Task<IActionResult> GetAlAddonOTCCardCustomerDetailsPartialView(string str)
        {
            var modals = await _ashokLeyLandService.GetAlAddonOTCCardCustomerDetailsPartialView(str);
            return PartialView("~/Views/AshokLeyLand/_AddonOTCCardCustomerDetailsTbl.cshtml", modals);
        }
        public async Task<IActionResult> GetAlAddonOTCCardAddCardsPartialView(string str)
        {
            var modals = await _ashokLeyLandService.GetAlAddonOTCCardAddCardsPartialView(str);
            return PartialView("~/Views/AshokLeyLand/_AddonOTCCardVehicleDetailsTbl.cshtml", modals);
        }

        [HttpPost]
        public async Task<IActionResult> AddonOTCCardMapping(AddonOTCCardMapping addAddOnCard)
        {
            var Model = await _ashokLeyLandService.AddonOTCCardMapping(addAddOnCard);

            if (Model.StatusCode == 1000)
            {
                return RedirectToAction("SuccessAddonOTCCardMapping", new { Message = Model.Reason });
            }

            return View(Model);
        }

        [HttpPost]
        public async Task<JsonResult> GetAlSalesExeEmpIdAddOnOTCCardMapping(string dealerCode)
        {

            AddonOTCCardMapping addonOTCCardMapping = new AddonOTCCardMapping();
            addonOTCCardMapping = await _ashokLeyLandService.GetAlSalesExeEmpIdAddOnOTCCardMapping(dealerCode);

            return Json(addonOTCCardMapping);
        }
        public async Task<IActionResult> SuccessAddonOTCCardMapping(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }

    }
}
