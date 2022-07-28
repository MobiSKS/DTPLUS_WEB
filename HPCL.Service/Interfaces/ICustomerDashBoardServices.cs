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
        Task<List<KeyEventsResponseModel>> KeyEvents(string CustomerId);
        Task<List<LastFiveTransactionsResponseModel>> LastFiveTransactions(string CustomerId);
        Task<List<LastestDrivestarsTransactionResponseModel>> LastestDrivestarsTransaction(string CustomerId);
        Task<List<GetNotificationContentResponseModel>> GetNotificationContent(string UserType);
        
    }
}
