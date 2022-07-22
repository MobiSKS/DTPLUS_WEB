using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.CustomerDashboard;
using HPCL.Common.Models.ResponseModel.CustomerDashboard;
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
    public class CustomerDashBoardService : ICustomerDashBoardServices
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;

        public CustomerDashBoardService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }
        public async Task<List<AccountSummaryResponseModel>> AccountSummary (string CustomerId)
        {
            var customerDashboardAccountSummaryForms = new CustomerDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID = CustomerId
            };

            StringContent customerDashboardAccountSummaryTableContent = new StringContent(JsonConvert.SerializeObject(customerDashboardAccountSummaryForms), Encoding.UTF8, "application/json");

            var customerDashboardAccountSummaryResponse = await _requestService.CommonRequestService(customerDashboardAccountSummaryTableContent, WebApiUrl.customerDashboardAccountSummary);

            JObject customerDashboardAccountSummaryTableObj = JObject.Parse(JsonConvert.DeserializeObject(customerDashboardAccountSummaryResponse).ToString());
            var customerDashboardAccountSummaryTableJarr = customerDashboardAccountSummaryTableObj["Data"].Value<JArray>();
            List<AccountSummaryResponseModel> accountSummaryResponseModel = customerDashboardAccountSummaryTableJarr.ToObject<List<AccountSummaryResponseModel>>();
            
            return accountSummaryResponseModel;
        }

        public async Task<List<VerifyYourDetailsResponseModel>> VerifyYourDetails(string CustomerId)
        {
            var verifyYourDetailsForms = new CustomerDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID = CustomerId
            };

            StringContent verifyYourDetailsTableContent = new StringContent(JsonConvert.SerializeObject(verifyYourDetailsForms), Encoding.UTF8, "application/json");

            var verifyYourDetailsResponse = await _requestService.CommonRequestService(verifyYourDetailsTableContent, WebApiUrl.customerDashboardVerifyYourDetails);

            JObject verifyYourDetailsTableObj = JObject.Parse(JsonConvert.DeserializeObject(verifyYourDetailsResponse).ToString());
            var verifyYourDetailsTableJarr = verifyYourDetailsTableObj["Data"].Value<JArray>();
            List<VerifyYourDetailsResponseModel> verifyYourDetailsResponseModel = verifyYourDetailsTableJarr.ToObject<List<VerifyYourDetailsResponseModel>>();

            return verifyYourDetailsResponseModel;
        }
        public async Task<List<ReminderResponseModel>> Reminder(string CustomerId)
        {
            var reminderForms = new CustomerDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID = CustomerId
            };

            StringContent reminderTableContent = new StringContent(JsonConvert.SerializeObject(reminderForms), Encoding.UTF8, "application/json");

            var reminderResponse = await _requestService.CommonRequestService(reminderTableContent, WebApiUrl.customerDashboardReminder);

            JObject reminderTableObj = JObject.Parse(JsonConvert.DeserializeObject(reminderResponse).ToString());
            var reminderTableJarr = reminderTableObj["Data"].Value<JArray>();
            List<ReminderResponseModel> reminderResponseModel = reminderTableJarr.ToObject<List<ReminderResponseModel>>();

            return reminderResponseModel;
        }

        public async Task<List<KeyEventsResponseModel>> KeyEvents(string CustomerId)
        {
            var KeyEventsForms = new CustomerDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID = CustomerId
            };

            StringContent KeyEventsTableContent = new StringContent(JsonConvert.SerializeObject(KeyEventsForms), Encoding.UTF8, "application/json");

            var KeyEventsResponse = await _requestService.CommonRequestService(KeyEventsTableContent, WebApiUrl.customerDashboardKeyEvent);

            JObject KeyEventsTableObj = JObject.Parse(JsonConvert.DeserializeObject(KeyEventsResponse).ToString());
            var KeyEventsTableJarr = KeyEventsTableObj["Data"].Value<JArray>();
            List<KeyEventsResponseModel> KeyEventsResponseModel = KeyEventsTableJarr.ToObject<List<KeyEventsResponseModel>>();

            return KeyEventsResponseModel;
        }

        public async Task<List<LastFiveTransactionsResponseModel>> LastFiveTransactions(string CustomerId)
        {
            var LastFiveTransactionsForms = new CustomerDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID = CustomerId
            };

            StringContent LastFiveTransactionsTableContent = new StringContent(JsonConvert.SerializeObject(LastFiveTransactionsForms), Encoding.UTF8, "application/json");

            var LastFiveTransactionsResponse = await _requestService.CommonRequestService(LastFiveTransactionsTableContent, WebApiUrl.customerDashboardLastTransactions);

            JObject LastFiveTransactionsTableObj = JObject.Parse(JsonConvert.DeserializeObject(LastFiveTransactionsResponse).ToString());
            var LastFiveTransactionsTableJarr = LastFiveTransactionsTableObj["Data"].Value<JArray>();
            List<LastFiveTransactionsResponseModel> LastFiveTransactionsResponseModel = LastFiveTransactionsTableJarr.ToObject<List<LastFiveTransactionsResponseModel>>();

            return LastFiveTransactionsResponseModel;
        }

        public Task<KeyEventsResponseModel> KeyEvents(CustomerDashboardRequestModel customerDashboardRequestModel)
        {
            throw new NotImplementedException();
        }

        public Task<LastFiveTransactionsResponseModel> LastFiveTransactions(CustomerDashboardRequestModel customerDashboardRequestModel)
        {
            throw new NotImplementedException();
        }
    }
}
