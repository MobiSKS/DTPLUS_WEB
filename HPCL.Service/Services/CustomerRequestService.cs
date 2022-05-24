using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.CustomerRequest;
using HPCL.Common.Models.ViewModel.CustomerRequest;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public CustomerRequestService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
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
                    UserIp = CommonBase.userip,
                    CustomerID = entity.CustomerID
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new GetSmsAlertForMultipleMobileDetailReq
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
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
                UserIp = CommonBase.userip,
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
                UserIp = CommonBase.userip,
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
                    UserIp = CommonBase.userip,
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
                    UserIp = CommonBase.userip,
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
                UserIp = CommonBase.userip,
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
                    UserIp = CommonBase.userip,
                    CustomerID = entity.CustomerID
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new GetConfigureSmsAlerts
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerID = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetConfigureSmsAlertsUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            ConfigureSmsAlertsRes searchList = obj.ToObject<ConfigureSmsAlertsRes>();
            return searchList;
        }
    }
}
