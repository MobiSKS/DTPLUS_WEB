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
                UserIp = CommonBase.userip,
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
                UserIp = CommonBase.userip,
                CustomerID = CustomerId
            };

            StringContent verifyYourDetailsTableContent = new StringContent(JsonConvert.SerializeObject(verifyYourDetailsForms), Encoding.UTF8, "application/json");

            var verifyYourDetailsResponse = await _requestService.CommonRequestService(verifyYourDetailsTableContent, WebApiUrl.customerDashboardAccountSummary);

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
                UserIp = CommonBase.userip,
                CustomerID = CustomerId
            };

            StringContent reminderTableContent = new StringContent(JsonConvert.SerializeObject(reminderForms), Encoding.UTF8, "application/json");

            var reminderResponse = await _requestService.CommonRequestService(reminderTableContent, WebApiUrl.customerDashboardAccountSummary);

            JObject reminderTableObj = JObject.Parse(JsonConvert.DeserializeObject(reminderResponse).ToString());
            var reminderTableJarr = reminderTableObj["Data"].Value<JArray>();
            List<ReminderResponseModel> reminderResponseModel = reminderTableJarr.ToObject<List<ReminderResponseModel>>();

            return reminderResponseModel;
        }

        public async Task<KeyEventsResponseModel> KeyEvents(CustomerDashboardRequestModel customerDashboardRequestModel)
        {
            KeyEventsResponseModel keyEventsResponseModel = null;
            return keyEventsResponseModel;
        }

        public async Task<LastFiveTransactionsResponseModel> LastFiveTransactions(CustomerDashboardRequestModel customerDashboardRequestModel)
        {
            LastFiveTransactionsResponseModel lastFiveTransactionsResponseModel = null;
            return lastFiveTransactionsResponseModel;
        }

    }
}
