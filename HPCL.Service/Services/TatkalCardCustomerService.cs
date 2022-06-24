using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ResponseModel.TatkalCardCustomer;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.TatkalCardCustomer;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Models.RequestModel.MyHpOTCCardCustomer;
using Microsoft.AspNetCore.Mvc;
using HPCL.Common.Models.RequestModel.TatkalCardCustomer;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace HPCL.Service.Services
{
    public class TatkalCardCustomerService : ITatkalCardCustomerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        private readonly IConfiguration _configuration;

        public TatkalCardCustomerService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
            _configuration = configuration;
        }
        public async Task<TatkalCustomerCardRequestInfo> RequestForTatkalCard()
        {
            TatkalCustomerCardRequestInfo custMdl = new TatkalCustomerCardRequestInfo();
            custMdl.Remarks = "";
            custMdl.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());

            return custMdl;
        }

        public async Task<TatkalCustomerCardRequestInfo> RequestForTatkalCard(TatkalCustomerCardRequestInfo tatkalCustomerCardRequestInfo)
        {
            tatkalCustomerCardRequestInfo.UserAgent = CommonBase.useragent;
            tatkalCustomerCardRequestInfo.UserIp = CommonBase.userip;
            tatkalCustomerCardRequestInfo.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            tatkalCustomerCardRequestInfo.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            StringContent content = new StringContent(JsonConvert.SerializeObject(tatkalCustomerCardRequestInfo), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertTatkalCardRequest);


            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            tatkalCustomerCardRequestInfo.Internel_Status_Code = customerResponse.Internel_Status_Code;
            tatkalCustomerCardRequestInfo.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null && customerResponse.Data.Count > 0)
                    tatkalCustomerCardRequestInfo.Remarks = customerResponse.Data[0].Reason;
                else
                    tatkalCustomerCardRequestInfo.Remarks = customerResponse.Message;

                tatkalCustomerCardRequestInfo.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());
            }
            else
            {
                if (customerResponse.Data != null && customerResponse.Data.Count > 0)
                    tatkalCustomerCardRequestInfo.Remarks = customerResponse.Data[0].Reason;
                else
                    tatkalCustomerCardRequestInfo.Remarks = customerResponse.Message;
            }

            return tatkalCustomerCardRequestInfo;
        }

        public async Task<TatkalCardCustomerModel> CreateTatkalCustomer()
        {
            TatkalCardCustomerModel custModel = new TatkalCardCustomerModel();
            custModel.Remarks = "";
            custModel.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
            custModel.SBUTypeID = 1;
            custModel.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficebySBUType(custModel.SBUTypeID.ToString()));
            custModel.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
            custModel.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());
            custModel.ExternalPANAPIStatus = _configuration.GetSection("ExternalAPI:PANAPI").Value.ToString();
            if (string.IsNullOrEmpty(custModel.ExternalPANAPIStatus))
            {
                custModel.ExternalPANAPIStatus = "Y";
            }

            return custModel;
        }
        public async Task<List<TatkalCardCustomerViewResponse>> GetAllTatkalCustomerCard(TatkalViewRequest entity)
        {
            var searchBody = new TatkalViewRequest();
            if (entity.RegionalId != null)
            {
                searchBody = new TatkalViewRequest
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    RegionalId = entity.RegionalId,
                    StatusFlag = entity.StatusFlag

                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new TatkalViewRequest
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    RegionalId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    StatusFlag = -1
                };
            }
            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ViewRequestDriverCard);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<TatkalCardCustomerViewResponse> searchList = jarr.ToObject<List<TatkalCardCustomerViewResponse>>();
            return searchList;
        }


        public async Task<TatkalViewRequestModel> ViewAllocatedMapCard()
        {
            TatkalViewRequestModel custModel = new TatkalViewRequestModel();
            custModel.ZonalOffices.AddRange(await _commonActionService.GetZonalOfficeList());
            custModel.FromDate = DateTime.Now.ToString("dd-MM-yyyy");
            custModel.ToDate = DateTime.Now.ToString("dd-MM-yyyy");
            return custModel;

        }

        public async Task<List<CustomerRegionModel>> GetRegionalDetailsDropDown(int ZonalOfficeID)
        {
            List<CustomerRegionModel> lstCustomerRegion = new List<CustomerRegionModel>();
            lstCustomerRegion = await _commonActionService.GetRegionalDetailsDropdown(ZonalOfficeID);
            return lstCustomerRegion;
        }

        public async Task<TatkalCardCustomerModel> CreateTatkalCustomer(TatkalCardCustomerModel customerModel)
        {
            customerModel.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            customerModel.UserAgent = CommonBase.useragent;
            customerModel.UserIp = CommonBase.userip;
            customerModel.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            customerModel.CommunicationPhoneNo = (string.IsNullOrEmpty(customerModel.CommunicationDialCode) ? "" : customerModel.CommunicationDialCode) + "-" + (string.IsNullOrEmpty(customerModel.CommunicationPhonePart2) ? "" : customerModel.CommunicationPhonePart2);
            customerModel.KeyOfficialSecretQuestion = "0";
            customerModel.KeyOfficialSecretAnswer = "";

            string customerDateOfApplication = "";
            string signedOn = "";

            //string[] custDateOfApplication = customerModel.DateOfApplication.Split("-");

            //customerDateOfApplication = custDateOfApplication[2] + "-" + custDateOfApplication[1] + "-" + custDateOfApplication[0];
            customerDateOfApplication = await _commonActionService.changeDateFormat(customerModel.DateOfApplication);
            if (!string.IsNullOrEmpty(customerModel.SignedOn))
            {
                //string[] dateSignedOn = customerModel.SignedOn.Split("-");
                //signedOn = dateSignedOn[2] + "-" + dateSignedOn[1] + "-" + dateSignedOn[0];
                signedOn = await _commonActionService.changeDateFormat(customerModel.SignedOn);
            }

            customerModel.DateOfApplication = customerDateOfApplication;
            customerModel.SignedOn = signedOn;

            StringContent content = new StringContent(JsonConvert.SerializeObject(customerModel), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertTatkalCardCustomer);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            OTCCustomerCardResponse customerResponse = JsonConvert.DeserializeObject<OTCCustomerCardResponse>(response, settings);

            customerModel.Internel_Status_Code = customerResponse.Internel_Status_Code;
            customerModel.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null && customerResponse.Data != null && customerResponse.Data.Count > 0)
                    customerModel.Remarks = customerResponse.Data[0].Reason;
                else
                    customerModel.Remarks = customerResponse.Message;

                customerModel.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
                customerModel.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());
                customerModel.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
                customerModel.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficebySBUType(customerModel.SBUTypeID.ToString()));
                customerModel.CustomerRegionMdl.AddRange(await _commonActionService.GetRegionalDetailsDropdown(customerModel.ZonalOffice));
            }
            else
            {
                if (customerResponse != null && customerResponse.Data != null && customerResponse.Data.Count > 0)
                {
                    customerModel.Remarks = customerResponse.Data[0].Reason;
                    if (customerResponse.Data[0].Status != 1)
                    {
                        customerModel.Internel_Status_Code = customerResponse.Internel_Status_Code + 1;
                        customerModel.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
                        customerModel.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());
                        customerModel.SBUTypes.AddRange(await _commonActionService.GetSbuTypeList());
                        customerModel.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficebySBUType(customerModel.SBUTypeID.ToString()));
                        customerModel.CustomerRegionMdl.AddRange(await _commonActionService.GetRegionalDetailsDropdown(customerModel.ZonalOffice));
                    }
                }
            }

            return customerModel;
        }
        public async Task<ViewRequestedTatkalCardModel> ViewRequestedTatkalCard()
        {
            ViewRequestedTatkalCardModel custModel = new ViewRequestedTatkalCardModel();
            custModel.Remarks = "";
            custModel.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());

            return custModel;
        }
        public async Task<ViewRequestedTatkalCardResponse> GetViewRequestedTatkalCard(int RegionalId)
        {
            var requestBody = new GetAllUnAllocatedOTCCardsRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                RegionalId = RegionalId.ToString(),
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.viewRequestedTatkalCard);

            ViewRequestedTatkalCardResponse viewRequestedTatkalCardResponse = JsonConvert.DeserializeObject<ViewRequestedTatkalCardResponse>(response);

            return viewRequestedTatkalCardResponse;
        }
        public async Task<GetTatkalCardsResponseModel> GetMapTatkalCardtoCustomer(string customerId)
        {
            GetTatkalCardsResponseModel GetTatkalCardsResponseModel = new GetTatkalCardsResponseModel();
            var requestBody = new MapTatkalCardtoCustomerModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                RegionalId = _httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Admin" ? "": _httpContextAccessor.HttpContext.Session.GetString("RegionalId"),
                CustomerId=customerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.maptatkalcardstotatkalcustomer);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetTatkalCardsResponseModel = JsonConvert.DeserializeObject<GetTatkalCardsResponseModel>(response);

            var JObj = obj["Data"].Value<JObject>();
            var cardjArr = JObj["ObjGetCardDetailsTatkalCardsToTatkalCustomer"].Value<JArray>();
            var customerjArr = JObj["ObjGetStatusTatkalCardsToTatkalCustomer"].Value<JArray>();

            List<TatkalCardDetails> cardList =
                cardjArr.ToObject<List<TatkalCardDetails>>();

            List<TatkalCardCustomerDetails> customerList =
                customerjArr.ToObject<List<TatkalCardCustomerDetails>>();

            GetTatkalCardsResponseModel.ObjGetCardDetailsTatkalCardsToTatkalCustomer.AddRange(cardList);
            GetTatkalCardsResponseModel.ObjGetStatusTatkalCardsToTatkalCustomer.AddRange(customerList);

            return GetTatkalCardsResponseModel;



            
        }
        public async Task<List<SuccessResponseTatkalCustomer>> UpdateTatkalCardtoCustomer([FromBody] MapTatkalCardtoCustomerUpdateModel UpdateDetails)
        {
            List<SuccessResponseTatkalCustomer> mapResponseList = new List<SuccessResponseTatkalCustomer>();

            var RequestForms = new MapTatkalCardtoCustomerUpdateModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = UpdateDetails.CustomerId,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjCardMap = UpdateDetails.ObjCardMap
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(RequestForms), Encoding.UTF8, "application/json");
            var mapResponse = await _requestService.CommonRequestService(requestContent, WebApiUrl.updatemaptatkalcardstotatkalcustomer);
            JObject mapResponseObj = JObject.Parse(JsonConvert.DeserializeObject(mapResponse).ToString());
            List<string> messageList = new List<string>();
            if (mapResponseObj["Status_Code"].ToString() == "200")
            {
                var mapResponseJarr = mapResponseObj["Data"].Value<JArray>();
                mapResponseList = mapResponseJarr.ToObject<List<SuccessResponseTatkalCustomer>>();
            }
            else
            {
                var mapResponseJarr = mapResponseObj["Data"].Value<JArray>();
                mapResponseList = mapResponseJarr.ToObject<List<SuccessResponseTatkalCustomer>>();
            }
            return mapResponseList;
        }
        public async Task<ViewTatkalCardsResponseModel> GetViewTatkalCards([FromBody] ViewTatkalCardRequestModel entity)
        {
            if (!string.IsNullOrEmpty(entity.FromDate) && !string.IsNullOrEmpty(entity.FromDate))
            {
                entity.FromDate = await _commonActionService.changeDateFormat(entity.FromDate);
                entity.ToDate = await _commonActionService.changeDateFormat(entity.ToDate);
            }
            var searchBody = new ViewTatkalCardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalOfficeID = entity.ZonalOfficeID == "0" ? "" : entity.ZonalOfficeID,
                RegionalOfficeID = entity.RegionalOfficeID == "0" ? "" : entity.RegionalOfficeID,
                FromDate = entity.FromDate,
                ToDate = entity.ToDate,
                StatusId = entity.StatusId == "-1" ? "" : entity.StatusId

            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.viewtatkalcards);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            ViewTatkalCardsResponseModel ViewTatkalCardsResponseModel  = obj.ToObject<ViewTatkalCardsResponseModel>();

            return ViewTatkalCardsResponseModel;
        }


    }


}
