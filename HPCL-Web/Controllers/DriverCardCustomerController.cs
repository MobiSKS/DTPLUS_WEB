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

            return View(custMdl);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDriverCardCustomer(DriverCardCustomerModel driverCardCustomerModel)
        {
            return View(driverCardCustomerModel);
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
    }
}