using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPCL.Common.Models.ViewModel.DriverCardCustomer;
using HPCL.Common.Helper;
using HPCL.Common.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using HPCL.Common.Models.ResponseModel.Customer;
using System.Text.Json;
using HPCL.Service.Interfaces;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Resources;
using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ResponseModel.DriverCardCustomer;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class DriverCardCustomerController : Controller
    {

        private readonly IDriverCardCustomerService _driverCardCustomerService;
        private readonly ICommonActionService _commonActionService;
        public DriverCardCustomerController(IDriverCardCustomerService driverCardCustomerService, ICommonActionService commonActionService)
        {
            _driverCardCustomerService = driverCardCustomerService;
            _commonActionService = commonActionService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> CreateDriverCardCustomer()
        {
            DriverCardCustomerModel custMdl = new DriverCardCustomerModel();
            custMdl = await _driverCardCustomerService.CreateDriverCardCustomer();
            custMdl.Remarks = "";
            return View(custMdl);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDriverCardCustomer(DriverCardCustomerModel customerModel)
        {
            customerModel = await _driverCardCustomerService.CreateDriverCardCustomer(customerModel);

            if (customerModel.Internel_Status_Code == 1000)
            {
                customerModel.Remarks = "";
                ViewBag.Message = "Driver Card customer saved successfully";
                return RedirectToAction("SuccessRedirectForDriverCardCustomer");
            }

            return View(customerModel);
        }

        public async Task<IActionResult> RequestForDriverCard()
        {
            RequestForDriverCardModel custMdl = new RequestForDriverCardModel();
            custMdl = await _driverCardCustomerService.RequestForDriverCard();

            return View(custMdl);
        }

        [HttpPost]
        public async Task<IActionResult> RequestForDriverCard(RequestForDriverCardModel requestForDriverCardModel)
        {

            RequestForDriverCardModel request = await _driverCardCustomerService.RequestForDriverCard(requestForDriverCardModel);

            if (request.Internel_Status_Code == 1000)
            {
                requestForDriverCardModel.Remarks = "";
                ViewBag.Message = "Driver Card add request saved successfully";
                return RedirectToAction("SuccessRequestForDriverCard");
            }

            return View(requestForDriverCardModel);
        }


        public async Task<IActionResult> SuccessRequestForDriverCard()
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

        public async Task<IActionResult> DriverCardAllocation()
        {
            DriverCardAllocationToMerchantModel custMdl = new DriverCardAllocationToMerchantModel();
            custMdl = await _driverCardCustomerService.DriverCardAllocation();

            return View(custMdl);
        }

        [HttpPost]
        public async Task<JsonResult> GetAllUnAllocatedDriverCards(string RegionalId)
        {
            OTCUnAllocatedCardsResponse otcUnAllocatedCardsResponse = new OTCUnAllocatedCardsResponse();
            otcUnAllocatedCardsResponse = await _driverCardCustomerService.GetAllUnAllocatedDriverCards(RegionalId);

            return Json(new { NoOfUnAllocatedCard = otcUnAllocatedCardsResponse.ObjNoOfUnAllocatedCard, UnAllocatedCard = otcUnAllocatedCardsResponse.ObjUnAllocatedCard });
        }

        [HttpPost]
        public async Task<JsonResult> VerifyMerchantByMerchantidAndRegionalid(string RegionalId, string MerchantID)
        {
            CommonResponseData commonResponseData = new CommonResponseData();
            commonResponseData = await _commonActionService.VerifyMerchantByMerchantidAndRegionalid(RegionalId, MerchantID);

            return Json(commonResponseData);
        }

        [HttpPost]
        public async Task<IActionResult> SaveDriverCardsAllocation([FromBody] LinkCardsToMerchantModel linkCardsToMerchantModel)
        {
            CommonResponseData commonResponseData = new CommonResponseData();
            commonResponseData = await _driverCardCustomerService.SaveDriverCardsAllocation(linkCardsToMerchantModel);

            return Json(new { commonResponseData = commonResponseData });
        }
        public async Task<IActionResult> SuccessDriverCardsAllocation()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetAvailableDriverCardByRegionalId(string RegionalId, string MerchantID)
        {
            List<CardDetails> lstCardDetails = await _driverCardCustomerService.GetAvailableDriverCardByRegionalId(RegionalId, MerchantID);

            if (lstCardDetails != null)
            {
                return Json(new { lstCardDetails = lstCardDetails });
            }
            else
            {
                return Json("Failed to load Card Details");
            }
        }

        public async Task<IActionResult> SuccessRedirectForDriverCardCustomer()
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
        public async Task<JsonResult> GetCustomerNameByCustomerId(string CustomerID)
        {

            GetCustomerNameByIdResponse customerInfo = new GetCustomerNameByIdResponse();
            customerInfo = await _driverCardCustomerService.GetCustomerNameByCustomerId(CustomerID);

            return Json(customerInfo);
        }

        [HttpPost]
        public async Task<JsonResult> GetAllViewDriverCard(RequestForViewDriverCard entity)
        {
            var searchList = await _driverCardCustomerService.GetAllViewDriverCard(entity);

            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        public async Task<IActionResult> ViewRequestDriverCard()
        {
            RequestForDriverCardModel custMdl = new RequestForDriverCardModel();
            custMdl = await _driverCardCustomerService.ViewRequestDriverCard();

            return View(custMdl);
        }

        public async Task<IActionResult> ViewDriverCardsMerchatMapping()
        {
            ViewDriverCardMerchatMappingModel custMdl = new ViewDriverCardMerchatMappingModel();
            custMdl = await _driverCardCustomerService.ViewDriverCardsMerchatMapping();

            return View(custMdl);
        }

        public async Task<IActionResult> GetViewOTCCardMerchantAllocation(string MerchantId, string CardNo)
        {
            var modals = await _driverCardCustomerService.ViewDriverCardMerchantAllocation(MerchantId, CardNo);
            return PartialView("~/Views/DriverCardCustomer/_DriverCardMerchantAllocationTable.cshtml", modals);
        }
        public async Task<IActionResult> GetDriverCardActivationAllocationDetails(string zonalOfcID, string regionalOfcID, string fromDate, string toDate, string customerId)
        {
            var modals = await _driverCardCustomerService.GetDriverCardActivationAllocationDetails(zonalOfcID, regionalOfcID, fromDate, toDate, customerId);
            return PartialView("~/Views/DriverCardCustomer/_DriverCardActivAllocationTbl.cshtml", modals);
            

        }
        public async Task<IActionResult> DriverCardAllocationandActivation()
        {
            var modals = await _driverCardCustomerService.DriverCardAllocationandActivation();
            return View(modals);
        }

        public async Task<IActionResult> DealerDriverCardRequests()
        {
            DealerWiseDriverCardRequestModel custMdl = new DealerWiseDriverCardRequestModel();
            custMdl = await _driverCardCustomerService.DealerDriverCardRequests();

            return View(custMdl);
        }

        [HttpPost]
        public async Task<IActionResult> DealerDriverCardRequests(DealerWiseDriverCardRequestModel dealerWiseDriverCardRequestModel)
        {
            DealerWiseDriverCardRequestModel custMdl = new DealerWiseDriverCardRequestModel();
            custMdl = await _driverCardCustomerService.DealerDriverCardRequests(dealerWiseDriverCardRequestModel);

            if (custMdl.Internel_Status_Code == 1000)
            {
                custMdl.Remarks = "";
                ViewBag.Message = "Dealer wise Driver card request saved successfully";
                return RedirectToAction("SuccessRedirectDealerDriverCardRequest");
            }

            return View(custMdl);
        }

        public async Task<IActionResult> SuccessRedirectDealerDriverCardRequest()
        {
            return View();
        }

    }
}