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

        public async Task<CustomerCards> ManageCards()
        {
            CustomerCards modals = new CustomerCards();

            var statusType = new StatusType
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                EntityTypeId = 3
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(statusType), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetStatusTypeUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<StatusModal> lst = jarr.ToObject<List<StatusModal>>();

            List<StatusModal> lsts = new List<StatusModal>();
            lsts.Add(new StatusModal { StatusId = -1, StatusName = "All" });
            foreach (var item in lst)
            {
                if (item.StatusId == 4 || item.StatusId == 6 || item.StatusId == 7)
                {
                    lsts.Add(item);
                }
            }
            modals.StatusModals.AddRange(lsts);
            return modals;
        }

        public async Task<List<SearchGridResponse>> ManageCards(CustomerCards entity, string editFlag)
        {
            var searchBody = new CustomerCards();

            var UserName = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            if (entity.CustomerId != null)
            {
                searchBody = new CustomerCards
                {
                    UserId = UserName,
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = entity.CustomerId,
                    CardNo = entity.CardNo,
                    MobileNumber = entity.MobileNumber,
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
                    StatusFlag = -1
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
                    MobileNumber = vGrid.MobileNumber,
                    VehicleNumber = vGrid.VehicleNumber,
                    StatusFlag = vGrid.StatusFlag
                };
            }
            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.SearchCardUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SearchGridResponse> searchList = jarr.ToObject<List<SearchGridResponse>>();
            return searchList;
        }

        public async Task<Tuple<List<SearchCardResult>, List<LimitResponse>, List<ServicesResponse>>> ViewCardDetails(string CardId)
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

            var searchRes = obj["Data"].Value<JObject>();
            var cardResult = searchRes["GetCardsDetailsModelOutput"].Value<JArray>();
            var limitResult = searchRes["GetCardLimtModel"].Value<JArray>();
            var serviceResult = searchRes["CardServices"].Value<JArray>();
            List<SearchCardResult> cardDetailsList = cardResult.ToObject<List<SearchCardResult>>();
            List<LimitResponse> limitDetailsList = limitResult.ToObject<List<LimitResponse>>();
            List<ServicesResponse> servicesDetailsList = serviceResult.ToObject<List<ServicesResponse>>();
            string cusId = string.Empty;
            foreach (var item in cardDetailsList)
            {
                cusId = item.CustomerID;
            }
            _httpContextAccessor.HttpContext.Session.SetString("CustomerIdSession", cusId);

            return Tuple.Create(cardDetailsList, limitDetailsList, servicesDetailsList);
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
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserName")
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

        public async Task<string> CardlessMapping(UpdateMobileModal entity)
        {
            var cardDetailsBody = new UpdateMobile
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CardNo = entity.CardNumber,
                MobileNo = entity.MobileNumber,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(cardDetailsBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateMobileUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var updateRes = obj["Data"].Value<JArray>();
            List<SuccessResponse> updateResponse = updateRes.ToObject<List<SuccessResponse>>();
            return updateResponse[0].Reason;
        }

        public async Task<List<SearchCardsResponse>> AcDcCardSearch(SearchCards entity)
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
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new SearchCards
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetAllCardStatusUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SearchCardsResponse> searchList = jarr.ToObject<List<SearchCardsResponse>>();
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

        public async Task<List<SearchCardsResponse>> RefreshGrid()
        {
            var searchBody = new SearchCards
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = _httpContextAccessor.HttpContext.Session.GetString("AcDcCustomerId"),
                CardNo = "",
                MobileNo = ""
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetAllCardStatusUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SearchCardsResponse> searchList = jarr.ToObject<List<SearchCardsResponse>>();
            return searchList;
        }

        public async Task<GetCardLimit> SetSaleLimit()
        {
            GetCardLimit modals = new GetCardLimit();

            var statusType = new StatusType
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                EntityTypeId = 3
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(statusType), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetStatusTypeUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<StatusModal> lst = jarr.ToObject<List<StatusModal>>();

            List<StatusModal> lsts = new List<StatusModal>();
            lsts.Add(new StatusModal { StatusId = -1, StatusName = "All" });
            foreach (var item in lst)
            {
                if (item.StatusId == 4 || item.StatusId == 1)
                {
                    lsts.Add(item);
                }
            }
            modals.CardStatusList.AddRange(lsts);
            return modals;
        }

        public async Task<List<GetCardLimitResponse>> SetSaleLimit(GetCardLimit entity)
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
                    Statusflag = -1
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCardLimitUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<GetCardLimitResponse> searchList = jarr.ToObject<List<GetCardLimitResponse>>();
            return searchList;
        }

        public async Task<string> UpdateCards(ObjCardLimits[] limitArray)
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

            var updateRes = obj["Data"].Value<JArray>();
            List<SuccessResponse> updateResponse = updateRes.ToObject<List<SuccessResponse>>();
            return updateResponse[0].Reason;
        }

        public async Task<GetCcmsLimitAll> SetCcmsLimitForAllCards()
        {
            GetCcmsLimitAll modals = new GetCcmsLimitAll();

            var statusType = new StatusType
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                EntityTypeId = 3
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(statusType), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetStatusTypeUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<StatusModal> lst = jarr.ToObject<List<StatusModal>>();

            List<StatusModal> lsts = new List<StatusModal>();
            lsts.Add(new StatusModal { StatusId = -1, StatusName = "All" });
            foreach (var item in lst)
            {
                if (item.StatusId == 4)
                {
                    lsts.Add(item);
                }
            }
            modals.CardStatusList.AddRange(lsts);

            var forms = new Dictionary<string, string>
                {
                    {"useragent", CommonBase.useragent},
                    {"userip", CommonBase.userip},
                    {"userid", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
                };

            StringContent contents = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var responseContent = await _requestService.CommonRequestService(contents, WebApiUrl.GetLimitTypeUrl);

            JObject objj = JObject.Parse(JsonConvert.DeserializeObject(responseContent).ToString());
            var jar = objj["Data"].Value<JArray>();
            List<LimitTypeModal> limitType = jar.ToObject<List<LimitTypeModal>>();

            modals.LimitTypeModals.AddRange(limitType);
            return modals;
        }

        public async Task<List<SearchCcmsLimitAllResponse>> SearchCcmsLimitForAllCards(GetCcmsLimitAll entity)
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
                    StatusFlag = -1
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.SearchCcmsAllCardLimitUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SearchCcmsLimitAllResponse> searchCcmsCard = jarr.ToObject<List<SearchCcmsLimitAllResponse>>();
            return searchCcmsCard;
        }

        public async Task<string> UpdateCcmsLimitAllCards(GetCcmsLimitAll entity)
        {
            var reqBody = new UpdateCcmsLimitAll
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = _httpContextAccessor.HttpContext.Session.GetString("CCMSCustomerId"),
                LimitType=entity.TypeOfLimit,
                Amount = entity.CcmsLimit,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateCcmsAllCardLimitUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> searchCcmsCard = jarr.ToObject<List<SuccessResponse>>();
            return searchCcmsCard[0].Reason;
        }

        public async Task<Tuple<List<SearchIndividualCardsResponse>, List<CCMSBalanceDetail>>> SetCcmsForIndCards(SetIndividualLimit entity)
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
                    VehicleNo = entity.VehicleNo ?? ""
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new SetIndividualLimit
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.SearchCcmsIndividualCardLimitUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JObject>();

            var gridResponse = jarr["CCMSBasicDetail"].Value<JArray>();
            var balanceAmuntResponse = jarr["CCMSBalanceDetail"].Value<JArray>();

            List<SearchIndividualCardsResponse> searchList = gridResponse.ToObject<List<SearchIndividualCardsResponse>>();
            List<CCMSBalanceDetail> amounts = balanceAmuntResponse.ToObject<List<CCMSBalanceDetail>>();

            return Tuple.Create(searchList, amounts);
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
    }
}
