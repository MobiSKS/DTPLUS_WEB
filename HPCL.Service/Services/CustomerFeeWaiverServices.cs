using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.CustomerFeeWaiver;
using HPCL.Common.Models.ViewModel.CustomerFeeWaiver;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Numerics;
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

        public async Task<PendingCustomer> FeeWaiver(PendingCustomer entity)
        {
            var custDetails = new PendingCustomer
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FormNumber = string.IsNullOrEmpty(entity.FormNumber) ? "" : entity.FormNumber
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(custDetails), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getCustomerPendingForFeeApproval);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            PendingCustResponse pendingList = obj.ToObject<PendingCustResponse>();

            if (pendingList != null && pendingList.data != null && pendingList.data.Count > 0)
            {
                entity.data = pendingList.data;
            }
            else
            {
                entity.data = new List<PendingCustResponseBody>();
                entity.Message = obj["Message"].ToString();
            }

            return entity;
        }

        public async Task<BindPendingCustomerRes> BindPendingCustomer(string formNumber)
        {
            var forms = new BindApprovePendingCustomer
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                formNumber = BigInteger.Parse(formNumber)
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getFeeWaiverDetailsUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            BindPendingCustomerRes searchList = obj.ToObject<BindPendingCustomerRes>();

            return searchList;
        }

        public async Task<List<SuccessResponse>> ApproveCustomer(string formNumber, string comments)
        {
            var forms = new ApproveRejectWaiver
            {
                UserAgent = CommonBase.useragent,
                UserIp= CommonBase.userip,
                UserId= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FormNumber = formNumber,
                Approvalstatus = 4,
                Comments = comments,
                ApprovedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.approveRejectFeeWaiverUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();

            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            return responseMsg;
        }

        public async Task<List<SuccessResponse>> RejectCustomer(string formNumber, string comments)
        {
            var forms = new ApproveRejectWaiver
            {
                UserAgent = CommonBase.useragent,
                UserIp= CommonBase.userip,
                UserId= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FormNumber = formNumber,
                Approvalstatus = 13,
                Comments = comments,
                ApprovedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.approveRejectFeeWaiverUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();

            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            return responseMsg;
        }

        public void ViewCustomer(string formNumber)
        {
            _httpContextAccessor.HttpContext.Session.SetString("formNumber", formNumber);
        }

        public async Task<ViewCustomerDetailsResponse> ViewCustomerDetails(string formNumber)
        {
            var customerBody = new BindPendingCustomer
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FormNumber = BigInteger.Parse(formNumber)
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(customerBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getPendingCustUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            ViewCustomerDetailsResponse UploadDocList = obj.ToObject<ViewCustomerDetailsResponse>();

            return UploadDocList;
        }
    }
}
