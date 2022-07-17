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
        Task<List<AccountSummaryResponseModel>> AccountSummary(string CustomerId);
        Task<List<VerifyYourDetailsResponseModel>> VerifyYourDetails(string CustomerId);
        Task<List<ReminderResponseModel>> Reminder(string CustomerId);
        Task<KeyEventsResponseModel> KeyEvents(CustomerDashboardRequestModel customerDashboardRequestModel);
        Task<LastFiveTransactionsResponseModel> LastFiveTransactions(CustomerDashboardRequestModel customerDashboardRequestModel);
    }
}
