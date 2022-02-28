﻿using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.Customer;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Models;
using HPCL.Common.Models.ViewModel.Officers;
using System;
using System.Linq;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Customer;
using Microsoft.AspNetCore.Mvc;
using HPCL_Web.Models;

namespace HPCL.Service.Services
{
    public class CustomerManageService:ICustomerManageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        public CustomerManageService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }
        public async Task<List<CustomerTypeModel>> GetCustomerType()
        {

            var CustType = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                    {"CTFlag",  "1" }
                };


            StringContent content = new StringContent(JsonConvert.SerializeObject(CustType), Encoding.UTF8, "application/json");

            var responseCustomerType = await _requestService.CommonRequestService(content, WebApiUrl.getCustomerType);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseCustomerType).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerTypeModel> lstCustomerType = jarr.ToObject<List<CustomerTypeModel>>();

            return lstCustomerType;

          

        }
        public async Task<List<CustomerZonalOfficeModel>> GetZonalOffice()
        {

            var ZonalOfficeForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")}
                };


            StringContent content = new StringContent(JsonConvert.SerializeObject(ZonalOfficeForms), Encoding.UTF8, "application/json");

            var responseZonalOffice = await _requestService.CommonRequestService(content, WebApiUrl.getZonalOffice);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseZonalOffice).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerZonalOfficeModel> lstZonalOffice = jarr.ToObject<List<CustomerZonalOfficeModel>>();

            return lstZonalOffice;

          
        }

        public async Task<List<CustomerTbentityModel>> GetCustomerTbentityModel()
        {

            var TBEntityForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")}
                };


            StringContent content = new StringContent(JsonConvert.SerializeObject(TBEntityForms), Encoding.UTF8, "application/json");

            var responseTBEntityForms = await _requestService.CommonRequestService(content, WebApiUrl.getTbentityName);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseTBEntityForms).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerTbentityModel> lstTbentity = jarr.ToObject<List<CustomerTbentityModel>>();

            return lstTbentity;


        }

        public async Task<List<CustomerStateModel>> GetCustomerState()
        {

            var CustomerStateForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")}
                };


            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerStateForms), Encoding.UTF8, "application/json");

            var responseState = await _requestService.CommonRequestService(content, WebApiUrl.getState);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseState).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerStateModel> lstState = jarr.ToObject<List<CustomerStateModel>>();

            return lstState;


        }
        public async Task<List<CustomerSecretQueModel>> GetCustomerSecretQue()
        {

            var CustomerSecretQueForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")}
                };


            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerSecretQueForms), Encoding.UTF8, "application/json");

            var responseSecretQue = await _requestService.CommonRequestService(content, WebApiUrl.getSecretQuestion);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseSecretQue).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerSecretQueModel> lstSecretQue = jarr.ToObject<List<CustomerSecretQueModel>>();

            return lstSecretQue;


        }
        public async Task<List<CustomerTypeOfFleetModel>> GetCustomerTypeofFlee()
        {

            var CustomerTypeOfFleetForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")}
                };


            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerTypeOfFleetForms), Encoding.UTF8, "application/json");

            var responseTypeofFlee = await _requestService.CommonRequestService(content, WebApiUrl.getTypeOfFleet);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseTypeofFlee).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerTypeOfFleetModel> lstTypeofFlee = jarr.ToObject<List<CustomerTypeOfFleetModel>>();

            return lstTypeofFlee;


        }
        public async Task<List<CustomerProfileResponse>> BindCustomerDetails(String CustomerId)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var CustType = new Dictionary<string, string>
                {
                     {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                    {"CustomerID", CustomerId},
                    {"NameOnCard","" }
                };

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(CustType), Encoding.UTF8, "application/json");

                var contentString = await _requestService.CommonRequestService(content, WebApiUrl.getCustomerDetails);

                //CustomerProfileResponse customerResponse = JsonConvert.DeserializeObject<CustomerProfileResponse>(contentString);
                JObject customerResponse = JObject.Parse(JsonConvert.DeserializeObject(contentString).ToString());

                var jarr = customerResponse["Data"].Value<JObject>();

                var customerResult = jarr["GetCustomerDetails"].Value<JArray>();
                List<CustomerProfileResponse> CustomerProfileResponse = customerResult.ToObject<List<CustomerProfileResponse>>();

                return CustomerProfileResponse;
            }
        }
        public async Task<List<SearchGridResponse>> CardDetails(String CustomerId)
        {
         

            var searchBody = new Dictionary<string, string>
            {
                { "UserId",CommonBase.userid },
                { "UserAgent", CommonBase.useragent},
                { "UserIp",CommonBase.userip},
                { "CustomerId",CustomerId},
               {  "CardNo","" },
               {  "MobileNumber","" },
               {  "VehicleNumber","" },
               {  "StatusFlag",  "-1" }
            };

            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
               // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
                var contentString = await _requestService.CommonRequestService(content, WebApiUrl.SearchCardUrl);

                JObject obj = JObject.Parse(JsonConvert.DeserializeObject(contentString).ToString());
                var jarr = obj["Data"].Value<JArray>();
                List<SearchGridResponse> searchList = jarr.ToObject<List<SearchGridResponse>>();


               // List<SearchGridResponse> searchList = JsonConvert.DeserializeObject<List<SearchGridResponse>>(contentString);
                return searchList;
            }
        }
    }
}