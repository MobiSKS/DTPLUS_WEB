﻿using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.AMEXBankCreditPouch;
using HPCL.Common.Models.ViewModel.AMEXBankCreditPouch;
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
    public class AMEXBankCreditPouchService : IAMEXBankCreditPouchService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;

        public AMEXBankCreditPouchService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }

        public async Task<CustomerDetailsRes> GetCustomerDetails(CustomerDetailsReq entity)
        {
            var searchBody = new CustomerDetailsReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = entity.CustomerId,
                RequestedBy = entity.CustomerId
            };


            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.AMEXGetCustomerDetailsUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            CustomerDetailsRes searchList = obj.ToObject<CustomerDetailsRes>();
            return searchList;
        }

        public async Task<GetPlanRes> GetPlan(string amount)
        {
            var searchBody = new GetPlan
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                Amount = Convert.ToDecimal(amount)
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetPlanUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetPlanRes searchList = obj.ToObject<GetPlanRes>();
            return searchList;
        }

        public async Task<List<SuccessResponse>> InsertExceptionRequest(EnrollExceptionRequest entity)
        {
            var searchBody = new EnrollExceptionRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = entity.CustomerId,
                FuleConsumptionCapacity = entity.FuleConsumptionCapacity,
                PlanTypeId = entity.PlanTypeId,
                ReferenceNo = entity.ReferenceNo,
                MoComment = entity.MoComment,
                RequestedBy = entity.RequestedBy,
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.AMEXEnrollExceptionReqUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }

        public async Task<SearchRequestApprovalRes> SearchRequestApproval(SearchRequestApprovalClone entity)
        {
            var searchBody = new SearchRequestApproval
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = entity.CustomerId,
                ZO = Convert.ToInt32(entity.ZO),
                RO = Convert.ToInt32(entity.RO),
                Status = entity.Status
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.AMEXGetExApprovalUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            SearchRequestApprovalRes searchList = obj.ToObject<SearchRequestApprovalRes>();
            return searchList;
        }

        public async Task<List<SuccessResponse>> SubmitRequestApproval(string bankEntryDetail)
        {
            ObjBankEntryDetail[] arrs = JsonConvert.DeserializeObject<ObjBankEntryDetail[]>(bankEntryDetail);

            var searchBody = new ActionApproveRejectReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ObjBankEntryDetail = arrs
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.AMEXSubmitExApprovalUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }

        public async Task<SearchEnrollStatusRes> GetEnrollStatus(SearchEnrollStatusClone entity)
        {
            string fromDate = "", toDate = "";
            if (!string.IsNullOrEmpty(entity.FromDate) && !string.IsNullOrEmpty(entity.FromDate))
            {
                fromDate = await _commonActionService.changeDateFormat(entity.FromDate);
                toDate = await _commonActionService.changeDateFormat(entity.ToDate);
            }
            else
            {
                fromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                toDate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            var searchBody = new SearchEnrollStatus
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = entity.CustomerId,
                ZO = Convert.ToInt32(entity.ZO),
                RO = Convert.ToInt32(entity.RO),
                FromDate = entity.FromDate,
                ToDate = entity.ToDate
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.AMEXGetEnrollStatusUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            SearchEnrollStatusRes searchList = obj.ToObject<SearchEnrollStatusRes>();
            return searchList;
        }

        public async Task<GetEnrollStatusReportRes> GetEnrollStatusReport(string customerId, int requestId)
        {
            var searchBody = new GetEnrollStatusReport
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = customerId,
                RequestId = requestId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.AMEXGetEnrollStatusReportUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetEnrollStatusReportRes searchList = obj.ToObject<GetEnrollStatusReportRes>();
            return searchList;
        }

        public async Task<CcmsRechargeHdfcRes> CCMSRechargeHDFC(string customerId, string amount)
        {
            var searchBody = new CcmsRechargeHdfc
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = customerId,
                Amount = Convert.ToDecimal(amount)
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.HdfcCcmsRechargeUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            CcmsRechargeHdfcRes searchList = obj.ToObject<CcmsRechargeHdfcRes>();
            return searchList;
        }

        public async Task<GetRequestAuthorizationRes> GetRequestAuthorizationDetails(GetRequestAuthorizationReq entity)
        {
            var searchBody = new GetRequestAuthorizationReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = entity.CustomerId,
                FromDate = entity.FromDate,
                ToDate = entity.ToDate
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.AMEXGetRequestAuthorizationDetailsUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetRequestAuthorizationRes searchList = obj.ToObject<GetRequestAuthorizationRes>();
            return searchList;
        }

        public async Task<List<SuccessResponse>> AuthorizationAction(string authReq)
        {
            ObjBankAuthEntryDetail[] arrs = JsonConvert.DeserializeObject<ObjBankAuthEntryDetail[]>(authReq);

            var searchBody = new AuthorizationActionReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ObjBankAuthEntryDetail = arrs
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.AMEXRequestAuthorizationActionUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }
    }
}
