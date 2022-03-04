using System;
using System.Collections.Generic;
using System.Text;
using HPCL.Service.Interfaces;
using System.Threading.Tasks;
using HPCL.Common.Models.ViewModel.DriverCardCustomer;
using Microsoft.AspNetCore.Http;
using HPCL.Common.Helper;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.RequestModel.MyHpOTCCardCustomer;
using HPCL.Common.Models;
using Microsoft.AspNetCore.Mvc;
using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;

namespace HPCL.Service.Services
{
    public class DriverCardCustomerService : IDriverCardCustomerService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;

        public DriverCardCustomerService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }

        public async Task<RequestForDriverCardModel> RequestForDriverCard()
        {
            RequestForDriverCardModel custMdl = new RequestForDriverCardModel();
            custMdl.Remarks = "";
            custMdl.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());

            return custMdl;
        }

        public async Task<RequestForDriverCardModel> RequestForDriverCard(RequestForDriverCardModel requestForDriverCardModel)
        {

            var driversRequestData = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"UserId", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"RegionalId", requestForDriverCardModel.CustomerRegionID.ToString()},
                {"NoofCards", requestForDriverCardModel.NoofCards.ToString()},
                {"CreatedBy", _httpContextAccessor.HttpContext.Session.GetString("UserName")}
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(driversRequestData), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertDriverCardRequest);


            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            requestForDriverCardModel.Internel_Status_Code = customerResponse.Internel_Status_Code;
            requestForDriverCardModel.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null)
                    requestForDriverCardModel.Remarks = customerResponse.Data[0].Reason;
                else
                    requestForDriverCardModel.Remarks = customerResponse.Message;

                requestForDriverCardModel.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());
            }

            return requestForDriverCardModel;
        }

        public async Task<DriverCardCustomerModel> CreateDriverCardCustomer()
        {
            DriverCardCustomerModel custMdl = new DriverCardCustomerModel();

            custMdl.CustomerStateMdl.AddRange(await _commonActionService.GetCustStateList());
            custMdl.DocumentTypeMdl.AddRange(await _commonActionService.GetAddressProofList());

            custMdl.LoggedInAs = "";

            if (_httpContextAccessor.HttpContext.Session.GetString("LoginType").ToUpper() == "MERCHANT")
            {
                custMdl.MerchantId = _httpContextAccessor.HttpContext.Session.GetString("MerchantID");
                custMdl.LoggedInAs = "MERCHANT";

                MerchantDetailsResponseOTCCardCustomer merchantDetailsResponseOTCCardCustomer = await _commonActionService.GetMerchantDetailsByMerchantId(custMdl.MerchantId);
                if (merchantDetailsResponseOTCCardCustomer.Internel_Status_Code == 1000)
                {
                    custMdl.Zone = merchantDetailsResponseOTCCardCustomer.ZonalOfficeName;
                    custMdl.OutletName = merchantDetailsResponseOTCCardCustomer.RetailOutletName;
                    //custMdl.SalesArea = merchantDetailsResponseOTCCardCustomer.SalesAreaName;
                    custMdl.RegionalOffice = merchantDetailsResponseOTCCardCustomer.RegionalOfficeName;
                    custMdl.RegionalOfficeId = merchantDetailsResponseOTCCardCustomer.RegionalOfficeId;
                    custMdl.Internel_Status_Code = merchantDetailsResponseOTCCardCustomer.Internel_Status_Code;
                }
            }

            return custMdl;
        }

        public async Task<DriverCardAllocationToMerchantModel> DriverCardAllocation()
        {
            DriverCardAllocationToMerchantModel custModel = new DriverCardAllocationToMerchantModel();
            custModel.Remarks = "";
            custModel.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());

            return custModel;
        }

        public async Task<OTCUnAllocatedCardsResponse> GetAllUnAllocatedDriverCards(string RegionalId)
        {
            OTCUnAllocatedCardsResponse responseData = new OTCUnAllocatedCardsResponse();

            GetAllUnAllocatedOTCCardsRequestModel requestinfo = new GetAllUnAllocatedOTCCardsRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                RegionalOfficeId = RegionalId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getAllUnAllocatedCardsForDriverCard);

            OTCUnAllocatedCard otcUnAllocatedCard = JsonConvert.DeserializeObject<OTCUnAllocatedCard>(response);

            //JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            //var jarr = obj["Data"].Value<JArray>();
            //List<OTCUnAllocatedCardsResponse> searchList = jarr.ToObject<List<OTCUnAllocatedCardsResponse>>();
            //responseData = searchList[0];

            responseData = otcUnAllocatedCard.Data;

            return responseData;
        }
        public async Task<CommonResponseData> SaveDriverCardsAllocation([FromBody] LinkCardsToMerchantModel linkCardsToMerchantModel)
        {
            CommonResponseData responseData = new CommonResponseData();

            var requestBody = new AllocateCardsToMerchantRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                NoOfCardsAllocated = linkCardsToMerchantModel.NoOfCardsAllocated,
                MerchantId = linkCardsToMerchantModel.MerchantId,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                ObjAllocatedCardsToMerchant = linkCardsToMerchantModel.ObjAllocatedCardsToMerchant
            };

            StringContent Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(Content, WebApiUrl.allocatedDriverCardToMerchant);
            JObject linkedObj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            if (linkedObj["Status_Code"].ToString() == "200")
            {
                var Jarr = linkedObj["Data"].Value<JArray>();
                //List<SuccessResponse> responseLst = Jarr.ToObject<List<SuccessResponse>>();
                //return responseLst.First().Reason.ToString();
                List<CommonResponseData> responseLst = Jarr.ToObject<List<CommonResponseData>>();
                responseData = responseLst[0];
                responseData.Internel_Status_Code = Convert.ToInt32(linkedObj["Internel_Status_Code"].ToString());
            }
            else
            {
                //return linkedObj["Message"].ToString();
                responseData.Internel_Status_Code = Convert.ToInt32(linkedObj["Internel_Status_Code"].ToString());
                responseData.Status = Convert.ToInt32(linkedObj["Status_Code"].ToString());
            }

            return responseData;
        }
        public async Task<List<CardDetails>> GetAvailableDriverCardByRegionalId(string RegionalId, string MerchantID)
        {

            GetAvailableOTCCardByRegionalIdRequestModel requestinfo = new GetAvailableOTCCardByRegionalIdRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                RegionalOfficeId = RegionalId,
                MerchantId = MerchantID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getAvailityDriverCard);

            //OTCCardDetailsResponse otcCardDetailsResponse = JsonConvert.DeserializeObject<OTCCardDetailsResponse>(response);
            //if (otcCardDetailsResponse.Internel_Status_Code == 1000)
            //{
            //    lstCardDetails = otcCardDetailsResponse.Data;
            //}

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CardDetails> searchList = jarr.ToObject<List<CardDetails>>();


            return searchList;
        }

        public async Task<DriverCardCustomerModel> CreateDriverCardCustomer(DriverCardCustomerModel customerModel)
        {
            customerModel.UserAgent = CommonBase.useragent;
            customerModel.UserIp = CommonBase.userip;
            customerModel.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            customerModel.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            customerModel.CommunicationPhoneNo = (string.IsNullOrEmpty(customerModel.CommunicationDialCode) ? "" : customerModel.CommunicationDialCode) + "-" + (string.IsNullOrEmpty(customerModel.CommunicationPhonePart2) ? "" : customerModel.CommunicationPhonePart2);
            
            StringContent content = new StringContent(JsonConvert.SerializeObject(customerModel), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertDriverCardCustomer);

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

                customerModel.CustomerStateMdl.AddRange(await _commonActionService.GetCustStateList());
                customerModel.DocumentTypeMdl.AddRange(await _commonActionService.GetAddressProofList());
            }

            return customerModel;
        }

    }
}
