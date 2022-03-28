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

namespace HPCL.Service.Services
{
    public class TatkalCardCustomerService : ITatkalCardCustomerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;

        public TatkalCardCustomerService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
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
            var driversRequestData = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"UserId", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
                    {"RegionalId", tatkalCustomerCardRequestInfo.CustomerRegionID.ToString()},
                    {"NoofCards", tatkalCustomerCardRequestInfo.NoofCards.ToString()},
                    {"CreatedBy", _httpContextAccessor.HttpContext.Session.GetString("UserName")}
                };

            StringContent content = new StringContent(JsonConvert.SerializeObject(driversRequestData), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertTatkalCardRequest);


            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            tatkalCustomerCardRequestInfo.Internel_Status_Code = customerResponse.Internel_Status_Code;
            tatkalCustomerCardRequestInfo.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null)
                    tatkalCustomerCardRequestInfo.Remarks = customerResponse.Data[0].Reason;
                else
                    tatkalCustomerCardRequestInfo.Remarks = customerResponse.Message;

                tatkalCustomerCardRequestInfo.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());
            }

            return tatkalCustomerCardRequestInfo;
        }

        public async Task<TatkalCardCustomerModel> CreateTatkalCustomer()
        {
            TatkalCardCustomerModel custModel = new TatkalCardCustomerModel();
            custModel.Remarks = "";
            custModel.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficeListForDropdown());
            custModel.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
            custModel.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());

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

            string fromDate = "", toDate = "";
            if (!string.IsNullOrEmpty(custModel.FromDate) && !string.IsNullOrEmpty(custModel.FromDate))
            {
                string[] fromDateArr = custModel.FromDate.Split("-");
                string[] toDateArr = custModel.ToDate.Split("-");

                fromDate = fromDateArr[2] + "-" + fromDateArr[1] + "-" + fromDateArr[0];
                toDate = toDateArr[2] + "-" + toDateArr[1] + "-" + toDateArr[0];

            }
            else
            {

                return custModel;
            }

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
            customerModel.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            customerModel.UserAgent = CommonBase.useragent;
            customerModel.UserIp = CommonBase.userip;
            customerModel.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            customerModel.CommunicationPhoneNo = (string.IsNullOrEmpty(customerModel.CommunicationDialCode) ? "" : customerModel.CommunicationDialCode) + "-" + (string.IsNullOrEmpty(customerModel.CommunicationPhonePart2) ? "" : customerModel.CommunicationPhonePart2);

            string customerDateOfApplication = "";
            string signedOn = "";

            string[] custDateOfApplication = customerModel.DateOfApplication.Split("-");

            customerDateOfApplication = custDateOfApplication[2] + "-" + custDateOfApplication[1] + "-" + custDateOfApplication[0];

            if (!string.IsNullOrEmpty(customerModel.SignedOn))
            {
                string[] dateSignedOn = customerModel.SignedOn.Split("-");
                signedOn = dateSignedOn[2] + "-" + dateSignedOn[1] + "-" + dateSignedOn[0];
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
                if (customerResponse.Data != null)
                    customerModel.Remarks = customerResponse.Data[0].Reason;
                else
                    customerModel.Remarks = customerResponse.Message;

                customerModel.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
                customerModel.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());
                customerModel.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficeListForDropdown());
                customerModel.CustomerRegionMdl.AddRange(await _commonActionService.GetRegionalDetailsDropdown(customerModel.ZonalOffice));
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
        public async Task<List<ViewRequestedTatkalCardResponse>> GetViewRequestedTatkalCard(int RegionalId)
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

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<ViewRequestedTatkalCardResponse> searchList = jarr.ToObject<List<ViewRequestedTatkalCardResponse>>();
            return searchList;
        }


    }

    
}
