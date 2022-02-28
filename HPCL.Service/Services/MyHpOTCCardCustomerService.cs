using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;


namespace HPCL.Service.Services
{
    public class MyHpOTCCardCustomerService : IMyHpOTCCardCustomerService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;

        public MyHpOTCCardCustomerService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }

        public async Task<RequestForOTCCardModel> RequestForOTCCard()
        {
            RequestForOTCCardModel custMdl = new RequestForOTCCardModel();

            custMdl.RegionMdl.AddRange(await _commonActionService.GetRegionList());

            return custMdl;
        }

        public async Task<RequestForOTCCardModel> RequestForOTCCard(RequestForOTCCardModel requestForOTCCardModel)
        {
            var driversRequestData = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"UserId", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                    {"RegionalId", requestForOTCCardModel.CustomerRegionID.ToString()},
                    {"NoofCards", requestForOTCCardModel.NoofCards.ToString()},
                    {"CreatedBy", _httpContextAccessor.HttpContext.Session.GetString("UserName")}
                };

            StringContent content = new StringContent(JsonConvert.SerializeObject(driversRequestData), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertOTCCardRequest);


            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            requestForOTCCardModel.Internel_Status_Code = customerResponse.Internel_Status_Code;
            requestForOTCCardModel.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null)
                    requestForOTCCardModel.Remarks = customerResponse.Data[0].Reason;
                else
                    requestForOTCCardModel.Remarks = customerResponse.Message;

                requestForOTCCardModel.RegionMdl.AddRange(await _commonActionService.GetRegionList());
            }

            return requestForOTCCardModel;
        }

        public async Task<MyHPOTCCardCustomerModel> CustomerCardCreation()
        {
            MyHPOTCCardCustomerModel custModel = new MyHPOTCCardCustomerModel();

            custModel.CustomerStateMdl.AddRange(await _commonActionService.GetCustStateList());

            return custModel;
        }

        public async Task<MerchantDetailsResponseOTCCardCustomer> GetMerchantDetailsByMerchantId(string MerchantID)
        {
            MerchantDetailsResponseOTCCardCustomer merchantDetails = new MerchantDetailsResponseOTCCardCustomer();

            var requestinfo = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"MerchantId", MerchantID}
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.searchMerchantForCardCreation);

            MerchantResponseOTCCardCustomer merchant = JsonConvert.DeserializeObject<MerchantResponseOTCCardCustomer>(response);


            if (merchant.Internel_Status_Code == 1000)
            {
                merchantDetails.RegionalOfficeName = merchant.Data[0].RegionalOfficeName;
                merchantDetails.RetailOutletName = merchant.Data[0].RetailOutletName;
                merchantDetails.SalesAreaName = merchant.Data[0].SalesAreaName;
                merchantDetails.ZonalOfficeName = merchant.Data[0].ZonalOfficeName;
                merchantDetails.RegionalOfficeId= merchant.Data[0].RegionalOfficeId;
                merchantDetails.Internel_Status_Code = merchant.Internel_Status_Code;
                merchantDetails.Status_Code = merchant.Status_Code;
            }

            return merchantDetails;

        }

        public async Task<List<CardDetails>> GetAvailableOTCCardByRegionalId(string RegionalId)
        {
            List<CardDetails> lstCardDetails = new List<CardDetails>();

            var requestinfo = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"RegionalId", RegionalId}
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getAvailityOtcCard);

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

        public async Task<MyHPOTCCardCustomerModel> CustomerCardCreation(MyHPOTCCardCustomerModel customerModel)
        {
            //var driversRequestData = new Dictionary<string, string>
            //    {
            //        {"Useragent", CommonBase.useragent},
            //        {"Userip", CommonBase.userip},
            //        {"UserId", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
            //        {"RegionalId", requestForOTCCardModel.CustomerRegionID.ToString()},
            //        {"NoofCards", requestForOTCCardModel.NoofCards.ToString()},
            //        {"CreatedBy", _httpContextAccessor.HttpContext.Session.GetString("UserName")}
            //    };

            customerModel.UserAgent = CommonBase.useragent;
            customerModel.UserIp = CommonBase.userip;
            customerModel.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            customerModel.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            customerModel.CommunicationPhoneNo = (string.IsNullOrEmpty(customerModel.CommunicationDialCode) ? "" : customerModel.CommunicationDialCode) + "-" + (string.IsNullOrEmpty(customerModel.CommunicationPhonePart2) ? "" : customerModel.CommunicationPhonePart2);
            customerModel.CommunicationFax = (string.IsNullOrEmpty(customerModel.CommunicationFaxCode) ? "" : customerModel.CommunicationFaxCode) + "-" + (string.IsNullOrEmpty(customerModel.CommunicationFaxPart2) ? "" : customerModel.CommunicationFaxPart2);

            customerModel.ObjOTCCardEntryDetail.Clear();

            if (!string.IsNullOrEmpty(customerModel.CardNumber1))
            {
                OTCCardEntryDetailsMdl cardDetails = new OTCCardEntryDetailsMdl();
                cardDetails.CardNo = customerModel.CardNumber1;
                cardDetails.VechileNo = customerModel.VehicleNo1;
                cardDetails.MobileNo = customerModel.MobileNo1;
                customerModel.ObjOTCCardEntryDetail.Add(cardDetails);
            }

            if (!string.IsNullOrEmpty(customerModel.CardNumber2))
            {
                OTCCardEntryDetailsMdl cardDetails = new OTCCardEntryDetailsMdl();
                cardDetails.CardNo = customerModel.CardNumber2;
                cardDetails.VechileNo = customerModel.VehicleNo2;
                cardDetails.MobileNo = customerModel.MobileNo2;
                customerModel.ObjOTCCardEntryDetail.Add(cardDetails);
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(customerModel), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertOtcCustomer);

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
                customerModel.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(customerModel.CommunicationStateId.ToString()));
            }

            return customerModel;
        }


    }
}
