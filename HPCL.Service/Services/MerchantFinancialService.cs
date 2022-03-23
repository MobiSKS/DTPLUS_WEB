using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.MerchantFinancials;
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
            batchDetailsResponse = JsonConvert.DeserializeObject<BatchDetailsResponse>(response);
            var merchantObj = obj["Data"].Value<JArray>();
            List<BatchDetailsResponseData> searchList = merchantObj.ToObject<List<BatchDetailsResponseData>>();
            batchDetailsResponse.BatchDetailsResponseData.AddRange(searchList);
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
                    TransactionType = (entity.TransType != null) ? string.Join(",", entity.TransType) : "",
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
                    TransactionType = (entity.TransType != null) ? string.Join(",", entity.TransType) : "",
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
        public async Task<MerchantDeltaReportModel> GetMerchantDeltaReport(string MerchantId, string TerminalId, string FromDate, string ToDate)
        {
            MerchantDeltaReportModel MerchantDeltaReport = new MerchantDeltaReportModel();
            string fromDate = "", toDate = "";
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(FromDate))
            {
                string[] fromDateArr = FromDate.Split("-");
                string[] toDateArr = ToDate.Split("-");

                fromDate = fromDateArr[2] + "-" + fromDateArr[1] + "-" + fromDateArr[0];
                toDate = toDateArr[2] + "-" + toDateArr[1] + "-" + toDateArr[0];

            }
            var reqBody = new GetDeltaReport
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                TerminalId = TerminalId,
                MerchantId = MerchantId,
                FromDate=fromDate,
                ToDate=toDate
            };
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getmerchantsalereloaddeltadetail);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response,settings).ToString());
            MerchantDeltaReport = JsonConvert.DeserializeObject<MerchantDeltaReportModel>(response,settings);
            var merchantObj = obj["Data"].Value<JArray>();
            List<MerchantDeltaReportDetails> searchList = merchantObj.ToObject<List<MerchantDeltaReportDetails>>();
            MerchantDeltaReport.MerchantDeltaReportDetails.AddRange(searchList);
            return MerchantDeltaReport;
        }

        public async Task<MerchantERPReloadSaleEarningModel> ERPReloadSaleEarningDetails(MerchantERPReloadSaleEarningModel Model)
        {
            string fromDate = "", toDate = "";

            Model.SaleEarningDetails = new List<MerchantERPReloadSaleEarningDetails>();
            Model.Message = "";

            if (!string.IsNullOrEmpty(Model.FromDate) && !string.IsNullOrEmpty(Model.ToDate))
            {
                string[] fromDateArr = Model.FromDate.Split("-");
                string[] toDateArr = Model.ToDate.Split("-");

                fromDate = fromDateArr[2] + "-" + fromDateArr[1] + "-" + fromDateArr[0];
                toDate = toDateArr[2] + "-" + toDateArr[1] + "-" + toDateArr[0];
            }
            else
            {
                if (string.IsNullOrEmpty(Model.TerminalOrMerchant))
                    Model.TerminalOrMerchant = "Merchant";
                return Model;
            }
            var requestData = new MerchantERPReloadSaleEarningRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FromDate = fromDate,
                ToDate = toDate,
                MerchantId = Model.TerminalOrMerchant == "Merchant" ? Model.MerchantId : "",
                TerminalId = Model.TerminalOrMerchant == "Terminal" ? Model.MerchantId : ""
            };
            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(requestContent, WebApiUrl.merchantErpReloadSaleEarningDetail);

            JObject Obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            Model.Message = Obj["Message"].ToString();
            var Jarr = Obj["Data"].Value<JArray>();
            List<MerchantERPReloadSaleEarningDetails> list = Jarr.ToObject<List<MerchantERPReloadSaleEarningDetails>>();
            Model.SaleEarningDetails.AddRange(list);
            return Model;
        }

    }
}
