using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.CustomerFeeWaiver;
using HPCL.Common.Models.ViewModel.CustomerFeeWaiver;
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
    public class CustomerFeeWaiverServices : ICustomerFeeWaiverServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public CustomerFeeWaiverServices(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor=httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<List<PendingCustResponse>> FeeWaiver(PendingCustomer entity)
        {
            var custDetails = new PendingCustomer
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FormNumber = entity.FormNumber
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(custDetails), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.bindPendingCustomerUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<PendingCustResponse> pendingList = jarr.ToObject<List<PendingCustResponse>>();
            return pendingList;
        }


        public async Task<Tuple<List<GetApproveFeeWaiverBasicDetail>, List<GetApproveFeeWaiverCardDetail>>> BindPendingCustomer(string customerReferenceNo)
        {
            var forms = new Dictionary<string, string>
            {
                {"useragent", CommonBase.useragent},
                {"userip", CommonBase.userip},
                {"userid", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
                {"CustomerReferenceNo", customerReferenceNo}
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getFeeWaiverDetailsUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JObject>();

            var basicJson = jarr["GetApproveFeeWaiverBasicDetail"].Value<JArray>();
            var cardJson = jarr["GetApproveFeeWaiverCardDetail"].Value<JArray>();

            List<GetApproveFeeWaiverBasicDetail> basicDetails = basicJson.ToObject<List<GetApproveFeeWaiverBasicDetail>>();
            List<GetApproveFeeWaiverCardDetail> cardDetails = cardJson.ToObject<List<GetApproveFeeWaiverCardDetail>>();
            return Tuple.Create(basicDetails, cardDetails);
        }

        public async Task<string> ApproveCustomer(string CustomerReferenceNo, string comments)
        {
            var forms = new ApproveRejectWaiver
            {
                UserAgent = CommonBase.useragent,
                UserIp= CommonBase.userip,
                UserId= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerReferenceNo = CustomerReferenceNo,
                Approvalstatus = 4,
                Comments = comments,
                ApprovedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.approveRejectFeeWaiverUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();

            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            return responseMsg[0].Reason;
        }

        public async Task<string> RejectCustomer(string CustomerReferenceNo, string comments)
        {
            var forms = new ApproveRejectWaiver
            {
                UserAgent = CommonBase.useragent,
                UserIp= CommonBase.userip,
                UserId= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerReferenceNo = CustomerReferenceNo,
                Approvalstatus = 13,
                Comments = comments,
                ApprovedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.approveRejectFeeWaiverUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();

            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            return responseMsg[0].Reason;
        }
    }
}
