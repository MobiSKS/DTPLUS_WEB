﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Service.Interfaces;
using HPCL.Common.Models.CommonEntity;
using HPCL_Web.Models;

namespace HPCL_Web.Controllers
{
    public class CustomerManageController : Controller
    {
        private readonly ICustomerManageService _customerManageService;
        public CustomerManageController(ICustomerManageService customerManageService)
        {
            _customerManageService = customerManageService;
        }
       
        HelperAPI _api = new HelperAPI();
        public IActionResult Index()
        {
            return View();
        }
        #region Customer Manage Profile
        [HttpPost]
        public async Task<JsonResult> BindCustomerDetails(String CustomerId)
        {
            //List<CustomerProfileResponse> customerCardInfo = new List<CustomerProfileResponse>();

            var customerCardInfo = await _customerManageService.BindCustomerDetails(CustomerId);
            ModelState.Clear();
            return Json(customerCardInfo);
           // return Json(new { customerCardInfo = customerCardInfo });
        }

        [HttpPost]
        public async Task<JsonResult> CardDetails(String CustomerId)
        {
            //List<SearchGridResponse> customerCardInfo = new List<SearchGridResponse>();

            var customerCardInfo = await _customerManageService.CardDetails(CustomerId);
            ModelState.Clear();
            return Json(customerCardInfo);
        }

        public async Task<IActionResult> ManageProfile()
        {
            CustomerProfileModel custMdl = new CustomerProfileModel();

            List<CustomerTypeModel> lstCustomerType = new List<CustomerTypeModel>();
            lstCustomerType = await _customerManageService.GetCustomerType();
            custMdl.CustomerTypeMdl.AddRange(lstCustomerType);

            List<CustomerZonalOfficeModel> lstCustomerZones = new List<CustomerZonalOfficeModel>();
            lstCustomerZones = await _customerManageService.GetZonalOffice();
            custMdl.CustomerZonalOfficeMdl.AddRange(lstCustomerZones);

            List<CustomerTbentityModel> lstTbentity = new List<CustomerTbentityModel>();
            lstTbentity = await _customerManageService.GetCustomerTbentityModel();
            custMdl.CustomerTbentityMdl.AddRange(lstTbentity);

            List<CustomerStateModel> lstState = new List<CustomerStateModel>();
            lstState = await _customerManageService.GetCustomerState();
            custMdl.CustomerStateMdl.AddRange(lstState);


            List<CustomerSecretQueModel> lstSecretQue = new List<CustomerSecretQueModel>();
            lstSecretQue = await _customerManageService.GetCustomerSecretQue();
            custMdl.CustomerSecretQueMdl.AddRange(lstSecretQue);

            List<CustomerTypeOfFleetModel> lstTypeofFlee = new List<CustomerTypeOfFleetModel>();
            lstTypeofFlee = await _customerManageService.GetCustomerTypeofFlee();
            custMdl.CustomerTypeOfFleetMdl.AddRange(lstTypeofFlee);

            return View(custMdl);

        }


        #endregion
    }
}