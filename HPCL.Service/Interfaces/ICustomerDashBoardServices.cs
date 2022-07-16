using HPCL.Common.Models.RequestModel.CustomerDashboard;
using HPCL.Common.Models.ResponseModel.CustomerDashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ICustomerDashBoardServices
    {
        Task<AccountSummaryResponseModel> AccountSummary(CustomerDashboardRequestModel customerDashboardRequestModel);
        Task<VerifyYourDetailsResponseModel> VerifyYourDetails(CustomerDashboardRequestModel customerDashboardRequestModel);
        Task<ReminderResponseModel> Reminder(CustomerDashboardRequestModel customerDashboardRequestModel);
        Task<KeyEventsResponseModel> KeyEvents(CustomerDashboardRequestModel customerDashboardRequestModel);
        Task<LastFiveTransactionsResponseModel> LastFiveTransactions(CustomerDashboardRequestModel customerDashboardRequestModel);
    }
}
