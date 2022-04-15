using HPCL.Common.Models;
using HPCL.Common.Helper;

using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.Officers;
using HPCL.Common.Resources;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Models.RequestModel.MyHpOTCCardCustomer;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class MyHpOTCCardCustomerController : Controller
    {

        private readonly IMyHpOTCCardCustomerService _myHpOTCCardCustomerService;
        private readonly ICommonActionService _commonActionService;
        public MyHpOTCCardCustomerController(IMyHpOTCCardCustomerService myHpOTCCardCustomerService, ICommonActionService commonActionService)
        {
            _myHpOTCCardCustomerService = myHpOTCCardCustomerService;
            _commonActionService = commonActionService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> CustomerCardCreation()
        {
            MyHPOTCCardCustomerModel custMdl = new MyHPOTCCardCustomerModel();
            custMdl = await _myHpOTCCardCustomerService.CustomerCardCreation();

            return View(custMdl);
        }
        [HttpPost]
        public async Task<IActionResult> CustomerCardCreation(MyHPOTCCardCustomerModel customerModel)
        {
            //MyHPOTCCardCustomerModel custMdl = new MyHPOTCCardCustomerModel();
            customerModel = await _myHpOTCCardCustomerService.CustomerCardCreation(customerModel);

            if (customerModel.Internel_Status_Code == 1000)
            {
                customerModel.Remarks = "";
                ViewBag.Message = "OTC Card customer saved successfully";
                return RedirectToAction("SuccessRedirectForOTCCardCustomer");
            }

            return View(customerModel);
        }

        public async Task<IActionResult> RequestForOTCCard()
        {
            RequestForOTCCardModel custMdl = new RequestForOTCCardModel();
            custMdl.Remarks = "";
            custMdl = await _myHpOTCCardCustomerService.RequestForOTCCard();

            return View(custMdl);
        }

        [HttpPost]
        public async Task<IActionResult> RequestForOTCCard(RequestForOTCCardModel requestForOTCCardModel)
        {

            RequestForOTCCardModel request = await _myHpOTCCardCustomerService.RequestForOTCCard(requestForOTCCardModel);

            if (request.Internel_Status_Code == 1000)
            {
                requestForOTCCardModel.Remarks = "";
                ViewBag.Message = "OTC Card add request saved successfully";
                return RedirectToAction("SuccessRedirectForOTCCard");
            }

            return View(requestForOTCCardModel);
        }

        public async Task<IActionResult> SuccessRedirectForOTCCard()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GetMerchantDetailsByMerchantId(string MerchantID)
        {

            MerchantDetailsResponseOTCCardCustomer merchantDetailsResponse = new MerchantDetailsResponseOTCCardCustomer();
            merchantDetailsResponse = await _commonActionService.GetMerchantDetailsByMerchantId(MerchantID);

            if (merchantDetailsResponse.Internel_Status_Code.ToString() == Constants.SuccessInternelStatusCode)
            {
                return Json(merchantDetailsResponse);
            }
            else
            {
                return Json("Failed to load Merchant Details");
            }

        }

        [HttpPost]
        public async Task<JsonResult> CheckformnumberDuplication(string FormNumber)
        {
            CustomerInserCardResponseData customerInserCardResponseData = await _commonActionService.CheckformNumberDuplication(FormNumber);

            if (customerInserCardResponseData != null)
            {
                return Json(customerInserCardResponseData);
            }
            else
            {
                return Json("Failed to load Form Details");
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetDistrictDetails(string Stateid)
        {
            List<OfficerDistrictModel> lstDistrict = new List<OfficerDistrictModel>();
            if (Convert.ToInt32(string.IsNullOrEmpty(Stateid) ? "0" : Stateid) > 0)
            {
                lstDistrict = await _commonActionService.GetDistrictDetails(Stateid);
            }
            else
            {
                lstDistrict.Add(new OfficerDistrictModel
                {
                    districtID = 0,
                    districtName = "Select District"
                });
            }
            return Json(lstDistrict);
        }

        [HttpPost]
        public async Task<JsonResult> GetAvailableOTCCardByRegionalId(string RegionalId, string MerchantID)
        {
            List<CardDetails> lstCardDetails = await _myHpOTCCardCustomerService.GetAvailableOTCCardByRegionalId(RegionalId, MerchantID);

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
        public async Task<JsonResult> CheckPanNoDuplication(string PanNo)
        {
            CustomerInserCardResponseData customerInserCardResponseData = new CustomerInserCardResponseData();
            customerInserCardResponseData = await _commonActionService.CheckPanNoDuplication(PanNo);

            return Json(customerInserCardResponseData);
        }

        [HttpPost]
        public async Task<JsonResult> PANValidation(string PANNumber)
        {
            string data = await _commonActionService.PANValidation(PANNumber);
            return new JsonResult(data);
        }

        public async Task<IActionResult> SuccessRedirectForOTCCardCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CheckMobilNoDuplication(string MobileNo)
        {
            CustomerInserCardResponseData customerInserCardResponseData = await _commonActionService.CheckMobilNoDuplication(MobileNo);

            return Json(customerInserCardResponseData);
        }

        [HttpPost]
        public async Task<JsonResult> CheckEmailDuplication(string Emailid)
        {
            CustomerInserCardResponseData customerInserCardResponseData = await _commonActionService.CheckEmailDuplication(Emailid);

            return Json(customerInserCardResponseData);
        }

        [HttpPost]
        public async Task<JsonResult> VerifyMerchantByMerchantidAndRegionalid(string RegionalId, string MerchantID)
        {
            CommonResponseData commonResponseData = new CommonResponseData();
            commonResponseData = await _commonActionService.VerifyMerchantByMerchantidAndRegionalid(RegionalId, MerchantID);

            return Json(commonResponseData);
        }

        [HttpPost]
        public async Task<JsonResult> GetAllUnAllocatedCardsForOtcCard(string RegionalId)
        {
            OTCUnAllocatedCardsResponse otcUnAllocatedCardsResponse = new OTCUnAllocatedCardsResponse();
            otcUnAllocatedCardsResponse = await _myHpOTCCardCustomerService.GetAllUnAllocatedCardsForOtcCard(RegionalId);

            return Json(new { NoOfUnAllocatedCard = otcUnAllocatedCardsResponse.ObjNoOfUnAllocatedCard, UnAllocatedCard = otcUnAllocatedCardsResponse.ObjUnAllocatedCard });
        }

        public async Task<IActionResult> OTCCardsAllocation()
        {
            MIDAllocationOfCardsModel custMdl = new MIDAllocationOfCardsModel();
            custMdl = await _myHpOTCCardCustomerService.OTCCardsAllocation();

            return View(custMdl);
        }

        public async Task<IActionResult> GetAllViewCardsForOtcCard(string RegionalId)
        {
            var modals = await _myHpOTCCardCustomerService.GetAllViewCardsForOtcCard(RegionalId);
            return PartialView("~/Views/MyHpOTCCardCustomer/_ViewOTCCardsTable.cshtml", modals);
        }

        public async Task<IActionResult> ViewOTCCards()
        {
            MIDAllocationOfCardsModel custMdl = new MIDAllocationOfCardsModel();
            custMdl = await _myHpOTCCardCustomerService.OTCCardsAllocation();

            return View(custMdl);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOTCCardsAllocation([FromBody] LinkCardsToMerchantModel linkCardsToMerchantModel)
        {
            CommonResponseData commonResponseData = new CommonResponseData();
            commonResponseData = await _myHpOTCCardCustomerService.SaveOTCCardsAllocation(linkCardsToMerchantModel);

            return Json(new { commonResponseData = commonResponseData });
        }

        public async Task<IActionResult> SuccessOTCCardsAllocation()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CheckVehicleRegistrationValid(string RegistrationNumber)
        {
            var data = await _commonActionService.CheckVehicleRegistrationValid(RegistrationNumber);
            return new JsonResult(data);
        }

        public async Task<IActionResult> GetCardAllocationActivation()
        {
            var modals = await _myHpOTCCardCustomerService.GetCardAllocationActivation();
            return View(modals);
        }

        public async Task<IActionResult> ViewOTCCardsMerchatMapping()
        {
            ViewOTCCardsMerchatMappingModel custMdl = new ViewOTCCardsMerchatMappingModel();
            custMdl = await _myHpOTCCardCustomerService.ViewOTCCardsMerchatMapping();

            return View(custMdl);
        }

        public async Task<IActionResult> GetViewOTCCardMerchantAllocation(string MerchantId, string CardNo)
        {
            var modals = await _myHpOTCCardCustomerService.ViewOTCCardMerchantAllocation(MerchantId, CardNo);
            return PartialView("~/Views/MyHpOTCCardCustomer/_OTCCardMerchantAllocationTable.cshtml", modals);
        }
        public async Task<IActionResult> SearchCardActivationandAllocation(GetCardAllocationActivation entity)
        {
            var modals = await _myHpOTCCardCustomerService.SearchCardActivationandAllocation(entity);
            return PartialView("~/Views/MyHpOTCCardCustomer/_MyCardActivationandAllocationTable.cshtml", modals);
        }
        public async Task<IActionResult> MyHPOTCCardAllocationandActivation()
        {
            var modals = await _myHpOTCCardCustomerService.MyHPOTCCardAllocationandActivation();
            return View(modals);
        }

        public async Task<IActionResult> DealerOTCCardRequests()
        {
            var modals = await _myHpOTCCardCustomerService.DealerOTCCardRequests();
            return View(modals);
        }

        [HttpPost]
        public async Task<IActionResult> DealerOTCCardRequests(DealerWiseMyHPOTCCardRequestModel dealerWiseMyHPOTCCardRequestModel)
        {

            DealerWiseMyHPOTCCardRequestModel request = await _myHpOTCCardCustomerService.DealerOTCCardRequests(dealerWiseMyHPOTCCardRequestModel);

            if (request.Internel_Status_Code == 1000)
            {
                dealerWiseMyHPOTCCardRequestModel.Remarks = "";
                ViewBag.Message = "OTC Card add request saved successfully";
                return RedirectToAction("SuccessRedirectDealerOTCCardRequest");
            }

            return View(dealerWiseMyHPOTCCardRequestModel);
        }

        public async Task<IActionResult> SuccessRedirectDealerOTCCardRequest()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CheckMobilNoAlreadyUsedForOTCCard(string MobileNo)
        {
            var Model = await _myHpOTCCardCustomerService.CheckMobilNoAlreadyUsedForOTCCard(MobileNo);

            return Json(Model);
        }

        [HttpPost]
        public async Task<JsonResult> CheckVechileNoUsed(string VechileNo)
        {
            var Model = await _commonActionService.CheckVechileNoUsed(VechileNo);

            return Json(Model);
        }

    }
}
