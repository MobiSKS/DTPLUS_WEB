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
using HPCL.Common.Models.RequestModel.DriverCardCustomer;
using HPCL.Common.Models.ResponseModel.DriverCardCustomer;

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
            if (!string.IsNullOrEmpty(customerModel.CommunicationEmailid))
            {
                customerModel.CommunicationEmailid = customerModel.CommunicationEmailid.ToLower();
            }

            MultipartFormDataContent form = new MultipartFormDataContent();

            int DTPCustomerStatus = customerModel.IfDTPCustomer.ToUpper() == "YES" ? 1 : 0;

            form.Add(new StringContent(customerModel.CardNo), "CardNo");
            form.Add(new StringContent(_httpContextAccessor.HttpContext.Session.GetString("UserName")), "CreatedBy");
            form.Add(new StringContent(customerModel.IndividualOrgName), "IndividualOrgName");
            form.Add(new StringContent(customerModel.IndividualOrgNameTitle), "IndividualOrgNameTitle");
            form.Add(new StringContent(customerModel.IncomeTaxPan), "IncomeTaxPan");
            form.Add(new StringContent(customerModel.CommunicationAddress1), "CommunicationAddress1");
            form.Add(new StringContent(customerModel.CommunicationCityName), "CommunicationCityName");
            form.Add(new StringContent(customerModel.CommunicationPincode), "CommunicationPincode");
            form.Add(new StringContent(customerModel.CommunicationStateId.ToString()), "CommunicationStateId");
            form.Add(new StringContent(string.IsNullOrEmpty(customerModel.CommunicationPhoneNo) ? "" : customerModel.CommunicationPhoneNo), "CommunicationPhoneNo");
            form.Add(new StringContent(customerModel.CommunicationMobileNo), "CommunicationMobileNo");
            form.Add(new StringContent(customerModel.CommunicationEmailid), "CommunicationEmailid");
            form.Add(new StringContent(customerModel.FormNumber), "FormNumber");
            form.Add(new StringContent(customerModel.MerchantId), "MerchantId");
            form.Add(new StringContent(customerModel.AddressProofType), "AddressProofType");
            form.Add(new StringContent(customerModel.AddressProofDocumentNo), "AddressProofDocumentNo");
            form.Add(new StringContent(customerModel.DrivingLicence), "DrivingLicence");
            form.Add(new StringContent(DTPCustomerStatus.ToString()), "DTPCustomerStatus");
            form.Add(new StringContent(string.IsNullOrEmpty(customerModel.ExistingCustomerId) ? "" : customerModel.ExistingCustomerId), "ExistingCustomerId");
            form.Add(new StringContent(customerModel.BeneficiaryName), "BeneficiaryName");
            form.Add(new StringContent(customerModel.RelationWithBeneficiary), "RelationwithBeneficiary");
            form.Add(new StringContent(customerModel.BeneficiaryMobile), "BeneficiaryMobile");
            form.Add(new StreamContent(customerModel.AddressProof.OpenReadStream()), "AddressProof", customerModel.AddressProof.FileName);
            form.Add(new StringContent(customerModel.UserId), "Userid");
            form.Add(new StringContent(customerModel.UserAgent), "Useragent");
            form.Add(new StringContent(customerModel.UserIp), "Userip");

            //StringContent content = new StringContent(JsonConvert.SerializeObject(form), Encoding.UTF8, "application/json");
            var response = await _requestService.FormDataRequestService(form, WebApiUrl.insertDriverCardCustomer);

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

        public async Task<GetCustomerNameByIdResponse> GetCustomerNameByCustomerId(string CustomerID)
        {
            GetCustomerNameByIdResponse customerInfo = new GetCustomerNameByIdResponse();

            var customerReqinfo = new GetCustomerNameModel()
            {
                UserAgent= CommonBase.useragent,
                UserIp= CommonBase.userip,
                UserId=_httpContextAccessor.HttpContext.Session.GetString("UserName"),
                CustomerID = CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(customerReqinfo), Encoding.UTF8, "application/json");

            var responseCustomer = await _requestService.CommonRequestService(content, WebApiUrl.getcustomerNameByCustomerId);

            customerInfo = JsonConvert.DeserializeObject<GetCustomerNameByIdResponse>(responseCustomer);

            customerInfo.CustomerName = "";
            if (customerInfo.Internel_Status_Code == 1000 && customerInfo.Data != null && customerInfo.Data.Count > 0)
            {
                customerInfo.CustomerName = customerInfo.Data[0].CustomerName;
            }

            return customerInfo;

        }

        public async Task<List<ViewDriverCardResponse>> GetAllViewDriverCard(RequestForViewDriverCard entity)
        {
            var searchBody = new RequestForViewDriverCard();
            if (entity.RegionalId != null)
            {
                searchBody = new RequestForViewDriverCard
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    RegionalId = entity.RegionalId,

                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new RequestForViewDriverCard
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    RegionalId = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
            }
            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ViewRequestDriverCard);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<ViewDriverCardResponse> searchList = jarr.ToObject<List<ViewDriverCardResponse>>();
            return searchList;
        }

        public async Task<RequestForDriverCardModel> ViewRequestDriverCard()
        {
            RequestForDriverCardModel custModel = new RequestForDriverCardModel();
            custModel.Remarks = "";
            custModel.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());

            return custModel;
        }
        public async Task<ViewDriverCardMerchatMappingModel> ViewDriverCardsMerchatMapping()
        {
            ViewDriverCardMerchatMappingModel custModel = new ViewDriverCardMerchatMappingModel();
            custModel.Remarks = "";
            
            return custModel;
        }

        public async Task<DriverCardMerchantAllocationResponse> ViewDriverCardMerchantAllocation(string MerchantId, string CardNo)
        {
            var searchBody = new GetOTCCardMerchantAllocationRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                MerchantId = MerchantId,
                CardNo = string.IsNullOrEmpty(CardNo) ? "" : CardNo
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.viewDriverCardMerchantAllocation);

            DriverCardMerchantAllocationResponse driverCardMerchantAllocationResponse = new DriverCardMerchantAllocationResponse();

            driverCardMerchantAllocationResponse = JsonConvert.DeserializeObject<DriverCardMerchantAllocationResponse>(ResponseContent);

            return driverCardMerchantAllocationResponse;
        }


        public async Task<DriverCardAllocationanadActivationViewModel> GetDriverCardActivationAllocationDetails(string zonalOfficeID, string regionalOfficeID, string fromDate, string toDate, string customerId)
        {
            DriverCardAllocationanadActivationViewModel getDrtiverAllocationandActivation = new DriverCardAllocationanadActivationViewModel();

            var cardAllocationforms = new GetCardAllocationActivation
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FromDate = fromDate,
                ToDate = toDate,
                ZonalOfficeId = string.IsNullOrEmpty(zonalOfficeID) || zonalOfficeID == "0" ? "" : zonalOfficeID,
                RegionalOfficeId = string.IsNullOrEmpty(regionalOfficeID) || regionalOfficeID == "0" ? "" : regionalOfficeID,
                CustomerId = string.IsNullOrEmpty(customerId) ? "" : customerId,
            };

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(cardAllocationforms), Encoding.UTF8, "application/json");

            var cardDetailsResponse = await _requestService.CommonRequestService(stringContent, WebApiUrl.getdrivercardallocationactivation);

            JObject cardDetailsResponseObj = JObject.Parse(JsonConvert.DeserializeObject(cardDetailsResponse).ToString());
            getDrtiverAllocationandActivation = JsonConvert.DeserializeObject<DriverCardAllocationanadActivationViewModel>(cardDetailsResponse);
            var cardDetailsResponseJarr = cardDetailsResponseObj["Data"].Value<JArray>();
            List<DriverCardAllocationandActivationDetails> getDriverAllocationandActivationDetails = cardDetailsResponseJarr.ToObject<List<DriverCardAllocationandActivationDetails>>();
            getDrtiverAllocationandActivation.DriverCardAllocationandActivationDetails.AddRange(getDriverAllocationandActivationDetails);
            return getDrtiverAllocationandActivation;

        }
        public async Task<DriverCardAllocationanadActivationViewModel> DriverCardAllocationandActivation()
        {
            DriverCardAllocationanadActivationViewModel GetCardAllocationActivation = new DriverCardAllocationanadActivationViewModel();
            GetCardAllocationActivation.ZoneMdl.AddRange(await _commonActionService.GetZonalOfficeList());
            return GetCardAllocationActivation;
        }
        public async Task<DealerWiseDriverCardRequestModel> DealerDriverCardRequests()
        {
            DealerWiseDriverCardRequestModel custModel = new DealerWiseDriverCardRequestModel();
            custModel.Remarks = "";

            return custModel;
        }
        public async Task<DealerWiseDriverCardRequestModel> DealerDriverCardRequests(DealerWiseDriverCardRequestModel dealerWiseDriverCardRequestModel)
        {
            dealerWiseDriverCardRequestModel.UserIp = CommonBase.userip;
            dealerWiseDriverCardRequestModel.UserId = CommonBase.userid;
            dealerWiseDriverCardRequestModel.UserAgent = CommonBase.useragent;
            dealerWiseDriverCardRequestModel.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserName");

            StringContent content = new StringContent(JsonConvert.SerializeObject(dealerWiseDriverCardRequestModel), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertDealerWiseDriverCardRequest);

            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            dealerWiseDriverCardRequestModel.Internel_Status_Code = customerResponse.Internel_Status_Code;
            dealerWiseDriverCardRequestModel.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null)
                    dealerWiseDriverCardRequestModel.Remarks = customerResponse.Data[0].Reason;
                else
                    dealerWiseDriverCardRequestModel.Remarks = customerResponse.Message;
            }

            return dealerWiseDriverCardRequestModel;
        }


    }
}
