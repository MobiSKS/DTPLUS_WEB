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
        public async Task<List<SuccessResponse>> UpdateDealerCreditmapping([FromBody] DealerRequestModel DealerRequestModel)
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
        
    }

}

