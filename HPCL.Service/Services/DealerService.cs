using HPCL.Common.Models.ViewModel.Dealer;
using HPCL.Common.Models.RequestModel.Dealer;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Helper;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;

namespace HPCL.Service.Services
{
    public class DealerService : IDealerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        private readonly IConfiguration _configuration;
        public DealerService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
            _configuration = configuration;
        }
        public async Task<DealerCreditMappingViewModel> GetDealerCreditMappingDetails(string CustomerID)
        {
            DealerCreditMappingViewModel dealerCreditMappingViewModel = new DealerCreditMappingViewModel();
            var searchBody = new DealerRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerID = CustomerID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getdealercreditmappingdetails);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            dealerCreditMappingViewModel = obj.ToObject<DealerCreditMappingViewModel>();
            return dealerCreditMappingViewModel;
        }

        public async Task<List<string>> SaveDealerForCreditSale([FromBody] DealerForCreditSaleViewModel dealerForCreditSaleViewModel)
        {
            if (dealerForCreditSaleViewModel != null)
            {
                if (dealerForCreditSaleViewModel.TypeMapDealerForCreditSale.Count() > 0)
                {
                    foreach (var item in dealerForCreditSaleViewModel.TypeMapDealerForCreditSale)
                    {
                        if (!string.IsNullOrEmpty(item.EffectiveDate))
                        {
                            item.EffectiveDate = await _commonActionService.changeDateFormat(item.EffectiveDate);
                        }
                    }
                }
            }
            var RequestForm = new DealerForCreditSaleViewModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerID = dealerForCreditSaleViewModel.CustomerID,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                TypeMapDealerForCreditSale = dealerForCreditSaleViewModel.TypeMapDealerForCreditSale
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(RequestForm), Encoding.UTF8, "application/json");
            var RequestResponse = await _requestService.CommonRequestService(requestContent, WebApiUrl.insertmapdealerforcreditsale);
            JObject RequestResponseObj = JObject.Parse(JsonConvert.DeserializeObject(RequestResponse).ToString());
            var RequestResponseJarr = RequestResponseObj["Data"].Value<JArray>();
            List<UpdateMerchantSuccessResponse> RequestResponseList = RequestResponseJarr.ToObject<List<UpdateMerchantSuccessResponse>>();
            List<string> messageList = new List<string>();
            if (RequestResponseList.Count > 0)
            {
                messageList.Add(RequestResponseList[0].Status.ToString());
                foreach (var item in RequestResponseList)
                    messageList.Add("Merchant ID :"+item.MerchantID +" "+ item.Reason);
            }
            return messageList;

        }
        public async Task<List<SuccessResponse>> MerchantMapEnableorDisable(string customerID,string merchantID,string action)
        { 
            var RequestForm = new DealerRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerID = customerID,
                MerchantID=merchantID,
                Action=action
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(RequestForm), Encoding.UTF8, "application/json");
            var RequestResponse = await _requestService.CommonRequestService(requestContent, WebApiUrl.customermerchantmappingenabledisable);
            JObject RequestResponseObj = JObject.Parse(JsonConvert.DeserializeObject(RequestResponse).ToString());
            //var RequestResponseJarr = RequestResponseObj["Data"].Value<JArray>();
            //List<SuccessResponse> RequestResponseList = RequestResponseJarr.ToObject<List<SuccessResponse>>();

            //return RequestResponseList;
            List<SuccessResponse> RequestResponseList = new List<SuccessResponse>();
            if (RequestResponseObj["Status_Code"].ToString() == "200")
            {
                var RequestResponseJarr = RequestResponseObj["Data"].Value<JArray>();
                RequestResponseList = RequestResponseJarr.ToObject<List<SuccessResponse>>();
            }
            else
            {
                SuccessResponse successResponse = new SuccessResponse();
                successResponse.Status = 0;
                successResponse.Reason = RequestResponseObj["Message"].ToString();
                RequestResponseList.Add(successResponse);
            }
            return RequestResponseList;

        }
        public async Task<List<UpdateMerchantSuccessResponse>> UpdateDealerCreditmapping([FromBody] DealerRequestModel DealerRequestModel)
        {
  
            var RequestForm = new DealerRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerID = DealerRequestModel.CustomerID,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                TypeUpdateDealerCreditMapping = DealerRequestModel.TypeUpdateDealerCreditMapping
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(RequestForm), Encoding.UTF8, "application/json");
            var RequestResponse = await _requestService.CommonRequestService(requestContent, WebApiUrl.updatedealercreditmapping);
            JObject RequestResponseObj = JObject.Parse(JsonConvert.DeserializeObject(RequestResponse).ToString());
           
            List<UpdateMerchantSuccessResponse> RequestResponseList = new List<UpdateMerchantSuccessResponse>();
            if (RequestResponseObj["Status_Code"].ToString() == "200")
            {
                var RequestResponseJarr = RequestResponseObj["Data"].Value<JArray>();
                RequestResponseList = RequestResponseJarr.ToObject<List<UpdateMerchantSuccessResponse>>();
                
            }
            else
            {
                UpdateMerchantSuccessResponse successResponse = new UpdateMerchantSuccessResponse();
                successResponse.Status = 0;
                successResponse.Reason = RequestResponseObj["Message"].ToString();
                RequestResponseList.Add(successResponse);
            }
            return RequestResponseList;
        }
        public async Task<DealerCreditSaleStatement> GetCreditSaleStatement(string CustomerID, string MerchantID, string FromDate, string ToDate)
        {
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(FromDate))
            {
                FromDate = await _commonActionService.changeDateFormat(FromDate);
                ToDate = await _commonActionService.changeDateFormat(ToDate);
            }
            else
            {
                FromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var searchBody = new DealerRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FromDate = FromDate,
                ToDate = ToDate,
                CustomerID = CustomerID,
                MerchantID = MerchantID,

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getdealercreditsalestatement);

            DealerCreditSaleStatement searchList = JsonConvert.DeserializeObject<DealerCreditSaleStatement>(response);
            return searchList;
        }
        public async Task<List<UpdateMerchantSuccessResponse>> UpdateDealerCreditPaymentBulk([FromBody] DealerRequestModel DealerRequestModel)
        {

            var RequestForm = new DealerRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerID = DealerRequestModel.CustomerID,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                TypeUpdateDealerCreditPaymentInBulk = DealerRequestModel.TypeUpdateDealerCreditPaymentInBulk
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(RequestForm), Encoding.UTF8, "application/json");
            var RequestResponse = await _requestService.CommonRequestService(requestContent, WebApiUrl.updatedealercreditpaymentinbulk);
            JObject RequestResponseObj = JObject.Parse(JsonConvert.DeserializeObject(RequestResponse).ToString());

            List<UpdateMerchantSuccessResponse> RequestResponseList = new List<UpdateMerchantSuccessResponse>();
            if (RequestResponseObj["Status_Code"].ToString() == "200")
            {
                var RequestResponseJarr = RequestResponseObj["Data"].Value<JArray>();
                RequestResponseList = RequestResponseJarr.ToObject<List<UpdateMerchantSuccessResponse>>();

            }
            else
            {
                UpdateMerchantSuccessResponse successResponse = new UpdateMerchantSuccessResponse();
                successResponse.Status = 0;
                successResponse.Reason = RequestResponseObj["Message"].ToString();
                RequestResponseList.Add(successResponse);
            }
            return RequestResponseList;
        }
        public async Task<DealerCreditPaymentinBulk> GetDealerCreditPaymentBulk(string CustomerId)
        {
            DealerCreditPaymentinBulk dealerCreditPaymentinBulk = new DealerCreditPaymentinBulk();
            var searchBody = new DealerRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerID= CustomerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getdealercreditpaymentinbulk);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            dealerCreditPaymentinBulk = obj.ToObject<DealerCreditPaymentinBulk>();
            return dealerCreditPaymentinBulk;
        }
        public async Task<DealerCreditSaleViewModel> GetDealerCreditSaleView(string CustomerID,string MerchantID,string FromDate,string ToDate)
        {
            DealerCreditSaleViewModel dealerCreditSaleViewModel = new DealerCreditSaleViewModel();
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(FromDate))
            {
                FromDate = await _commonActionService.changeDateFormat(FromDate);
                ToDate = await _commonActionService.changeDateFormat(ToDate);
            }
            else
            {
                FromDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var searchBody = new DealerRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerID= CustomerID,
                MerchantID = MerchantID==null?"":MerchantID,
                FromDate = FromDate,
                ToDate = ToDate,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getdealercreditsaleview);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            dealerCreditSaleViewModel = obj.ToObject<DealerCreditSaleViewModel>();
            return dealerCreditSaleViewModel;
        }
        public async Task<DealerCreditSaleViewModel> GetMerchantDealerCreditSaleView(string CustomerID, string MerchantID, string FromDate, string ToDate)
        {
            DealerCreditSaleViewModel dealerCreditSaleViewModel = new DealerCreditSaleViewModel();
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(FromDate))
            {
                FromDate = await _commonActionService.changeDateFormat(FromDate);
                ToDate = await _commonActionService.changeDateFormat(ToDate);
            }
            else
            {
                FromDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var searchBody = new DealerRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerID = CustomerID == null ? "" : CustomerID,
                MerchantID = MerchantID ,
                FromDate = FromDate,
                ToDate = ToDate,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getmerchantcreditsaleview);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            dealerCreditSaleViewModel = obj.ToObject<DealerCreditSaleViewModel>();
            return dealerCreditSaleViewModel;
        }
        public async Task<List<StatementDateModel>> GetMerchantSaleStatementDate(string CustomerID, string MerchantID)
        {
            var searchBody = new DealerRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
               CustomerID=CustomerID,
               MerchantID=MerchantID,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getstatementdatelist);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
          
            var responseJarr = obj["Data"].Value<JArray>();
            List<StatementDateModel> statementDateList = responseJarr.ToObject<List<StatementDateModel>>();
            return statementDateList;
        }
        public async Task<MerchanDealerSaleStatementModel> GetMerchantDealerCreditSaleStatement(string CustomerID, string MerchantID, string SearchDate)
        {
            MerchanDealerSaleStatementModel merchanDealerSaleStatementModel = new MerchanDealerSaleStatementModel();
           
            var searchBody = new DealerRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerID = CustomerID == null ? "" : CustomerID,
                MerchantID = MerchantID,
                FromDate = SearchDate,
                
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getmerchantdealercreditsalestatement);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            merchanDealerSaleStatementModel = obj.ToObject<MerchanDealerSaleStatementModel>();
            return merchanDealerSaleStatementModel;
        }
        public async Task<MerchantCreditSaleOutstandingViewModel> GetCreditSaleOutstandingDetails( string MerchantID)
        {
            MerchantCreditSaleOutstandingViewModel merchantCreditSaleOutstandingViewModel = new MerchantCreditSaleOutstandingViewModel();

            var searchBody = new DealerRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                MerchantID = MerchantID,

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getcreditsaleoutstandingdetails);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            merchantCreditSaleOutstandingViewModel = obj.ToObject<MerchantCreditSaleOutstandingViewModel>();
            return merchantCreditSaleOutstandingViewModel;
        }
    }

}

