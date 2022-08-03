using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.Customer;
using HPCL.Common.Models.ResponseModel.CustomerRequest;
using HPCL.Common.Models.ViewModel.CustomerRequest;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class CustomerRequestService : ICustomerRequestService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;

        public CustomerRequestService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }

        public async Task<GetSmsAlertForMultipleMobileDetailRes> GetSmsAlertForMultipleMobileDetail(GetSmsAlertForMultipleMobileDetailReq entity)
        {
            var searchBody = new GetSmsAlertForMultipleMobileDetailReq();

            if (entity.CustomerID != null)
            {
                searchBody = new GetSmsAlertForMultipleMobileDetailReq
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CustomerID = entity.CustomerID
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new GetSmsAlertForMultipleMobileDetailReq
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CustomerID = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetSmsAlertForMultipleMobileDetailUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetSmsAlertForMultipleMobileDetailRes searchList = obj.ToObject<GetSmsAlertForMultipleMobileDetailRes>();
            return searchList;
        }

        public async Task<List<SuccessResponse>> UpdateSmsAlertForMultipleMobileDetail(string customerDetailForSmsAlert)
        {
            CustomerDetailForSmsAlert[] arrs = JsonConvert.DeserializeObject<CustomerDetailForSmsAlert[]>(customerDetailForSmsAlert);

            var reqBody = new UpdateSmsAlertForMultipleMobileDetailReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerDetailForSmsAlert = arrs
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateSmsAlertForMultipleMobileDetailUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }

        public async Task<List<SuccessResponse>> DeleteSmsAlertForMultipleMobileDetail(string CustomerID, string MobileNo)
        {
            var reqBody = new DeleteSmsAlertForMultipleMobileDetailReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID = CustomerID,
                MobileNo = MobileNo
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.DeleteSmsAlertForMultipleMobileDetailUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }

        public async Task<GetCardRenwalRequestListRes> GetCardRenwalRequest(GetCardRenwalRequestList entity)
        {
            var searchBody = new GetCardRenwalRequestList();

            if (entity.CustomerId != null)
            {
                searchBody = new GetCardRenwalRequestList
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CustomerId = entity.CustomerId,
                    CardNo = entity.CardNo ?? ""
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new GetCardRenwalRequestList
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CustomerId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    CardNo = entity.CardNo ?? ""
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCardRenwalRequestUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetCardRenwalRequestListRes searchList = obj.ToObject<GetCardRenwalRequestListRes>();
            return searchList;
        }

        public async Task<List<SuccessResponse>> UpdateCardRenwalRequest(string CustomerId, string updatePostArray)
        {
            TypeCardRenewalRequest[] arrs = JsonConvert.DeserializeObject<TypeCardRenewalRequest[]>(updatePostArray);

            var reqBody = new UpdateCardRenwalRequestPost
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = CustomerId,
                TypeCardRenewalRequest = arrs,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateCardRenwalRequestUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }

        public async Task<ConfigureSmsAlertsRes> GetSmsAlertsConfigure(GetConfigureSmsAlerts entity)
        {
            var searchBody = new GetConfigureSmsAlerts();

            if (entity.CustomerID != null)
            {
                searchBody = new GetConfigureSmsAlerts
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CustomerID = entity.CustomerID
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new GetConfigureSmsAlerts
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CustomerID = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetConfigureSmsAlertsUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            ConfigureSmsAlertsRes searchList = obj.ToObject<ConfigureSmsAlertsRes>();
            return searchList;
        }

        public async Task<List<SuccessResponse>> UpdateSmsAlertsConfigure(string CustomerId, string SmsAlertList)
        {
            TypeConfigureSMSAlerts[] arrs = JsonConvert.DeserializeObject<TypeConfigureSMSAlerts[]>(SmsAlertList);

            var reqBody = new UpdateSmsAlertsConfigureReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = CustomerId,
                TypeConfigureSMSAlerts = arrs,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateConfigureSmsAlertsUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }

        public async Task<List<SuccessResponse>> UpdateDndSmsAlertsConfigure(string CustomerId)
        {
            var reqBody = new UpdateDndSmsAlertsConfigureReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = CustomerId,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateDndSmsAlertsConfigureUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }

        public async Task<GetHotlistCardsPermanentlyRes> HotlistCardsPermanently(GetHotlistCardsPermanently entity)
        {
            var searchBody = new GetHotlistCardsPermanently();

            if (entity.CustomerID != null || entity.CardNo != null)
            {
                searchBody = new GetHotlistCardsPermanently
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CustomerID = entity.CustomerID,
                    CardNo = entity.CardNo
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new GetHotlistCardsPermanently
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CustomerID = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    CardNo = entity.CardNo
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetHotlistCardsPermanentlyUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetHotlistCardsPermanentlyRes searchList = obj.ToObject<GetHotlistCardsPermanentlyRes>();
            return searchList;
        }

        public async Task<List<UpdateHotlistCardRes>> UpdatePermanentlyHotlistCards(string CustomerId, string cardsList)
        {
            TypePermanentlyHotlistCards[] arrs = JsonConvert.DeserializeObject<TypePermanentlyHotlistCards[]>(cardsList);

            var reqBody = new UpdatePermanentlyHotlistCardsRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = CustomerId,
                TypePermanentlyHotlistCards = arrs,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdatePermanentlyHotlistCardsUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<UpdateHotlistCardRes> reasonList = jarr.ToObject<List<UpdateHotlistCardRes>>();
            return reasonList;
        }
        public async Task<ConfigureEmailAlertViewModel> ConfigureEmailAlerts(string CustomerId)
        {
            ConfigureEmailAlertViewModel configureEmailAlertViewModel = new ConfigureEmailAlertViewModel();
            var reqBody = new ConfigureEmailAlertRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerId = CustomerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getconfigureemailalerts);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            configureEmailAlertViewModel = obj.ToObject<ConfigureEmailAlertViewModel>();
            return configureEmailAlertViewModel;
        }
        public async Task<List<SuccessResponse>> UpdateConfigureEmailAlert([FromBody] ConfigureEmailAlertRequest reqModel)
        {
            var reqBody = new ConfigureEmailAlertRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerId=reqModel.CustomerId,
                objConfigureEmailAlert = reqModel.objConfigureEmailAlert
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.updateconfigureemailalert);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res;
        }

        public async Task<ApproveCardRenwalRequestRes> GetApproveCardRenwalRequest(ApproveCardRenwalRequestReq entity)
        {
            string fromDate = "", toDate = "";
            if (!string.IsNullOrEmpty(entity.FromDate) && !string.IsNullOrEmpty(entity.FromDate))
            {
                fromDate = await _commonActionService.changeDateFormat(entity.FromDate);
                toDate = await _commonActionService.changeDateFormat(entity.ToDate);
            }
            else
            {
                fromDate = DateTime.Now.ToString("yyyy-MM-dd");
                toDate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            var reqBody = new ApproveCardRenwalRequestReq
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CardNo = entity.CardNo ?? "",
                FromDate = fromDate,
                ToDate = toDate
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetApproveCardRenewReqUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            ApproveCardRenwalRequestRes searchList = obj.ToObject<ApproveCardRenwalRequestRes>();
            return searchList;
        }

        public async Task<List<SuccessResponse>> UpdateApproveCardRenwalRequest(string actionType, string appRejValues)
        {
            TypeApproveCardRenewalRequestsList[] arrs = JsonConvert.DeserializeObject<TypeApproveCardRenewalRequestsList[]>(appRejValues);

            var reqBody = new UpdateApproveCardRenwalRequestReq
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ActionType = actionType,
                TypeApproveCardRenewalRequests = arrs,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateApproveCardRenewReqUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res;
        }

        public async Task<SearchHotlistAndReissueRes> SearchReissueCard(SearchHotlistAndReissue entity)
        {
            var reqBody = new SearchHotlistAndReissue
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CardNo = entity.CardNo,
                CustomerID = entity.CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.SearchReissueCardUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            SearchHotlistAndReissueRes searchList = obj.ToObject<SearchHotlistAndReissueRes>();
            return searchList;
        }

        public async Task<List<InitialReissueCardRes>> InitialReissueCardService(string customerId, string reissueReq)
        {
            TypeHotlistReissueCardRequests[] arrs = JsonConvert.DeserializeObject<TypeHotlistReissueCardRequests[]>(reissueReq);

            var reqBody = new InitialReissueCard
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerId = customerId,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                TypeHotlistReissueCardRequest = arrs
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ApplyReissueCardUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<InitialReissueCardRes> resp = jarr.ToObject<List<InitialReissueCardRes>>();
            return resp;
        }
    }
}
