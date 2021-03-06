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
using HPCL.Common.Models;
using HPCL.Common.Models.RequestModel.MyHpOTCCardCustomer;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using HPCL.Common.Models.RequestModel.TatkalCardCustomer;
using Microsoft.Extensions.Configuration;

namespace HPCL.Service.Services
{
    public class MyHpOTCCardCustomerService : IMyHpOTCCardCustomerService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        private readonly IConfiguration _configuration;

        public MyHpOTCCardCustomerService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
            _configuration = configuration;
        }

        public async Task<RequestForOTCCardModel> RequestForOTCCard()
        {
            RequestForOTCCardModel custMdl = new RequestForOTCCardModel();
            custMdl.Remarks = "";
            custMdl.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());

            return custMdl;
        }

        public async Task<RequestForOTCCardModel> RequestForOTCCard(RequestForOTCCardModel requestForOTCCardModel)
        {
            var driversRequestData = new RequestForTatkalCardModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                RegionalId = requestForOTCCardModel.CustomerRegionID.ToString(),
                NoofCards = requestForOTCCardModel.NoofCards.ToString(),
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(driversRequestData), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertOTCCardRequest);


            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            requestForOTCCardModel.Internel_Status_Code = customerResponse.Internel_Status_Code;
            requestForOTCCardModel.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null && customerResponse.Data.Count > 0)
                    requestForOTCCardModel.Remarks = customerResponse.Data[0].Reason;
                else
                    requestForOTCCardModel.Remarks = customerResponse.Message;

                requestForOTCCardModel.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());
            }
            else
            {
                if (customerResponse.Data != null && customerResponse.Data.Count > 0)
                    requestForOTCCardModel.Remarks = customerResponse.Data[0].Reason;
            }

            return requestForOTCCardModel;
        }

        public async Task<MyHPOTCCardCustomerModel> CustomerCardCreation()
        {
            MyHPOTCCardCustomerModel custModel = new MyHPOTCCardCustomerModel();
            custModel.Remarks = "";

            custModel.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
            custModel.LoggedInAs = "";
            custModel.ExternalPANAPIStatus = _configuration.GetSection("ExternalAPI:PANAPI").Value.ToString();
            if (string.IsNullOrEmpty(custModel.ExternalPANAPIStatus))
            {
                custModel.ExternalPANAPIStatus = "Y";
            }
            custModel.ExternalVehicleAPIStatus = _configuration.GetSection("ExternalAPI:VehicleAPI").Value.ToString();
            if (string.IsNullOrEmpty(custModel.ExternalVehicleAPIStatus))
            {
                custModel.ExternalVehicleAPIStatus = "Y";
            }

            if (_httpContextAccessor.HttpContext.Session.GetString("LoginType").ToUpper() == "MERCHANT")
            {
                custModel.MerchantId = _httpContextAccessor.HttpContext.Session.GetString("MerchantID");
                custModel.LoggedInAs = "MERCHANT";

                MerchantDetailsResponseOTCCardCustomer merchantDetailsResponseOTCCardCustomer = await _commonActionService.GetMerchantDetailsByMerchantId(custModel.MerchantId);
                if (merchantDetailsResponseOTCCardCustomer.Internel_Status_Code == 1000)
                {
                    custModel.Zone = merchantDetailsResponseOTCCardCustomer.ZonalOfficeName;
                    custModel.OutletName = merchantDetailsResponseOTCCardCustomer.RetailOutletName;
                    custModel.SalesArea = merchantDetailsResponseOTCCardCustomer.SalesAreaName;
                    custModel.RegionalOffice = merchantDetailsResponseOTCCardCustomer.RegionalOfficeName;
                    custModel.RegionalOfficeId = merchantDetailsResponseOTCCardCustomer.RegionalOfficeId;
                    custModel.Internel_Status_Code = merchantDetailsResponseOTCCardCustomer.Internel_Status_Code;
                }
            }

            return custModel;
        }
        
        public async Task<List<OTCCardDetails>> GetAvailableOTCCardByRegionalId(string RegionalId, string MerchantID)
        {
            List<OTCCardDetails> lstCardDetails = new List<OTCCardDetails>();

            GetAvailableOTCCardByRegionalIdRequestModel requestinfo = new GetAvailableOTCCardByRegionalIdRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                RegionalOfficeId = RegionalId,
                MerchantId = MerchantID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getAvailityOtcCard);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<OTCCardDetails> searchList = jarr.ToObject<List<OTCCardDetails>>();

            return searchList;
        }

        public async Task<MyHPOTCCardCustomerModel> CustomerCardCreation(MyHPOTCCardCustomerModel customerModel)
        {
            customerModel.UserAgent = CommonBase.useragent;
            customerModel.UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress");
            customerModel.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            customerModel.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            customerModel.CommunicationPhoneNo = (string.IsNullOrEmpty(customerModel.CommunicationDialCode) ? "" : customerModel.CommunicationDialCode) + "-" + (string.IsNullOrEmpty(customerModel.CommunicationPhonePart2) ? "" : customerModel.CommunicationPhonePart2);
            customerModel.CommunicationFax = (string.IsNullOrEmpty(customerModel.CommunicationFaxCode) ? "" : customerModel.CommunicationFaxCode) + "-" + (string.IsNullOrEmpty(customerModel.CommunicationFaxPart2) ? "" : customerModel.CommunicationFaxPart2);
            if (!string.IsNullOrEmpty(customerModel.CommunicationEmailid))
            {
                customerModel.CommunicationEmailid = customerModel.CommunicationEmailid.ToLower();
            }

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
                if (customerResponse != null && customerResponse.Data != null && customerResponse.Data.Count > 0)
                    customerModel.Remarks = customerResponse.Data[0].Reason;
                else
                    customerModel.Remarks = customerResponse.Message;

                customerModel.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
                customerModel.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(customerModel.CommunicationStateId.ToString()));
            }
            else
            {
                if (customerResponse != null && customerResponse.Data != null && customerResponse.Data.Count > 0)
                {
                    customerModel.Remarks = customerResponse.Data[0].Reason;
                    if (customerResponse.Data[0].Status != 1)
                    {
                        customerModel.Internel_Status_Code = customerResponse.Internel_Status_Code + 1;
                    }
                }
            }

            return customerModel;
        }
   
        public async Task<OTCUnAllocatedCardsResponse> GetAllUnAllocatedCardsForOtcCard(string RegionalId)
        {
            OTCUnAllocatedCardsResponse responseData = new OTCUnAllocatedCardsResponse();

            GetAllUnAllocatedOTCCardsRequestModel requestinfo = new GetAllUnAllocatedOTCCardsRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                RegionalOfficeId = RegionalId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getAllUnAllocatedCardsForOtcCard);

            OTCUnAllocatedCard otcUnAllocatedCard = JsonConvert.DeserializeObject<OTCUnAllocatedCard>(response);

            //JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            //var jarr = obj["Data"].Value<JArray>();
            //List<OTCUnAllocatedCardsResponse> searchList = jarr.ToObject<List<OTCUnAllocatedCardsResponse>>();
            //responseData = searchList[0];

            responseData = otcUnAllocatedCard.Data;

            return responseData;
        }
        public async Task<MIDAllocationOfCardsModel> OTCCardsAllocation()
        {
            MIDAllocationOfCardsModel custModel = new MIDAllocationOfCardsModel();
            custModel.Remarks = "";
            custModel.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());

            return custModel;
        }

        public async Task<ViewOTCCardResponse> GetAllViewCardsForOtcCard(string RegionalId)
        {
            var searchBody = new GetAllUnAllocatedOTCCardsRequestModel();
            if (!string.IsNullOrEmpty(RegionalId))
            {
                searchBody = new GetAllUnAllocatedOTCCardsRequestModel
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    RegionalId = RegionalId
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new GetAllUnAllocatedOTCCardsRequestModel
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    RegionalId = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
            }
            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ViewOTCCardRequest);

            ViewOTCCardResponse viewOTCCardResponse = JsonConvert.DeserializeObject<ViewOTCCardResponse>(response);
            return viewOTCCardResponse;
        }


        public async Task<MIDAllocationOfCardsModel> ViewOTCCards()
        {
            MIDAllocationOfCardsModel custModel = new MIDAllocationOfCardsModel();
            custModel.Remarks = "";
            custModel.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());

            return custModel;
        }

        public async Task<CommonResponseData> SaveOTCCardsAllocation([FromBody] LinkCardsToMerchantModel linkCardsToMerchantModel)
        {
            CommonResponseData responseData = new CommonResponseData();

            var requestBody = new AllocateCardsToMerchantRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                NoOfCardsAllocated = linkCardsToMerchantModel.NoOfCardsAllocated,
                MerchantId = linkCardsToMerchantModel.MerchantId,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjAllocatedCardsToMerchant = linkCardsToMerchantModel.ObjAllocatedCardsToMerchant,
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                RegionalId="1",
                ZonalId="1"
            };

            StringContent Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(Content, WebApiUrl.allocatedOtcCardToMerchant);
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

        public async Task<GetCardAllocationActivation> GetCardAllocationActivation()
        {
            GetCardAllocationActivation getCardAllocationActivation = new GetCardAllocationActivation();
            getCardAllocationActivation.ZoneMdl.AddRange(await _commonActionService.GetZonalOfficeList());
            return getCardAllocationActivation;
        }

        public async Task<ViewOTCCardsMerchatMappingModel> ViewOTCCardsMerchatMapping()
        {
            ViewOTCCardsMerchatMappingModel custModel = new ViewOTCCardsMerchatMappingModel();
            custModel.Remarks = "";
            
            return custModel;
        }

        public async Task<OTCCardMerchantAllocationResponse> ViewOTCCardMerchantAllocation(string MerchantId, string CardNo)
        {
            var searchBody = new GetOTCCardMerchantAllocationRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                MerchantId = MerchantId,
                CardNo = string.IsNullOrEmpty(CardNo) ? "" : CardNo
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(content, WebApiUrl.viewOtcCardMerchantAllocation);

            OTCCardMerchantAllocationResponse responseData = new OTCCardMerchantAllocationResponse();

            responseData = JsonConvert.DeserializeObject<OTCCardMerchantAllocationResponse>(ResponseContent);

            if (responseData != null && responseData.Data != null && responseData.Data.ObjMerchantTotalCardDetail != null
                && responseData.Data.ObjMerchantTotalCardDetail.Count > 0 && responseData.Data.ObjMerchantTotalCardDetail[0].Status == 0)
            {
                responseData.Message = responseData.Data.ObjMerchantTotalCardDetail[0].Reason;
                responseData.Data.ObjMerchantViewCardDetail = new List<OTCCardMerchantViewCardDetail>();
            }

            return responseData;
        }

        public async Task<GetCardAllocationActivation> MyHPOTCCardAllocationandActivation()
        {
            GetCardAllocationActivation GetCardAllocationActivation = new GetCardAllocationActivation();
            GetCardAllocationActivation.SBUTypes. AddRange(await _commonActionService.GetSbuTypeList());
            GetCardAllocationActivation.ZoneMdl.AddRange(await _commonActionService.GetZonalOfficeListbySBUtype("1"));
            return GetCardAllocationActivation;
        }
        public async Task<MyCardAllocationandActivationModel> SearchCardActivationandAllocation(GetCardAllocationActivation entity)
        {
            MyCardAllocationandActivationModel getMyCardAllocationandActivation = new MyCardAllocationandActivationModel();
            if (!string.IsNullOrEmpty(entity.FromDate) && !string.IsNullOrEmpty(entity.FromDate))
            {
                entity.FromDate = await _commonActionService.changeDateFormat(entity.FromDate);
                entity.ToDate = await _commonActionService.changeDateFormat(entity.ToDate);
            }
            
            var cardAllocationforms = new GetCardAllocationActivation
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FromDate = entity.FromDate,
                ToDate = entity.ToDate,
                ZonalOfficeId = string.IsNullOrEmpty(entity.ZonalOfficeId) || entity.ZonalOfficeId == "0" ? "" : entity.ZonalOfficeId,
                RegionalOfficeId = string.IsNullOrEmpty(entity.RegionalOfficeId) || entity.RegionalOfficeId == "0" ? "" : entity.RegionalOfficeId,
                CustomerId = string.IsNullOrEmpty(entity.CustomerId) ? "" : entity.CustomerId,
                SBUTypeId=entity.SBUTypeId,
            };

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(cardAllocationforms), Encoding.UTF8, "application/json");

            var cardDetailsResponse = await _requestService.CommonRequestService(stringContent, WebApiUrl.getotccardallocationactivation);

            JObject cardDetailsResponseObj = JObject.Parse(JsonConvert.DeserializeObject(cardDetailsResponse).ToString());
            getMyCardAllocationandActivation= JsonConvert.DeserializeObject<MyCardAllocationandActivationModel>(cardDetailsResponse);
            var cardDetailsResponseJarr = cardDetailsResponseObj["Data"].Value<JArray>();
            List<MyCardAllocationandActivationDetails> getMyCardAllocationandActivationDetails = cardDetailsResponseJarr.ToObject<List<MyCardAllocationandActivationDetails>>();
            getMyCardAllocationandActivation.MyCardAllocationandActivationDetails.AddRange(getMyCardAllocationandActivationDetails);
            return getMyCardAllocationandActivation;
        }

        public async Task<DealerWiseMyHPOTCCardRequestModel> DealerOTCCardRequests()
        {
            DealerWiseMyHPOTCCardRequestModel custModel = new DealerWiseMyHPOTCCardRequestModel();
            custModel.Remarks = "";

            return custModel;
        }

        public async Task<DealerWiseMyHPOTCCardRequestModel> DealerOTCCardRequests(DealerWiseMyHPOTCCardRequestModel dealerWiseMyHPOTCCardRequestModel)
        {
            dealerWiseMyHPOTCCardRequestModel.UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress");
            dealerWiseMyHPOTCCardRequestModel.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            dealerWiseMyHPOTCCardRequestModel.UserAgent = CommonBase.useragent;
            dealerWiseMyHPOTCCardRequestModel.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            StringContent content = new StringContent(JsonConvert.SerializeObject(dealerWiseMyHPOTCCardRequestModel), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertDealerWiseOtcCardRequest);

            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            dealerWiseMyHPOTCCardRequestModel.Internel_Status_Code = customerResponse.Internel_Status_Code;
            dealerWiseMyHPOTCCardRequestModel.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null && customerResponse.Data.Count > 0)
                    dealerWiseMyHPOTCCardRequestModel.Remarks = customerResponse.Data[0].Reason;
                else
                    dealerWiseMyHPOTCCardRequestModel.Remarks = customerResponse.Message;
            }
            else
            {
                if (customerResponse.Data != null && customerResponse.Data.Count > 0)
                    dealerWiseMyHPOTCCardRequestModel.Remarks = customerResponse.Data[0].Reason;
                else
                    dealerWiseMyHPOTCCardRequestModel.Remarks = customerResponse.Message;
            }

            return dealerWiseMyHPOTCCardRequestModel;
        }

        public async Task<CustomerInserCardResponseData> CheckMobilNoAlreadyUsedForOTCCard(string MobileNo)
        {
            var requestInfo = new CheckMobilNoDuplicationRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                Mobileno = MobileNo
            };

            StringContent cusFormcontent = new StringContent(JsonConvert.SerializeObject(requestInfo), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(cusFormcontent, WebApiUrl.checkMobileNo);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerInserCardResponseData> lst = jarr.ToObject<List<CustomerInserCardResponseData>>();
            return lst[0];
        }

        public async Task<MyHPOTCCardCustomerModel> OTCCustomerCreation()
        {
            MyHPOTCCardCustomerModel custModel = new MyHPOTCCardCustomerModel();
            custModel.Remarks = "";

            custModel.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
            custModel.LoggedInAs = "";
            custModel.ExternalPANAPIStatus = _configuration.GetSection("ExternalAPI:PANAPI").Value.ToString();
            if (string.IsNullOrEmpty(custModel.ExternalPANAPIStatus))
            {
                custModel.ExternalPANAPIStatus = "Y";
            }
            custModel.ExternalVehicleAPIStatus = _configuration.GetSection("ExternalAPI:VehicleAPI").Value.ToString();
            if (string.IsNullOrEmpty(custModel.ExternalVehicleAPIStatus))
            {
                custModel.ExternalVehicleAPIStatus = "Y";
            }

            return custModel;
        }
        public async Task<List<OTCCardDetails>> GetAvailableOTCCardUserWise()
        {
            List<OTCCardDetails> lstCardDetails = new List<OTCCardDetails>();

            var requestBody = new GetAvailableOTCCardByRegionalIdRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserName = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getAvailityOtcCardUserWise);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<OTCCardDetails> searchList = jarr.ToObject<List<OTCCardDetails>>();


            return searchList;
        }

        public async Task<MyHPOTCCardCustomerModel> InsertOTCCustomer(MyHPOTCCardCustomerModel customerModel)
        {
            customerModel.UserAgent = CommonBase.useragent;
            customerModel.UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress");
            customerModel.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            customerModel.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            customerModel.UserName = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            customerModel.CommunicationPhoneNo = (string.IsNullOrEmpty(customerModel.CommunicationDialCode) ? "" : customerModel.CommunicationDialCode) + "-" + (string.IsNullOrEmpty(customerModel.CommunicationPhonePart2) ? "" : customerModel.CommunicationPhonePart2);
            customerModel.CommunicationFax = (string.IsNullOrEmpty(customerModel.CommunicationFaxCode) ? "" : customerModel.CommunicationFaxCode) + "-" + (string.IsNullOrEmpty(customerModel.CommunicationFaxPart2) ? "" : customerModel.CommunicationFaxPart2);
            if (!string.IsNullOrEmpty(customerModel.CommunicationEmailid))
            {
                customerModel.CommunicationEmailid = customerModel.CommunicationEmailid.ToLower();
            }

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
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertOtcCustomerRegionWise);

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
                if (customerResponse != null && customerResponse.Data != null && customerResponse.Data.Count > 0)
                    customerModel.Remarks = customerResponse.Data[0].Reason;
                else
                    customerModel.Remarks = customerResponse.Message;

                customerModel.CustomerStateMdl.AddRange(await _commonActionService.GetStateList());
                customerModel.CommunicationDistrictMdl.AddRange(await _commonActionService.GetDistrictDetails(customerModel.CommunicationStateId.ToString()));
            }
            else
            {
                if (customerResponse != null && customerResponse.Data != null && customerResponse.Data.Count > 0)
                {
                    customerModel.Remarks = customerResponse.Data[0].Reason;
                    if (customerResponse.Data[0].Status != 1)
                    {
                        customerModel.Internel_Status_Code = customerResponse.Internel_Status_Code + 1;
                    }
                }
            }

            return customerModel;
        }

    }
}
