using HPCL.Common.Helper;
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
using HPCL.Common.Models.ResponseModel.CustomerManage;
using HPCL.Common.Models.ViewModel.CustomerManage;

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
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
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
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserId")}
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
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserId")}
                };


            StringContent content = new StringContent(JsonConvert.SerializeObject(TBEntityForms), Encoding.UTF8, "application/json");

            var responseTBEntityForms = await _requestService.CommonRequestService(content, WebApiUrl.getTbentityName);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseTBEntityForms).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerTbentityModel> lstTbentity = jarr.ToObject<List<CustomerTbentityModel>>();

            return lstTbentity;


        }
        public async Task<List<CustomerSecretQueModel>> GetCustomerSecretQue()
        {

            var CustomerSecretQueForms = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserId")}
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
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserId")}
                };


            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerTypeOfFleetForms), Encoding.UTF8, "application/json");

            var responseTypeofFlee = await _requestService.CommonRequestService(content, WebApiUrl.getTypeOfFleet);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseTypeofFlee).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerTypeOfFleetModel> lstTypeofFlee = jarr.ToObject<List<CustomerTypeOfFleetModel>>();

            return lstTypeofFlee;


        }
        public async Task<List<CustomerProfileResponse>> BindCustomerDetails(string CustomerId, string NameOnCard)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {
                var searchBody = new CustomerProfileModel();
                if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
                {
                    searchBody = new CustomerProfileModel
                    {
                        UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                        UserAgent = CommonBase.useragent,
                        UserIp = CommonBase.userip,
                        CustomerId = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                    };
                }
                else
                {
                    searchBody = new CustomerProfileModel
                    {
                        UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                        UserAgent = CommonBase.useragent,
                        UserIp = CommonBase.userip,
                        CustomerId = string.IsNullOrEmpty(CustomerId) ? "" : CustomerId,
                        NameOnCard = string.IsNullOrEmpty(NameOnCard) ? "" : NameOnCard
                    };
                }

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                var contentString = await _requestService.CommonRequestService(content, WebApiUrl.getCustomerDetails);

                //CustomerProfileResponse customerResponse = JsonConvert.DeserializeObject<CustomerProfileResponse>(contentString);
                JObject customerResponse = JObject.Parse(JsonConvert.DeserializeObject(contentString).ToString());

                var jarr = customerResponse["Data"].Value<JObject>();

                var customerResult = jarr["GetCustomerDetails"].Value<JArray>();
                List<CustomerProfileResponse> customerProfileResponse = customerResult.ToObject<List<CustomerProfileResponse>>();

                if (customerProfileResponse != null && customerProfileResponse.Count > 0)
                {
                    foreach (CustomerProfileResponse response in customerProfileResponse)
                    {
                        if (string.IsNullOrEmpty(response.AreaOfOperation))
                        {
                            response.AreaOfOperation = "";
                        }
                        if (!string.IsNullOrEmpty(response.CommunicationPhoneNo))
                        {
                            string[] subs = response.CommunicationPhoneNo.Split("-");

                            if (subs.Count() > 1)
                            {
                                response.CommunicationDialCode = subs[0].ToString();
                                response.CommunicationPhoneNo = subs[1].ToString();
                            }
                            else
                            {
                                response.CommunicationDialCode = "";
                                response.CommunicationPhoneNo = "";
                            }
                        }

                        if (!string.IsNullOrEmpty(response.CommunicationFax))
                        {
                            string[] subs = response.CommunicationFax.Split("-");

                            if (subs.Count() > 1)
                            {
                                response.CommunicationFaxCode = subs[0].ToString();
                                response.CommunicationFax = subs[1].ToString();
                            }
                            else
                            {
                                response.CommunicationFaxCode = "";
                                response.CommunicationFax = "";
                            }
                        }

                        if (!string.IsNullOrEmpty(response.PermanentPhoneNo))
                        {
                            string[] subs = response.PermanentPhoneNo.Split("-");

                            if (subs.Count() > 1)
                            {
                                response.PerOrRegAddressDialCode = subs[0].ToString();
                                response.PermanentPhoneNo = subs[1].ToString();
                            }
                            else
                            {
                                response.PerOrRegAddressDialCode = "";
                                response.PermanentPhoneNo = "";
                            }
                        }

                        if (!string.IsNullOrEmpty(response.PermanentFax))
                        {
                            string[] subs = response.PermanentFax.Split("-");

                            if (subs.Count() > 1)
                            {
                                response.PermanentFaxCode = subs[0].ToString();
                                response.PermanentFax = subs[1].ToString();
                            }
                            else
                            {
                                response.PermanentFaxCode = "";
                                response.PermanentFax = "";
                            }
                        }

                        if (!string.IsNullOrEmpty(response.KeyOfficialFax))
                        {
                            string[] subs = response.KeyOfficialFax.Split("-");

                            if (subs.Count() > 1)
                            {
                                response.KeyOffFaxCode = subs[0].ToString();
                                response.KeyOffFax = subs[1].ToString();
                            }
                            else
                            {
                                response.KeyOffFaxCode = "";
                                response.KeyOffFax = "";
                            }
                        }

                        if (!string.IsNullOrEmpty(response.KeyOfficialPhoneNo))
                        {
                            string[] subs = response.KeyOfficialPhoneNo.Split("-");

                            if (subs.Count() > 1)
                            {
                                response.KeyOffDialCode = subs[0].ToString();
                                response.KeyOfficialPhoneNo = subs[1].ToString();
                            }
                            else
                            {
                                response.KeyOffDialCode = "";
                                response.KeyOfficialPhoneNo = "";
                            }
                        }

                        if (response.FleetSizeNoOfVechileOwnedHCV == "0")
                            response.FleetSizeNoOfVechileOwnedHCV = "";
                        response.FleetSizeNoOfVechileOwnedLCV = (string.IsNullOrEmpty(response.FleetSizeNoOfVechileOwnedLCV) ? "" : response.FleetSizeNoOfVechileOwnedLCV);
                        if (response.FleetSizeNoOfVechileOwnedLCV == "0")
                            response.FleetSizeNoOfVechileOwnedLCV = "";
                        response.FleetSizeNoOfVechileOwnedMUVSUV = (string.IsNullOrEmpty(response.FleetSizeNoOfVechileOwnedMUVSUV) ? "" : response.FleetSizeNoOfVechileOwnedMUVSUV);
                        if (response.FleetSizeNoOfVechileOwnedMUVSUV == "0")
                            response.FleetSizeNoOfVechileOwnedMUVSUV = "";
                        response.FleetSizeNoOfVechileOwnedCarJeep = (string.IsNullOrEmpty(response.FleetSizeNoOfVechileOwnedCarJeep) ? "" : response.FleetSizeNoOfVechileOwnedCarJeep);
                        if (response.FleetSizeNoOfVechileOwnedCarJeep == "0")
                            response.FleetSizeNoOfVechileOwnedCarJeep = "";

                        if (!string.IsNullOrEmpty(response.KeyOfficialDOA))
                        {
                            if (!response.KeyOfficialDOA.Contains("1900"))
                            {
                                string[] subs = response.KeyOfficialDOA.Split(' ');
                                string[] date = subs[0].Split('/');
                                response.KeyOfficialDOA = date[1] + "-" + date[0] + "-" + date[2];
                            }
                            else
                            {
                                response.KeyOfficialDOA = "";
                            }
                        }

                        if (!string.IsNullOrEmpty(response.KeyOfficialDOB))
                        {
                            if (!response.KeyOfficialDOB.Contains("1900"))
                            {
                                string[] subs = response.KeyOfficialDOB.Split(' ');
                                string[] date = subs[0].Split('/');
                                response.KeyOfficialDOB = date[1] + "-" + date[0] + "-" + date[2];
                            }
                            else
                            {
                                response.KeyOfficialDOB = "";
                            }
                        }
                        if (string.IsNullOrEmpty(response.NameOnCard))
                        {
                            response.NameOnCard = "";
                        }
                    }
                }

                return customerProfileResponse;
            }
        }
        public async Task<List<SearchGridResponse>> CardDetails(String CustomerId, String CustomerTypeId)
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

                if (CustomerTypeId == "927")//DriverCard
                {
                    if (searchList != null && searchList.Count > 0)
                    {
                        foreach (SearchGridResponse item in searchList)
                        {
                            item.VechileNo = "DRIVER CARD";
                        }
                    }
                }

                // List<SearchGridResponse> searchList = JsonConvert.DeserializeObject<List<SearchGridResponse>>(contentString);
                return searchList;
            }
        }

        public async Task<AddOnCustomerModel> AddOnCustomer()
        {
            AddOnCustomerModel custMdl = new AddOnCustomerModel();
            custMdl.Remarks = "";
            custMdl.Success = "";
            custMdl.Message = "";

            return custMdl;
        }
        public async Task<AddOnCustomerModel> AddOnCustomer(AddOnCustomerModel model)
        {
            model.UserAgent = CommonBase.useragent;
            model.UserIp = CommonBase.userip;
            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            CustomerInserCardResponse updateResponse;

            var responseCustomer = await _requestService.CommonRequestService(content, WebApiUrl.customerAddOnUser);

            updateResponse = JsonConvert.DeserializeObject<CustomerInserCardResponse>(responseCustomer);
                        
            model.Success = "";
            if (updateResponse.Internel_Status_Code == 1000)
            {
                model.StatusCode = updateResponse.Internel_Status_Code;
                model.Message = updateResponse.Message;
                if (updateResponse != null && updateResponse.Data != null && updateResponse.Data.Count > 0)
                {
                    model.Status = updateResponse.Data[0].Status;
                    model.Message = updateResponse.Data[0].Reason;

                    if (model.Status == 1)
                    {
                        model.Success = updateResponse.Data[0].Reason;
                        model.Message = "";
                    }
                }
            }
            else
            {
                model.Message = updateResponse.Message;
                model.StatusCode = updateResponse.Internel_Status_Code;
                if (updateResponse != null && updateResponse.Data != null && updateResponse.Data.Count > 0)
                {
                    model.Status = updateResponse.Data[0].Status;
                    model.Message = updateResponse.Data[0].Reason;
                    model.Success = "";
                }
            }

            return model;
        }

    }
}
