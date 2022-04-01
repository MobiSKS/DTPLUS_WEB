﻿using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.TatkalCardCustomer;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.TatkalCardCustomer;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class TatkalCardCustomerController : Controller
    {
        private readonly ITatkalCardCustomerService _tatkalCardCustomerService;
        private readonly ICommonActionService _commonActionService;
        public TatkalCardCustomerController(ITatkalCardCustomerService tatkalCardCustomerService, ICommonActionService commonActionService)
        {
            _tatkalCardCustomerService = tatkalCardCustomerService;
            _commonActionService = commonActionService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> RequestForTatkalCard()
        {
            TatkalCustomerCardRequestInfo custMdl = new TatkalCustomerCardRequestInfo();
            custMdl = await _tatkalCardCustomerService.RequestForTatkalCard();

            return View(custMdl);
        }

        [HttpPost]
        public async Task<IActionResult> RequestForTatkalCard(TatkalCustomerCardRequestInfo tatkalCustomerCardRequestInfo)
        {

            TatkalCustomerCardRequestInfo request = await _tatkalCardCustomerService.RequestForTatkalCard(tatkalCustomerCardRequestInfo);

            if (request.Internel_Status_Code == 1000)
            {
                tatkalCustomerCardRequestInfo.Remarks = "";
                ViewBag.Message = "Tatkal Card add request saved successfully";
                return RedirectToAction("SuccessRedirectForTatkalCard");
            }

            return View(tatkalCustomerCardRequestInfo);
        }


        public async Task<IActionResult> SuccessRedirectForTatkalCard()
        {
            return View();
        }

        public async Task<IActionResult> CreateTatkalCustomer()
        {
            TatkalCardCustomerModel custMdl = new TatkalCardCustomerModel();
            custMdl = await _tatkalCardCustomerService.CreateTatkalCustomer();

            return View(custMdl);
        }

        [HttpPost]
        public async Task<JsonResult> GetAllTatkalCustomerCard(TatkalViewRequest entity)
        {
            var searchList = await _tatkalCardCustomerService.GetAllTatkalCustomerCard(entity);

            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        public async Task<IActionResult> ViewAllocatedMapCard()
        {
            TatkalViewRequestModel custMdl = new TatkalViewRequestModel();
            custMdl = await _tatkalCardCustomerService.ViewAllocatedMapCard();
            return View(custMdl);
        }
        [HttpPost]
        public async Task<JsonResult> GetRegionalDetailsDropDown(int ZonalOfficeID)
        {
            List<CustomerRegionModel> lstCustomerRegion = new List<CustomerRegionModel>();
            lstCustomerRegion = await _tatkalCardCustomerService.GetRegionalDetailsDropDown(ZonalOfficeID);
            return Json(lstCustomerRegion);
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
        public async Task<IActionResult> CreateTatkalCustomer(TatkalCardCustomerModel customerModel)
        {
            customerModel = await _tatkalCardCustomerService.CreateTatkalCustomer(customerModel);

            if (customerModel.Internel_Status_Code == 1000)
            {
                customerModel.Remarks = "";
                ViewBag.Message = "Tatkal Card customer saved successfully";
                return RedirectToAction("SuccessRedirectForTatkalCardCustomer");
            }

            return View(customerModel);
        }
        public async Task<IActionResult> SuccessRedirectForTatkalCardCustomer()
        {
            return View();
        }

        public async Task<IActionResult> ViewRequestedTatkalCard()
        {
            ViewRequestedTatkalCardModel custMdl = new ViewRequestedTatkalCardModel();
            custMdl = await _tatkalCardCustomerService.ViewRequestedTatkalCard();

            return View(custMdl);
        }

        [HttpPost]
        public async Task<JsonResult> GetViewRequestedTatkalCard(int RegionalId)
        {
            var searchList = await _tatkalCardCustomerService.GetViewRequestedTatkalCard(RegionalId);

            ModelState.Clear();
            return Json(new { searchList = searchList });
        }
        public async Task<IActionResult> MapTatkalCardtoTatkalCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetMapTatkalCardtoCustomer()
        {
            var searchList = await _tatkalCardCustomerService.GetMapTatkalCardtoCustomer();
            ModelState.Clear();
            return Json(searchList);
            //return PartialView("~/Views/TatkalCardCustomer/_TatkalCardsView.cshtml", searchList);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTatkalCardtoCustomer([FromBody] MapTatkalCardtoCustomerUpdateModel UpdateDetails)
        {
            var result = await _tatkalCardCustomerService.UpdateTatkalCardtoCustomer(UpdateDetails);
            return Json(result);
        }
    }
}
