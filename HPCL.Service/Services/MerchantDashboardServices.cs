using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.MerchantDashboard;
using HPCL.Common.Models.ResponseModel.MerchantDashboard;
using HPCL.Common.Models.ResponseModel.CommonResponse;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;




namespace HPCL.Service.Services
{
    public class MerchantDashboardServices : IMerchantDashboardServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        public MerchantDashboardServices(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }
        public async Task<List<KeyInformationResponseModel>> keyInformation(string MerchantID)
        {
            var merchantDashboardKeyInformation = new MerchantDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                MerchantId = "3010000009"

            };


            StringContent merchantDashboardKeyInformationTableContent = new StringContent(JsonConvert.SerializeObject(merchantDashboardKeyInformation), Encoding.UTF8, "application/json");

            var merchantDashboardKeyInformationResponse = await _requestService.CommonRequestService(merchantDashboardKeyInformationTableContent, WebApiUrl.getkeyinformation);

            JObject merchantDashboardKeyInformationTableObj = JObject.Parse(JsonConvert.DeserializeObject(merchantDashboardKeyInformationResponse).ToString());
            var merchantDashboardKeyInformationTableJarr = merchantDashboardKeyInformationTableObj["Data"].Value<JArray>();
            List<KeyInformationResponseModel> KeyInformationResponseModel = merchantDashboardKeyInformationTableJarr.ToObject<List<KeyInformationResponseModel>>();

            return KeyInformationResponseModel;


        }

        public async Task<List<TodaysTransactionSummaryResponseModel>> TodaysTransactionSummaries(string MerchantID)
        {
            var todaysTransactionSummariesDetails = new MerchantDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),

                MerchantId = "3010000009"
                //CardSale="",
                //CCMSSale="",
                //CashReload="",
                //CCMSRecharge="",
                //Status="",
                //Reason=""
            };


            StringContent merchantDashboardtodayInstructionTableContent = new StringContent(JsonConvert.SerializeObject(todaysTransactionSummariesDetails), Encoding.UTF8, "application/json");

            var merchantDashboardtodaysTransaction = await _requestService.CommonRequestService(merchantDashboardtodayInstructionTableContent, WebApiUrl.todaysTransactions);

            JObject merchantDashboardtodaysTransactionTableObj = JObject.Parse(JsonConvert.DeserializeObject(merchantDashboardtodaysTransaction).ToString());
            var merchantDashboardtodaysTransactionTableJarr = merchantDashboardtodaysTransactionTableObj["Data"].Value<JArray>();
            List<TodaysTransactionSummaryResponseModel> todaysTransactionResponseModel = merchantDashboardtodaysTransactionTableJarr.ToObject<List<TodaysTransactionSummaryResponseModel>>();

            return todaysTransactionResponseModel;


        }



        public async Task<List<LastTrasactionResponseModel>> lastTrasaction(string MerchantID)
        {
            var lastTransactionDetails = new MerchantDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),

                MerchantId = "3010000008"
                //CardSale="",
                //CCMSSale="",
                //CashReload="",
                //CCMSRecharge="",
                //Status="",
                //Reason=""
            };


            StringContent merchantDashboardlastTrasactionTableContent = new StringContent(JsonConvert.SerializeObject(lastTransactionDetails), Encoding.UTF8, "application/json");

            var merchantDashboardlastTransaction = await _requestService.CommonRequestService(merchantDashboardlastTrasactionTableContent, WebApiUrl.lastTrasactions);

            JObject merchantDashboardlastTransactionTableObj = JObject.Parse(JsonConvert.DeserializeObject(merchantDashboardlastTransaction).ToString());
            var merchantDashboardlastTransactionTableJarr = merchantDashboardlastTransactionTableObj["Data"].Value<JArray>();
            List<LastTrasactionResponseModel> lastTransactionResponseModel = merchantDashboardlastTransactionTableJarr.ToObject<List<LastTrasactionResponseModel>>();

            return lastTransactionResponseModel;


        }


        public async Task<List<LastBatchDetailResponseModel>> lastBatches(string MerchantID)
        {

            var LastBatchDetails = new MerchantDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),

                MerchantId = "3010000008"
                //CardSale="",
                //CCMSSale="",
                //CashReload="",
                //CCMSRecharge="",
                //Status="",
                //Reason=""
            };


            StringContent merchantDashboardlastBatchTableContent = new StringContent(JsonConvert.SerializeObject(LastBatchDetails), Encoding.UTF8, "application/json");

            var merchantDashboardlastBatch = await _requestService.CommonRequestService(merchantDashboardlastBatchTableContent, WebApiUrl.lastBatch);

            JObject merchantDashboardlastBatchTableObj = JObject.Parse(JsonConvert.DeserializeObject(merchantDashboardlastBatch).ToString());
            var merchantDashboardlastBatchTableJarr = merchantDashboardlastBatchTableObj["Data"].Value<JArray>();
            List<LastBatchDetailResponseModel> lastBatchResponseModel = merchantDashboardlastBatchTableJarr.ToObject<List<LastBatchDetailResponseModel>>();

            return lastBatchResponseModel;


        }


        public async Task<List<LastSaleReloadEarningDetailsResponseModel>> lastSaleReloadEarningDetails(string MerchantID)
        {
            var LastSaleReloadEarningDetails = new MerchantDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),

                MerchantId = "3010000011"
                //CardSale="",
                //CCMSSale="",
                //CashReload="",
                //CCMSRecharge="",
                //Status="",
                //Reason=""
            };


            StringContent merchantDashboardlastSaleReloadEarningTableContent = new StringContent(JsonConvert.SerializeObject(LastSaleReloadEarningDetails), Encoding.UTF8, "application/json");

            var merchantDashboardlastSaleReload = await _requestService.CommonRequestService(merchantDashboardlastSaleReloadEarningTableContent, WebApiUrl.lastSaleEarning);

            JObject merchantDashboardlastSalesReloadTableObj = JObject.Parse(JsonConvert.DeserializeObject(merchantDashboardlastSaleReload).ToString());
            var merchantDashboardlastSalesReloadTableJarr = merchantDashboardlastSalesReloadTableObj["Data"].Value<JArray>();
            List<LastSaleReloadEarningDetailsResponseModel> lastSaleReloadEarningResponseModel = merchantDashboardlastSalesReloadTableJarr.ToObject<List<LastSaleReloadEarningDetailsResponseModel>>();

            return lastSaleReloadEarningResponseModel;


        }

        //public async Task<List<KeyEventAndFigureResponseModel>> lastKeyEventAndFigure(string MerchantID)
        //{
        //    var merchantDashboardlastKeyEventAndFigure = new MerchantDashboardRequestModel()
        //    {
        //        UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
        //        UserAgent = CommonBase.useragent,
        //        UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),

        //        MerchantId = "3010000008"
        //        //CardSale="",
        //        //CCMSSale="",
        //        //CashReload="",
        //        //CCMSRecharge="",
        //        //Status="",
        //        //Reason=""
        //    };


        //    StringContent merchantDashboardlastKeyEventTableContent = new StringContent(JsonConvert.SerializeObject(merchantDashboardlastKeyEventAndFigure), Encoding.UTF8, "application/json");

        //    var merchantDashboardlastKeyEvent = await _requestService.CommonRequestService(merchantDashboardlastKeyEventTableContent, WebApiUrl.lastKeyEvent);

        //    JObject merchantDashboardlastKeyEventTableObj = JObject.Parse(JsonConvert.DeserializeObject(merchantDashboardlastKeyEvent).ToString());
        //    var merchantDashboardlastkeyEventsJarr = merchantDashboardlastKeyEventTableObj["Data"].Value<JArray>();
        //    List<KeyEventAndFigureResponseModel> lastKeyEventsResponseModel = merchantDashboardlastkeyEventsJarr.ToObject<List<KeyEventAndFigureResponseModel>>();

        //    return lastKeyEventsResponseModel;


        //}



        public async Task<List<KeyEventAndFigureResponseModel>> lastKeyEventAndFigure(string MerchantID)
        {

            var merchantDashboardlastKeyEventAndFigure = new MerchantDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                MerchantId = "3010000021"

            };


            StringContent merchantDashboardlastKeyEventTableContent = new StringContent(JsonConvert.SerializeObject(merchantDashboardlastKeyEventAndFigure), Encoding.UTF8, "application/json");

            var merchantDashboardlastKeyEventResponse = await _requestService.CommonRequestService(merchantDashboardlastKeyEventTableContent, WebApiUrl.lastKeyEvent);

            if (String.IsNullOrEmpty(merchantDashboardlastKeyEventResponse))
            {
                List<KeyEventAndFigureResponseModel> keyEventAndFigureResponseModels = new List<KeyEventAndFigureResponseModel>();
                return keyEventAndFigureResponseModels;
            }
            else
            {
                JObject merchantDashboardKeyEventTableObj = JObject.Parse(JsonConvert.DeserializeObject(merchantDashboardlastKeyEventResponse).ToString());
                var merchantDashboardKeyEventsTableJarr = merchantDashboardKeyEventTableObj["Data"].Value<JArray>();
                List<KeyEventAndFigureResponseModel> KeyEventsResponseModel = merchantDashboardKeyEventsTableJarr.ToObject<List<KeyEventAndFigureResponseModel>>();

                return KeyEventsResponseModel;
            }



        }


        public async Task<List<NotificationResponse>> NotificationResponsesDetails(string UserTypes)
        {
            var NotificationResponseDetails = new NotificationContent
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserType = "Merchant"
            };


            StringContent merchantDashboardNotificationTableContent = new StringContent(JsonConvert.SerializeObject(NotificationResponseDetails), Encoding.UTF8, "application/json");

            var merchantDashboardNotification = await _requestService.CommonRequestService(merchantDashboardNotificationTableContent, WebApiUrl.notificationContent);

            JObject merchantDashboardNotificationTableObj = JObject.Parse(JsonConvert.DeserializeObject(merchantDashboardNotification).ToString());
            var merchantDashboardNotificationTableJarr = merchantDashboardNotificationTableObj["Data"].Value<JArray>();
            List<NotificationResponse> NotificationResponseModel = merchantDashboardNotificationTableJarr.ToObject<List<NotificationResponse>>();

            return NotificationResponseModel;


        }







    }
}