using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.AshokLeyLand;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
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
                return RedirectToAction("SuccessRedirectDealerOTCCardRequest", new { Message = alOTCCardRequestModel.Remarks });
            }

            return View(alOTCCardRequestModel);
        }

        public async Task<IActionResult> SuccessRedirectDealerOTCCardRequest(string Message)
        {
            ViewBag.Message = Message;
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
            List<OTCCardDetails> lstCardDetails = await _ashokLeyLandService.GetAvailableAlOTCCardForDealer(DealerCode);

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
                return RedirectToAction("SuccessRedirectCreateMultipleOTCCard", new { Message = ashokLeylandCardCreationModel.Remarks });
            }

            return View(ashokLeylandCardCreationModel);
        }
        public async Task<IActionResult> SuccessRedirectCreateMultipleOTCCard(string Message)
        {
            ViewBag.Message = Message;
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
            var modals = await _ashokLeyLandService.AddonOTCCardMapping();
            return View(modals);
        }

        [HttpPost]
        public async Task<JsonResult> GetAlAddonOTCCardMappingCustomerDetails(string customerId)
        {
            var searchResult = await _ashokLeyLandService.GetAlAddonOTCCardMappingCustomerDetails(customerId);
            return Json(new { searchResult = searchResult });
        }
        public async Task<IActionResult> GetAlAddonOTCCardCustomerDetailsPartialView([FromBody] List<AddonOTCCardDetails> objCardDetails)
        {
            var modals = await _ashokLeyLandService.GetAlAddonOTCCardCustomerDetailsPartialView(objCardDetails);
            return PartialView("~/Views/AshokLeyLand/_AddonOTCCardCustomerDetailsTbl.cshtml", modals);
        }
        public async Task<IActionResult> GetAlAddonOTCCardAddCardsPartialView([FromBody] List<AddonOTCCardDetails> objCardDetails)
        {
            var modals = await _ashokLeyLandService.GetAlAddonOTCCardAddCardsPartialView(objCardDetails);
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

        public async Task<IActionResult> UpdateALCustomer()
        {
            var modals = await _ashokLeyLandService.UpdateALCustomer();
            return View(modals);
        }

        [HttpPost]
        public async Task<JsonResult> GetCustomerAddress(string CustomerId)
        {
            var modals = await _ashokLeyLandService.GetCustomerAddress(CustomerId);
            return Json(new { customer = modals });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateALCustomer(AshokLeylandCustomerUpdateModel model)
        {

            var modals = await _ashokLeyLandService.UpdateALCustomer(model);

            if (model.Internel_Status_Code == 1000)
            {
                return RedirectToAction("SuccessRedirectUpdateALCustomer", new { Message = model.Message });
            }

            return View(modals);
        }
        public async Task<IActionResult> SuccessRedirectUpdateALCustomer(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }
        public async Task<IActionResult> GetMultipleOTCCardPartialView([FromBody] List<ALCardEntryDetails> objCardDetails)
        {
            var modals = await _ashokLeyLandService.GetMultipleOTCCardPartialView(objCardDetails);
            return PartialView("~/Views/AshokLeyLand/_MultipleOTCCardVehicleDetailsTbl.cshtml", modals);
        }
        public async Task<IActionResult> VerifyCustomerDocuments(GetAlCustomerDetailForVerificationModel model, string reset, string success, string error, string StateID, string FormNumber, string CustomerName, string Status)
        {
            var searchResult = await _ashokLeyLandService.VerifyCustomerDocuments(model);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            ViewBag.SuccessMessage = success;
            ViewBag.ErrorMessage = error;
            if (!String.IsNullOrEmpty(reset))
            {
                model.StateID = 0;
            }
            return View(searchResult);
        }
        [HttpPost]
        public async Task<JsonResult> AproveRejectCustomer(string CustomerID, string Comments, string Approvalstatus)
        {
            var updateKycResponse = await _ashokLeyLandService.AproveRejectCustomer(CustomerID, Comments, Approvalstatus);
            return Json(new { customer = updateKycResponse });
        }
        public async Task<IActionResult> ManageProfile()
        {
            var custMdl = await _ashokLeyLandService.ManageProfile();

            return View(custMdl);
        }
        [HttpPost]
        public async Task<JsonResult> CheckVINNoUsed(string VinNo)
        {
            var Model = await _commonActionService.CheckVINNoUsed(VinNo);

            return Json(Model);
        }

        [HttpPost]
        public async Task<JsonResult> BindCustomerDetailsForSearch(string CustomerId, string NameOnCard)
        {
            var customerCardInfo = await _ashokLeyLandService.BindCustomerDetailsForSearch(CustomerId, NameOnCard);
            ModelState.Clear();
            return Json(customerCardInfo);
        }
        [HttpPost]
        public async Task<JsonResult> CardDetailsForSearch(String CustomerId, String CustomerTypeId)
        {
            var customerCardInfo = await _ashokLeyLandService.CardDetailsForSearch(CustomerId, CustomerTypeId);
            ModelState.Clear();
            return Json(customerCardInfo);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateAlCostomerProfile(string str)
        {
            var result = await _ashokLeyLandService.UpdateAlCostomerProfile(str);
            return Json(new { result = result });
        }
        [HttpPost]
        public async Task<JsonResult> GetRegionalOfcDetails(string zonalOfcId)
        {
            var sortedtList = await _commonActionService.GetRegionalOfficeList(zonalOfcId);

            ModelState.Clear();
            return Json(sortedtList);
        }

        [HttpPost]
        public async Task<JsonResult> GetAttachmentDetails(string CustomerID)
        {
            UploadALDoc uploadDoc = new UploadALDoc();
            uploadDoc.CustomerID = CustomerID;
            uploadDoc.AddressProofFront = "";
            uploadDoc.IdProofFront = "";
            uploadDoc.PanCardFront = "";
            uploadDoc.VehicleDetailsFront = "";
            uploadDoc.CustomerFormProofFront = "";
            uploadDoc.Reason = "";
            uploadDoc.Status = 1;

            if (!string.IsNullOrEmpty(CustomerID))
            {
                uploadDoc.CustomerID = CustomerID;

                var response = await _ashokLeyLandService.UploadALDoc(CustomerID);

                if (response != null && response.Data != null && response.Data.Count > 0)
                {
                    if (!string.IsNullOrEmpty(response.Data[0].AddressProofFront) && response.Data[0].AddressProofFront.ToUpper() != "FILE DOES NOT EXISTS")
                        uploadDoc.AddressProofFront = response.Data[0].AddressProofFront;
                    if (!string.IsNullOrEmpty(response.Data[0].IdProofFront) && response.Data[0].IdProofFront.ToUpper() != "FILE DOES NOT EXISTS")
                        uploadDoc.IdProofFront = response.Data[0].IdProofFront;
                    if (!string.IsNullOrEmpty(response.Data[0].PanCardFront) && response.Data[0].PanCardFront.ToUpper() != "FILE DOES NOT EXISTS")
                        uploadDoc.PanCardFront = response.Data[0].PanCardFront;
                    if (!string.IsNullOrEmpty(response.Data[0].VehicleDetailsFront) && response.Data[0].VehicleDetailsFront.ToUpper() != "FILE DOES NOT EXISTS")
                        uploadDoc.VehicleDetailsFront = response.Data[0].VehicleDetailsFront;
                    if (!string.IsNullOrEmpty(response.Data[0].CustomerFormProofFront) && response.Data[0].CustomerFormProofFront.ToUpper() != "FILE DOES NOT EXISTS")
                        uploadDoc.CustomerFormProofFront = response.Data[0].CustomerFormProofFront;
                    if (!string.IsNullOrEmpty(response.Data[0].Reason))
                        uploadDoc.Reason = response.Data[0].Reason;
                    uploadDoc.Status = response.Data[0].Status;
                }
            }

            ModelState.Clear();
            return Json(uploadDoc);
        }
        
        public async Task<IActionResult> UploadALDoc(string CustomerID)
        {
            UploadALDoc uploadDoc = new UploadALDoc();
            if (!string.IsNullOrEmpty(CustomerID))
            {
                uploadDoc.CustomerID = CustomerID;
            }

            return View(uploadDoc);
        }

        [HttpPost]
        public async Task<JsonResult> SaveUploadDoc(UploadALDoc entity)
        {
            var reason = await _ashokLeyLandService.SaveUploadDoc(entity);

            ModelState.Clear();
            return Json(reason);
        }
        public async Task<IActionResult> ALPendingKYCCustomerDetail(PendingKYCCustomerDetailsModel model, string reset, string success, string error, string CustomerID)
        {
            var searchResult = await _ashokLeyLandService.ALPendingKYCCustomerDetail(model);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            ViewBag.SuccessMessage = success;
            ViewBag.ErrorMessage = error;

            return View(searchResult);
        }

        [HttpPost]
        public async Task<JsonResult> ResetAlDealerPassword(string UserName)
        {
            var result = await _ashokLeyLandService.ResetAlDealerPassword(UserName);
            return Json(new { result = result });
        }

    }
}
