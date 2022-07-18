using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.CCMSRecharge;
using HPCL.Common.Models.ViewModel.CCMSRecharge;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class CCMSRechargeService : ICCMSRechargeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public CCMSRechargeService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<GetDetailsByMobRes> GetDetailsByMObNoCust(string mobNo, string customerId, string useriprecharge)
        {
            var searchBody = new GetDetailsByMob
            {
                UserId = "demo",
                UserAgent = CommonBase.useragent,
                UserIp = useriprecharge,
                MobileNo = mobNo,
                customerId = customerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.RechargeRequestService(content, WebApiUrl.GetDetailsByMobNoUrl, useriprecharge);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetDetailsByMobRes searchList = obj.ToObject<GetDetailsByMobRes>();
            return searchList;
        }

        public async Task<RedirectToPGResponse> RedirectToPG(string customerId, string mobNo, string controlCardNo, string amount, string useriprecharge)
        {
            var searchBody = new RedirectToPGRequest
            {
                UserId = "demo",
                UserAgent = CommonBase.useragent,
                UserIp = useriprecharge,
                MobileNo = mobNo,
                Amount = Convert.ToDecimal(amount),
                controlCardNo = controlCardNo,
                customerId = customerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.RechargeRequestService(content, WebApiUrl.RedirectToPGUrl, useriprecharge);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            RedirectToPGResponse redirectDetails = obj.ToObject<RedirectToPGResponse>();
            return redirectDetails;
        }

        public async Task<CCCMSRecGenerateOtpRes> CCCMSRecGenerateOtp(string mobNo, string customerId, string useriprecharge)
        {
            var reqBody = new CCCMSRecGenerateOtpReq
            {
                UserId = "demo",
                UserAgent = CommonBase.useragent,
                UserIp = useriprecharge,
                Mobileno = mobNo ?? "",
                CustomerId = customerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.RechargeRequestService(content, WebApiUrl.CcmsRecGenerateOtp, useriprecharge);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            CCCMSRecGenerateOtpRes getOtp = obj.ToObject<CCCMSRecGenerateOtpRes>();
            return getOtp;
        }

        public async Task<List<CCCMSRecVerifyOtpRes>> CCCMSRecVerifyOtp(string mobNo, string otp, string customerId, string useriprecharge)
        {
            var reqBody = new CCCMSRecVerifyOtpReq
            {
                UserId = "demo",
                UserAgent = CommonBase.useragent,
                UserIp = useriprecharge,
                MobileNo = mobNo,
                otp = otp,
                customerId = customerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.RechargeRequestService(content, WebApiUrl.CcmsRecVerifyOtp, useriprecharge);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CCCMSRecVerifyOtpRes> verifyOtp = jarr.ToObject<List<CCCMSRecVerifyOtpRes>>();
            return verifyOtp;
        }
    }
}
