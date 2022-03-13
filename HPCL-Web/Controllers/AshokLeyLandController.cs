﻿using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.AshokLeyLand;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

            //alOTCCardRequestModel = await _ashokLeyLandService.CreateMultipleOTCCard(alOTCCardRequestModel);

            //if (alOTCCardRequestModel.Internel_Status_Code == 1000)
            //{
            //    alOTCCardRequestModel.Remarks = "";
            //    ViewBag.Message = "AL OTC Card request saved successfully";
            //    return RedirectToAction("SuccessRedirectDealerOTCCardRequest");
            //}

            return View(ashokLeylandCardCreationModel);
        }

    }
}
