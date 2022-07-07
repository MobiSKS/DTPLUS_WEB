using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.HDFCBankCreditPouch;
using HPCL.Common.Models.ViewModel.HDFCBankCreditPouch;
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
    public class HDFCBankCreditPouchService : IHDFCBankCreditPouchService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;

        public HDFCBankCreditPouchService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
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
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCustomerDetailsUrl);
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
            var response = await _requestService.CommonRequestService(content, WebApiUrl.EnrollExceptionReqUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }

        public async Task<SearchRequestApprovalRes> SearchRequestApproval(SearchRequestApproval entity)
        {
            var searchBody = new SearchRequestApproval
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = entity.CustomerId,
                ZO = entity.ZO ?? "0",
                RO = entity.RO ?? "0",
                Status = entity.Status
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetExApproval);
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
            var response = await _requestService.CommonRequestService(content, WebApiUrl.SubmitExApprovalUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }

        public async Task<SearchEnrollStatusRes> GetEnrollStatus(SearchEnrollStatus entity)
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

            var searchBody = new SearchEnrollStatus
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = entity.CustomerId,
                ZO = entity.ZO ?? "0",
                RO = entity.RO ?? "0",
                FromDate = fromDate,
                ToDate = toDate
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetEnrollStatusUrl);
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
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetEnrollStatusReportUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetEnrollStatusReportRes searchList = obj.ToObject<GetEnrollStatusReportRes>();
            return searchList;
        }

        public async Task<HdfcInitateRecharge> CCMSInitiateRechargeHDFC(string customerId, string amount)
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

            HdfcInitateRecharge checkList = obj.ToObject<HdfcInitateRecharge>();
            return checkList;
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

            var searchBody = new GetRequestAuthorizationReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = entity.CustomerId,
                FromDate = fromDate,
                ToDate = toDate
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetRequestAuthorizationDetailsUrl);
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
            var response = await _requestService.CommonRequestService(content, WebApiUrl.RequestAuthorizationActionUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }

        public async Task<CheckEligibleRes> CheckEligibility(CheckEligibleReq entity)
        {
            var searchBody = new CheckEligibleReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = entity.CustomerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.RequestToAvailCheckUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            CheckEligibleRes reasonList = obj.ToObject<CheckEligibleRes>();
            return reasonList;
        }

        public async Task<List<SuccessResponse>> ReqAvailEnroll(string customerId, string planTypeId)
        {
            var searchBody = new ReqAvailEnrollReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = customerId,
                FuleConsumptionCapacity = 0,
                PlanTypeId = Convert.ToInt32(planTypeId),
                ReferenceNo = null,
                CustomerRemarks = "Request By Customer",
                ActionType = 1,
                RequestedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.RequestToAvailEnrollUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> reasonList = jarr.ToObject<List<SuccessResponse>>();
            return reasonList;
        }
    }
}
