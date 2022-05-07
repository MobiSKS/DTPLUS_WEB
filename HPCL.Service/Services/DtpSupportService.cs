using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.DtpSupport;
using HPCL.Common.Models.ViewModel.DtpSupport;
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
    public class DtpSupportService : IDtpSupportService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public DtpSupportService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<GetBlockUnblockCustomerCcmsAccountByCustomeridResponse> GetBlockUnblockCustomerCcmsAccount(string customerId)
        {
            var reqBody = new GetBlockUnblockCustomerCcmsAccountByCustomerid
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerId = customerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetBlockUnblockCustomerCcmsAccountByCustomeridUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetBlockUnblockCustomerCcmsAccountByCustomeridResponse res = obj.ToObject<GetBlockUnblockCustomerCcmsAccountByCustomeridResponse>();
            return res;
        }

        public async Task<string> UpdateCustomerCcmsAccountStatus(BlockUnblockCustomerCcmsAccount entity)
        {
            var reqBody = new BlockUnblockCustomerCcmsAccount
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = entity.CustomerID,
                CCMSBalanceStatus = entity.CCMSBalanceStatus,
                CCMSBalanceRemarks = entity.CCMSBalanceRemarks,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.BlockUnblockCustomerCcmsAccountUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res[0].Reason;
        }
        public async Task<string> GetCardBalanceTransferDetails(string CardNo)
        {
            var reqBody = new CardBalanceTransferViewModel
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CardNo = CardNo
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCardBalanceTransfer);

           
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res[0].Reason;
        }
        public async Task<UnblockUserModel> UnblockUser()
        {
            UnblockUserModel model = new UnblockUserModel();
            model.Message = "";
            return model;
        }

    }
}
