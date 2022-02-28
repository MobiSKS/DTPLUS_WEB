using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.Officers;
using HPCL.Common.Resources;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class MyHpOTCCardCustomerController : Controller
    {

        private readonly IMyHpOTCCardCustomerService _myHpOTCCardCustomerService;
        private readonly ICommonActionService _commonActionService;
        public MyHpOTCCardCustomerController(IMyHpOTCCardCustomerService myHpOTCCardCustomerService, ICommonActionService commonActionService)
        {
            _myHpOTCCardCustomerService = myHpOTCCardCustomerService;
            _commonActionService = commonActionService;
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
            merchantDetailsResponse = await _myHpOTCCardCustomerService.GetMerchantDetailsByMerchantId(MerchantID);

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
            lstDistrict = await _commonActionService.GetDistrictDetails(Stateid);
            return Json(lstDistrict);
        }

        [HttpPost]
        public async Task<JsonResult> GetAvailableOTCCardByRegionalId(string RegionalId)
        {
            List<CardDetails> lstCardDetails = await _myHpOTCCardCustomerService.GetAvailableOTCCardByRegionalId(RegionalId);

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


    }
}
