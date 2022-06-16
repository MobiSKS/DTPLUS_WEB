﻿using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.CCMSRecharge;
using HPCL.Common.Models.ViewModel.CCMSRecharge;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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

        public async Task<GetDetailsByMobRes> GetDetailsByMObNoCust(string mobNo, string customerId)
        {
            var searchBody = new GetDetailsByMob
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                MobileNo = mobNo,
                customerId = customerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetDetailsByMobNoUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetDetailsByMobRes searchList = obj.ToObject<GetDetailsByMobRes>();
            return searchList;
        }

        public async Task<RedirectToPGResponse> RedirectToPG(string customerId, string mobNo, string controlCardNo, string amount)
        {
            var searchBody = new RedirectToPGRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                MobileNo = mobNo,
                Amount = Convert.ToDecimal(amount),
                controlCardNo = controlCardNo,
                customerId = customerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.RedirectToPGUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            RedirectToPGResponse redirectDetails = obj.ToObject<RedirectToPGResponse>();
            return redirectDetails;
        }
    }
}
