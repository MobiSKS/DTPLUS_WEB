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
using HPCL.Common.Models.ResponseModel.CommonResponse;
using Microsoft.AspNetCore.Mvc;
using HPCL.Common.Models.ViewModel;

namespace HPCL.Service.Services
{
    public class CustomerDashBoardService : ICustomerDashBoardServices
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public CustomerDashBoardService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }
        public async Task<List<AccountSummaryResponseModel>> AccountSummary (string CustomerId)
        {
            var customerDashboardAccountSummaryForms = new CustomerDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID = "2000000237"
            };

            StringContent customerDashboardAccountSummaryTableContent = new StringContent(JsonConvert.SerializeObject(customerDashboardAccountSummaryForms), Encoding.UTF8, "application/json");

            var customerDashboardAccountSummaryResponse = await _requestService.CommonRequestService(customerDashboardAccountSummaryTableContent, WebApiUrl.customerDashboardAccountSummary);
            if (string.IsNullOrEmpty(customerDashboardAccountSummaryResponse))
            {
                List<AccountSummaryResponseModel> accountSummaryResponseModels = new List<AccountSummaryResponseModel>();
                return accountSummaryResponseModels;
            }
            else
            {
                JObject customerDashboardAccountSummaryTableObj = JObject.Parse(JsonConvert.DeserializeObject(customerDashboardAccountSummaryResponse).ToString());
                var customerDashboardAccountSummaryTableJarr = customerDashboardAccountSummaryTableObj["Data"].Value<JArray>();
                List<AccountSummaryResponseModel> accountSummaryResponseModel = customerDashboardAccountSummaryTableJarr.ToObject<List<AccountSummaryResponseModel>>();

                return accountSummaryResponseModel;
            }
        }

        public async Task<List<VerifyYourDetailsResponseModel>> VerifyYourDetails(string CustomerId)
        {
            var verifyYourDetailsForms = new CustomerDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID = "2000000237"
            };

            StringContent verifyYourDetailsTableContent = new StringContent(JsonConvert.SerializeObject(verifyYourDetailsForms), Encoding.UTF8, "application/json");

            var verifyYourDetailsResponse = await _requestService.CommonRequestService(verifyYourDetailsTableContent, WebApiUrl.customerDashboardVerifyYourDetails);

            if (string.IsNullOrEmpty(verifyYourDetailsResponse))
            {
                List<VerifyYourDetailsResponseModel> verifyYourDetailsResponseModel = new List<VerifyYourDetailsResponseModel>();
                return verifyYourDetailsResponseModel;
            }
            else
            {
                JObject verifyYourDetailsTableObj = JObject.Parse(JsonConvert.DeserializeObject(verifyYourDetailsResponse).ToString());
                var verifyYourDetailsTableJarr = verifyYourDetailsTableObj["Data"].Value<JArray>();
                List<VerifyYourDetailsResponseModel> verifyYourDetailsResponseModel = verifyYourDetailsTableJarr.ToObject<List<VerifyYourDetailsResponseModel>>();

                return verifyYourDetailsResponseModel;
            }


        }
        public async Task<List<ReminderResponseModel>> Reminder(string CustomerId)
        {
            var reminderForms = new CustomerDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID = "2000000114"
            };

            StringContent reminderTableContent = new StringContent(JsonConvert.SerializeObject(reminderForms), Encoding.UTF8, "application/json");

            var reminderResponse = await _requestService.CommonRequestService(reminderTableContent, WebApiUrl.customerDashboardReminder);

            if (string.IsNullOrEmpty(reminderResponse))
            {
                List<ReminderResponseModel> reminderResponseModel = new List<ReminderResponseModel>();
                return reminderResponseModel;
            }
            else
            {
                JObject reminderTableObj = JObject.Parse(JsonConvert.DeserializeObject(reminderResponse).ToString());
                var reminderTableJarr = reminderTableObj["Data"].Value<JArray>();
                List<ReminderResponseModel> reminderResponseModel = reminderTableJarr.ToObject<List<ReminderResponseModel>>();

                return reminderResponseModel;
            }
        }

        public async Task<List<KeyEventsResponseModel>> KeyEvents(string CustomerId)
        {
            var KeyEventsForms = new CustomerDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID = "2600000097"
            };

            StringContent KeyEventsTableContent = new StringContent(JsonConvert.SerializeObject(KeyEventsForms), Encoding.UTF8, "application/json");

            var KeyEventsResponse = await _requestService.CommonRequestService(KeyEventsTableContent, WebApiUrl.customerDashboardKeyEvent);

            if (string.IsNullOrEmpty(KeyEventsResponse))
            {
                List<KeyEventsResponseModel> KeyEventsResponseModel = new List<KeyEventsResponseModel>();
                return KeyEventsResponseModel;
            }
            else
            {
                JObject KeyEventsTableObj = JObject.Parse(JsonConvert.DeserializeObject(KeyEventsResponse).ToString());
                var KeyEventsTableJarr = KeyEventsTableObj["Data"].Value<JArray>();
                List<KeyEventsResponseModel> KeyEventsResponseModel = KeyEventsTableJarr.ToObject<List<KeyEventsResponseModel>>();

                return KeyEventsResponseModel;
            }
        }

        public async Task<List<LastFiveTransactionsResponseModel>> LastFiveTransactions(string CustomerId)
        
        {
            var LastFiveTransactionsForms = new CustomerDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID = "2000000141"
            };

            StringContent LastFiveTransactionsTableContent = new StringContent(JsonConvert.SerializeObject(LastFiveTransactionsForms), Encoding.UTF8, "application/json");

            var LastFiveTransactionsResponse = await _requestService.CommonRequestService(LastFiveTransactionsTableContent, WebApiUrl.customerDashboardLastTransactions);

            if (string.IsNullOrEmpty(LastFiveTransactionsResponse))
            {
                List<LastFiveTransactionsResponseModel> LastFiveTransactionsResponseModel = new List<LastFiveTransactionsResponseModel>();
                return LastFiveTransactionsResponseModel;
            }
            else
            {
                JObject LastFiveTransactionsTableObj = JObject.Parse(JsonConvert.DeserializeObject(LastFiveTransactionsResponse).ToString());
                var LastFiveTransactionsTableJarr = LastFiveTransactionsTableObj["Data"].Value<JArray>();
                List<LastFiveTransactionsResponseModel> LastFiveTransactionsResponseModel = LastFiveTransactionsTableJarr.ToObject<List<LastFiveTransactionsResponseModel>>();

                return LastFiveTransactionsResponseModel;
            }
        }

        public async Task<List<LastestDrivestarsTransactionResponseModel>> LastestDrivestarsTransaction(string CustomerId)
        {
            var LastestDrivestarsTransactionForms = new CustomerDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID = ""
            };

            StringContent LastestDrivestarsTransactionTableContent = new StringContent(JsonConvert.SerializeObject(LastestDrivestarsTransactionForms), Encoding.UTF8, "application/json");

            var LastestDrivestarsTransactionResponse = await _requestService.CommonRequestService(LastestDrivestarsTransactionTableContent, WebApiUrl.customerDashboardLastestDrivestarsTransactions);

            if (string.IsNullOrEmpty(LastestDrivestarsTransactionResponse))
            {
                List<LastestDrivestarsTransactionResponseModel> LastFiveTransactionsResponseModel = new List<LastestDrivestarsTransactionResponseModel>();
                return LastFiveTransactionsResponseModel;
            }
            else
            {
                JObject LastestDrivestarsTransactionTableObj = JObject.Parse(JsonConvert.DeserializeObject(LastestDrivestarsTransactionResponse).ToString());
                var LastestDrivestarsTransactionTableJarr = LastestDrivestarsTransactionTableObj["Data"].Value<JArray>();
                List<LastestDrivestarsTransactionResponseModel> LastestDrivestarsTransactionResponseModel = LastestDrivestarsTransactionTableJarr.ToObject<List<LastestDrivestarsTransactionResponseModel>>();

                return LastestDrivestarsTransactionResponseModel;
            }
        }

        public async Task<List<GetNotificationContentResponseModel>> GetNotificationContent(string UserType)
        {
           var GetNotificationContentForms = new CustomerDashboardRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserType = "Customer"
            };

            StringContent GetNotificationContentTableContent = new StringContent(JsonConvert.SerializeObject(GetNotificationContentForms), Encoding.UTF8, "application/json");

            var GetNotificationContentResponse = await _requestService.CommonRequestService(GetNotificationContentTableContent, WebApiUrl.customerDashboardGetNotificationContent);

            if (string.IsNullOrEmpty(GetNotificationContentResponse))
            {
                List<GetNotificationContentResponseModel> GetNotificationContentResponseModel = new List<GetNotificationContentResponseModel>();
                return GetNotificationContentResponseModel;
            }
            else
            {
                JObject GetNotificationContentTableObj = JObject.Parse(JsonConvert.DeserializeObject(GetNotificationContentResponse).ToString());
                var GetNotificationContentTableJarr = GetNotificationContentTableObj["Data"].Value<JArray>();
                List<GetNotificationContentResponseModel> GetNotificationContentResponseModel = GetNotificationContentTableJarr.ToObject<List<GetNotificationContentResponseModel>>();

                return GetNotificationContentResponseModel;
            }
        }

        public Task GetNotificationContent(object userType)
        {
            throw new NotImplementedException();
        }
    }
}
