using HPCL.Common.Models.RequestModel.CustomerDashboard;
using HPCL.Common.Models.ResponseModel.CustomerDashboard;
using HPCL.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class CustomerDashBoardService : ICustomerDashBoardServices
    {
        public async Task<AccountSummaryResponseModel> AccountSummary (CustomerDashboardRequestModel customerDashboardRequestModel)
        {
            AccountSummaryResponseModel accountSummaryResponseModel = null;
            return accountSummaryResponseModel;
        }

        public async Task<VerifyYourDetailsResponseModel> VerifyYourDetails(CustomerDashboardRequestModel customerDashboardRequestModel)
        {
            VerifyYourDetailsResponseModel verifyYourDetailsResponseModel = null;
            return verifyYourDetailsResponseModel;
        }
        public async Task<ReminderResponseModel> Reminder(CustomerDashboardRequestModel customerDashboardRequestModel)
        {
            ReminderResponseModel reminderResponseModel = null;
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
