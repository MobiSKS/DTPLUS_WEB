using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.CustomerFinancial;
using HPCL.Common.Models.ResponseModel.MerchantFinancial;
using HPCL.Common.Models.ViewModel.MerchantFinancials;
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
    public class MerchantFinancialService : IMerchantFinancialService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public MerchantFinancialService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<UploadMerchantCautionLimitResponse> ViewUploadMerchantCautionLimit(GetUploadMerchantCautionLimit entity)
        {
            var reqBody = new GetUploadMerchantCautionLimit();

            if (entity.FromDate != null && entity.ToDate != null)
            {
                reqBody = new GetUploadMerchantCautionLimit
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    MerchantId = entity.MerchantId,
                    MerchantStatus = entity.MerchantStatus,
                    FromDate = entity.FromDate,
                    ToDate = entity.ToDate,
                    RegionalOffice = entity.RegionalOffice,
                    SalesArea = entity.SalesArea
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Merchant")
            {
                reqBody = new GetUploadMerchantCautionLimit
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    MerchantId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    MerchantStatus = entity.MerchantStatus,
                    FromDate = entity.FromDate,
                    ToDate = entity.ToDate,
                    RegionalOffice = entity.RegionalOffice,
                    SalesArea = entity.SalesArea
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetUploadMerchantCautionLimitUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            UploadMerchantCautionLimitResponse searchList = obj.ToObject<UploadMerchantCautionLimitResponse>();
            return searchList;
        }

        public async Task<MerchantSettlementDetailsResponse> SettlementDetails(GetMerchantSettlementDetails entity)
        {
            var reqBody = new GetMerchantSettlementDetails();

            if (entity.FromDate != null && entity.ToDate != null)
            {
                reqBody = new GetMerchantSettlementDetails
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    MerchantId = entity.MerchantId,
                    TerminalId = entity.TerminalId,
                    FromDate = entity.FromDate,
                    ToDate = entity.ToDate
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Merchant")
            {
                reqBody = new GetMerchantSettlementDetails
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    MerchantId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    TerminalId = entity.TerminalId,
                    FromDate = entity.FromDate,
                    ToDate = entity.ToDate
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetSattlementDetailsUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            MerchantSettlementDetailsResponse searchList = obj.ToObject<MerchantSettlementDetailsResponse>();
            return searchList;
        }

        public async Task<BatchDetailsResponse> GetBatchDetails(string terminalId, int batchId)
        {
            BatchDetailsResponse batchDetailsResponse = new BatchDetailsResponse();

            var reqBody = new GetBatchDetails
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                TerminalId = terminalId,
                BatchId = batchId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetBatchDetailsUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var merchantObj = obj["Data"].Value<JArray>();
            List<BatchDetailsResponseData> searchList = merchantObj.ToObject<List<BatchDetailsResponseData>>();
            batchDetailsResponse.data.AddRange(searchList);
            return batchDetailsResponse;
        }

        public async Task<GetTerminalDetailsResponse> GetTerminalDetails(string terminalId)
        {
            GetTerminalDetailsResponse terminalDetailsResponse = new GetTerminalDetailsResponse();

            var reqBody = new GetTerminalDetails
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                TerminalId = terminalId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetTerminalDetailsUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var terminalObj = obj["Data"].Value<JObject>();
            var ObjTerminalDetail = terminalObj["ObjTerminalDetail"].Value<JArray>();
            var ObjTerminalDeploymentDetail = terminalObj["ObjTerminalDeploymentDetail"].Value<JArray>();
            List<ObjTerminalDetail> terminalDetails = ObjTerminalDetail.ToObject<List<ObjTerminalDetail>>();
            List<ObjTerminalDeploymentDetail> terminalDeploymentDetails = ObjTerminalDeploymentDetail.ToObject<List<ObjTerminalDeploymentDetail>>();
            terminalDetailsResponse.objTerminalDetails.AddRange(terminalDetails);
            terminalDetailsResponse.objTerminalDeploymentDetail.AddRange(terminalDeploymentDetails);
            return terminalDetailsResponse;
        }

        public async Task<TransactionlDetailsResponse> GetTransactionlDetails(GetTransactionDetails entity)
        {
            var reqBody = new GetTransactionDetails();

            if (entity.FromDate != null && entity.ToDate != null)
            {
                reqBody = new GetTransactionDetails
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    MerchantId = entity.MerchantId,
                    TerminalId = entity.TerminalId,
                    TransactionType = string.Join(",", entity.TransType),
                    FromDate = entity.FromDate,
                    ToDate = entity.ToDate
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Merchant")
            {
                reqBody = new GetTransactionDetails
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    MerchantId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    TerminalId = entity.TerminalId,
                    TransactionType = string.Join(",", entity.TransType),
                    FromDate = entity.FromDate,
                    ToDate = entity.ToDate
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetTransactionDetailsUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            TransactionlDetailsResponse searchList = obj.ToObject<TransactionlDetailsResponse>();
            return searchList;
        }
    }
}
