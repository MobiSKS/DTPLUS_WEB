using HPCL.Common.Models.ViewModel.Cards;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Helper;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Cards;
using HPCL.Service.Interfaces;
using System;
using System.Linq;
using HPCL.Common.Models.RequestModel.Cards;
using HPCL.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace HPCL.Service.Services
{
    public class CardServices : ICardServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public CardServices(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<SearchManageCards> ManageCards(CustomerCards entity, string editFlag)
        {
            var searchBody = new CustomerCards();

            var UserName = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            if (entity.CustomerId != null)
            {
                searchBody = new CustomerCards
                {
                    UserId = UserName,
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = entity.CustomerId,
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo,
                    VehicleNumber = entity.VehicleNumber,
                    StatusFlag = entity.StatusFlag
                };
                _httpContextAccessor.HttpContext.Session.SetString("viewUpdatedGrid", JsonConvert.SerializeObject(searchBody));
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new CustomerCards
                {
                    UserId = UserName,
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    StatusFlag = -1,
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo,
                    VehicleNumber = entity.VehicleNumber,
                };
            }
            else if (editFlag == "edit" && _httpContextAccessor.HttpContext.Session.GetString("LoginType") != "Customer")
            {
                var str = _httpContextAccessor.HttpContext.Session.GetString("viewUpdatedGrid");

                CustomerCards vGrid = JsonConvert.DeserializeObject<CustomerCards>(str);

                searchBody = new CustomerCards
                {
                    UserId = UserName,
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = vGrid.CustomerId,
                    CardNo = vGrid.CardNo,
                    MobileNo = vGrid.MobileNo,
                    VehicleNumber = vGrid.VehicleNumber,
                    StatusFlag = vGrid.StatusFlag
                };
            }
            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.SearchCardUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            SearchManageCards searchList = obj.ToObject<SearchManageCards>();
            return searchList;
        }

        public async Task<SearchDetailsByCardId> ViewCardDetails(string CardId)
        {
            _httpContextAccessor.HttpContext.Session.SetString("CardIdSession", CardId);

            var cardDetailsBody = new CardsSearch
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CardNo = CardId,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(cardDetailsBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCardDetailsUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            SearchDetailsByCardId searchRes = obj.ToObject<SearchDetailsByCardId>();

            string cusId = string.Empty;
            foreach (var item in searchRes.Data.GetCardsDetailsModelOutput)
            {
                cusId = item.CustomerID;
            }
            _httpContextAccessor.HttpContext.Session.SetString("CustomerIdSession", cusId);

            return searchRes;
        }

        public async Task<string> UpdateService(string serviceId, bool flag)
        {
            var updateServiceBody = new UpdateService
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = _httpContextAccessor.HttpContext.Session.GetString("CustomerIdSession"),
                CardNo = _httpContextAccessor.HttpContext.Session.GetString("CardIdSession"),
                ServiceId = Convert.ToInt32(serviceId),
                Flag = Convert.ToInt32(flag),
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(updateServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateServiceUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var updateRes = obj["Data"].Value<JArray>();
            List<SuccessResponse> updateResponse = updateRes.ToObject<List<SuccessResponse>>();

            return updateResponse[0].Reason;
        }

        public async Task<UpdateMobileModal> CardlessMapping(string cardNumber, string mobileNumber, string LimitTypeName, string CCMSReloadSaleLimitValue)
        {
            UpdateMobileModal editMobBody = new UpdateMobileModal();
            editMobBody.CardNumber = cardNumber;
            editMobBody.MobileNumber = mobileNumber;
            editMobBody.LimitTypeName = LimitTypeName;
            editMobBody.CCMSReloadSaleLimitValue = CCMSReloadSaleLimitValue;

            _httpContextAccessor.HttpContext.Session.SetString("lmtType", editMobBody.LimitTypeName);
            return editMobBody;
        }

        public async Task<List<SuccessResponse>> CardlessMappingUpdate(string mobNoNew, string crdNo)
        {
            var cardDetailsBody = new UpdateMobile
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CardNo = crdNo,
                MobileNo = mobNoNew,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(cardDetailsBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateMobileUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var updateRes = obj["Data"].Value<JArray>();
            List<SuccessResponse> updateResponse = updateRes.ToObject<List<SuccessResponse>>();
            return updateResponse;
        }

        public async Task<SearchCardsResponse> AcDcCardSearch(SearchCards entity)
        {
            var searchBody = new SearchCards();

            if (entity.CustomerId != null)
            {
                searchBody = new SearchCards
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = entity.CustomerId,
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo
                };
                _httpContextAccessor.HttpContext.Session.SetString("AcDccustId", entity.CustomerId);
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new SearchCards
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("AcDccustId") != "")
            {
                searchBody = new SearchCards
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = _httpContextAccessor.HttpContext.Session.GetString("AcDccustId"),
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetAllCardStatusUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            SearchCardsResponse searchList = obj.ToObject<SearchCardsResponse>();
            return searchList;
        }

        public async Task<string> UpdateStatus(string cardNo, int Statusflag)
        {
            var updateServiceBody = new UpdateStatus
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CardNo = cardNo,
                Statusflag = Statusflag,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(updateServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateCardStatusUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var updateRes = obj["Data"].Value<JArray>();
            List<SuccessResponse> updateResponse = updateRes.ToObject<List<SuccessResponse>>();
            return updateResponse[0].Reason;
        }

        public async Task<GetCardLimitResponse> SetSaleLimit(GetCardLimit entity)
        {
            var searchBody = new GetCardLimit();

            if (entity.CustomerId != null)
            {
                searchBody = new GetCardLimit
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = entity.CustomerId,
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo,
                    Statusflag = entity.Statusflag
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new GetCardLimit
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    Statusflag = -1,
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo,
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCardLimitUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetCardLimitResponse searchList = obj.ToObject<GetCardLimitResponse>();
            return searchList;
        }

        public async Task<SetSaleLimitUpdateRes> UpdateCards(ObjCardLimits[] limitArray)
        {
            var updateServiceBody = new UpdateCardLimit
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                objCardLimits = limitArray,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(updateServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateCardLimitUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            SetSaleLimitUpdateRes updateResponse = obj.ToObject<SetSaleLimitUpdateRes>();

            return updateResponse;
        }

        public async Task<SearchCcmsLimitAllResponse> SearchCcmsLimitForAllCards(GetCcmsLimitAll entity)
        {
            var reqBody = new GetCcmsLimitAll();

            if (entity.CustomerId != null)
            {
                reqBody = new GetCcmsLimitAll
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = entity.CustomerId,
                    StatusFlag = entity.StatusFlag
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                reqBody = new GetCcmsLimitAll
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    StatusFlag = entity.StatusFlag
                };
            }

            _httpContextAccessor.HttpContext.Session.SetString("CCMSCustomerId", reqBody.CustomerId);

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.SearchCcmsAllCardLimitUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            SearchCcmsLimitAllResponse searchCcmsCard = obj.ToObject<SearchCcmsLimitAllResponse>();
            return searchCcmsCard;
        }

        public async Task<string> UpdateCcmsLimitAllCards(int limitype, int amountVal)
        {
            var reqBody = new UpdateCcmsLimitAll
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = _httpContextAccessor.HttpContext.Session.GetString("CCMSCustomerId"),
                LimitType = limitype,
                Amount = amountVal,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateCcmsAllCardLimitUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> searchCcmsCard = jarr.ToObject<List<SuccessResponse>>();
            return searchCcmsCard[0].Reason;
        }

        public async Task<IndividualCardResponse> SetCcmsForIndCards(SetIndividualLimit entity)
        {
            var searchBody = new SetIndividualLimit();

            if (entity.CustomerId != null)
            {
                searchBody = new SetIndividualLimit
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = entity.CustomerId,
                    CardNo = entity.CardNo ?? "",
                    MobileNo = entity.MobileNo ?? "",
                    VechileNo = entity.VechileNo ?? ""
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new SetIndividualLimit
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    CardNo = entity.CardNo ?? "",
                    MobileNo = entity.MobileNo ?? "",
                    VechileNo = entity.VechileNo ?? ""
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.SearchCcmsIndividualCardLimitUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            IndividualCardResponse searchList = obj.ToObject<IndividualCardResponse>();

            return searchList;
        }

        public async Task<string> UpdateCcmsIndividualCard(string objCCMSLimits, string viewGirds)
        {
            _httpContextAccessor.HttpContext.Session.SetString("viewGrid", viewGirds);

            ObjCCMSLimits[] arrs = JsonConvert.DeserializeObject<ObjCCMSLimits[]>(objCCMSLimits);

            var searchBody = new UpdateCcmsIndLimit
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ObjCCMSLimits = arrs,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateCcmsIndividualCardLimitUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> searchList = jarr.ToObject<List<SuccessResponse>>();
            _httpContextAccessor.HttpContext.Session.SetString("ResMsg", searchList[0].Reason);
            _httpContextAccessor.HttpContext.Session.SetInt32("ResMsgCode", searchList[0].Status);
            return searchList[0].Reason;
        }

        public async Task<List<ViewGird>> ViewUpdatedGrid()
        {
            var str = _httpContextAccessor.HttpContext.Session.GetString("viewGrid");
            JArray obj = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            List<ViewGird> vGrid = obj.ToObject<List<ViewGird>>();
            return vGrid;
        }

        public async Task<List<CardListResponse>> GetCardList()
        {
            var forms = new GetCardList
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCardListUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CardListResponse> cardList = jarr.ToObject<List<CardListResponse>>();
            return cardList;
        }

        public async Task<CustomerSearchDetails> ManageMapping(GetCustomerDetailsMapMerchant entity)
        {
            var reqBody = new GetCustomerDetailsMapMerchant
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = entity.CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCustMapDetailsUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            CustomerSearchDetails searchlist = obj.ToObject<CustomerSearchDetails>();

            return searchlist;
        }

        public async Task<FetchMerchant> GetMerchantForMapping(GetCustomerDetailsMapMerchant entity)
        {
            var merchantDetails = new GetCustomerDetailsMapMerchant
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                MerchantID = entity.MerchantID,
                StateID = entity.StateID,
                DistrictID = entity.DistrictID,
                City = entity.City,
                HighwayName = entity.HighwayName,
                HighwayNo1 = "",
                HighwayNo2 = entity.HighwayNo2
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(merchantDetails), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetMerchantForMapUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            FetchMerchant merchantList = obj.ToObject<FetchMerchant>();
            return merchantList;
        }

        public async Task<string> SaveCustomerMappingMerchant(string objCardMerchantMaps, string status)
        {
            ObjCardMerchantMap[] arrs = JsonConvert.DeserializeObject<ObjCardMerchantMap[]>(objCardMerchantMaps);

            var reqBody = new SaveCustomerCardMerchantMappingReq
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjCardMerchantMap = arrs,
                Status = status
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.SaveCustomerMerchantMapUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> searchCcmsCard = jarr.ToObject<List<SuccessResponse>>();
            return searchCcmsCard[0].Reason;
        }

        public async Task<SearchAllowedMerchantResponse> SearchAllowedMerchant(SearchAllowedMerchant entity)
        {
            var reqBody = new SearchAllowedMerchant
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CardNumber = entity.CardNumber,
                MerchantId = entity.MerchantId,
                CustomerId = entity.CustomerId
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.MappingAllowedCardsToMerchantUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            SearchAllowedMerchantResponse searchList = obj.ToObject<SearchAllowedMerchantResponse>();
            return searchList;
        }

        public async Task<LimitUpdateForSingleRechargeCardsRes> LimitUpdateForSingleRechargeCardsService(GetLimitUpdateForSingleRechargeCards entity)
        {
            var searchBody = new GetLimitUpdateForSingleRechargeCards();

            if (entity.CustomerID != null)
            {
                searchBody = new GetLimitUpdateForSingleRechargeCards
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerID = entity.CustomerID,
                    Cardno = entity.Cardno ?? ""
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new GetLimitUpdateForSingleRechargeCards
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerID = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    Cardno = entity.Cardno ?? ""
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.LimitUpdateForSingleRechargeCardsUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            LimitUpdateForSingleRechargeCardsRes searchList = obj.ToObject<LimitUpdateForSingleRechargeCardsRes>();
            return searchList;
        }

        public async Task<List<SuccessResponse>> LimitUpdateForSingleRecharge(string objCCMSLimits)
        {
            ObjCCMSLimitsRec[] arrs = JsonConvert.DeserializeObject<ObjCCMSLimitsRec[]>(objCCMSLimits);

            var reqBody = new LimitUpdateForSingleRechargeReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                objCCMSLimits = arrs,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.LimitUpdateForSingleRechargeUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }

        public async Task<GetEmergencyAddOnCardResponse> EmergencyAddOnCard(GetEmergencyAddOnCard entity)
        {
            var searchBody = new GetEmergencyAddOnCard();

            if (entity.CustomerId != null)
            {
                searchBody = new GetEmergencyAddOnCard
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = entity.CustomerId
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new GetEmergencyAddOnCard
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetEmergencyAddOnCardUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetEmergencyAddOnCardResponse searchList = obj.ToObject<GetEmergencyAddOnCardResponse>();
            return searchList;
        }

        public async Task<List<SuccessResponse>> MapEmergencyAddOnCard(string objCards)
        {
            objEmergencyReplacementCards[] arrs = JsonConvert.DeserializeObject<objEmergencyReplacementCards[]>(objCards);

            var reqBody = new MapEmergencyAddOnCardReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                objEmergencyReplacementCards = arrs,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.MapEmergencyAddOnCardUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }

        public async Task<EnableCustomerServicesModel> EnableCustomerServices()
        {
            EnableCustomerServicesModel custMdl = new EnableCustomerServicesModel();
            custMdl.Remarks = "";

            return custMdl;
        }
        public async Task<GetDetailForEnableDisableProductsAndTransactions> GetDetailForEnableDisableProductsAndTransactions(string CustomerId, string CardNo, string MobileNo)
        {
            var requestBody = new EnableCustomerServicesModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = CustomerId,
                CardNo = CardNo,
                MobileNo = MobileNo
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetDetailForEnableDisableProductsAndTransactions);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetDetailForEnableDisableProductsAndTransactions searchList = obj.ToObject<GetDetailForEnableDisableProductsAndTransactions>();
            return searchList;
        }

        public async Task<CommonResponseData> EnableDisableProductsAndTransaction(string ObjProductsInput, string ObjTransactionsInput, string CustomerId, string CardNo, string MobileNo)
        {
            ProductTypeDetails[] arrs = JsonConvert.DeserializeObject<ProductTypeDetails[]>(ObjProductsInput);
            TransactionTypeDetails[] arrs1 = JsonConvert.DeserializeObject<TransactionTypeDetails[]>(ObjTransactionsInput);

            var requestBody = new EnableDisableProductsAndTransactionsRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerId = CustomerId,
                CardNo = CardNo,
                MobileNo = MobileNo,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjProducts = arrs,
                ObjTransactions = arrs1
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.EnableDisableProductsAndTransactions);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            CommonResponseData responseData = new CommonResponseData();
            if (obj["Status_Code"].ToString() == "200")
            {
                var Jarr = obj["Data"].Value<JArray>();
                List<CommonResponseData> responseLst = Jarr.ToObject<List<CommonResponseData>>();
                responseData = responseLst[0];
                responseData.Internel_Status_Code = Convert.ToInt32(obj["Internel_Status_Code"].ToString());
            }
            else
            {
                responseData.Internel_Status_Code = Convert.ToInt32(obj["Internel_Status_Code"].ToString());
                responseData.Status = Convert.ToInt32(obj["Status_Code"].ToString());
            }

            return responseData;
        }
        public async Task<CorporateMultiRechargeLimitModel> GetCustomerRechargeLimitConfig(string CustomerId)
        {
            CorporateMultiRechargeLimitModel corporateMultiRechargeLimitModel = new CorporateMultiRechargeLimitModel();
            var reqBody = new CorporateMultiRechargeLimitRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID =CustomerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getdetailforcorpmultirechargelimitconfig);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            corporateMultiRechargeLimitModel = obj.ToObject<CorporateMultiRechargeLimitModel>();
            return corporateMultiRechargeLimitModel;
        }
        public async Task<List<SuccessResponse>> ConfigureLimits([FromBody] CorporateMultiRechargeLimitRequest reqModel)
        {
            var reqBody = new CorporateMultiRechargeLimitRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ObjLimitConfig = reqModel.ObjLimitConfig
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.corpmultirechargelimitconfig);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res;
        }

        public async Task<GetGenericAttachedVehicleRes> GetGenericAttachedVehicle(GetGenericAttachedVehicleReq entity)
        {

            var searchBody = new GetGenericAttachedVehicleReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                Customerid = entity.Customerid,
                Cardno = entity.Cardno,
                Mobileno=entity.Mobileno,
                Vehiclenumber=entity.Vehiclenumber,
                Statusflag = entity.Statusflag
            };


            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetGenericAttachedVehicleUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetGenericAttachedVehicleRes searchList = obj.ToObject<GetGenericAttachedVehicleRes>();
            return searchList;
        }
    }
}