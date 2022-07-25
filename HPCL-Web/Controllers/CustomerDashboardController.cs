using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using HPCL.Common.Models.ResponseModel.CustomerDashboard;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class CustomerDashboardController : Controller
    {

        private readonly ICustomerDashBoardServices _customerDashboardService;
        private readonly ICommonActionService _commonActionService;

        public CustomerDashboardController(ICustomerDashBoardServices customerDashboardServices, ICommonActionService commonActionService)
        {
            _customerDashboardService = customerDashboardServices;
            _commonActionService = commonActionService;
        }
        public async Task<IActionResult> CustomerDashboard(string CustomerId)
        {
            var dashboardLst = await _customerDashboardService.AccountSummary(CustomerId);
            var verifyDetailsLst = await _customerDashboardService.VerifyYourDetails(CustomerId);
            var reminderLst = await _customerDashboardService.Reminder(CustomerId);
            var lastfivetran = await _customerDashboardService.LastFiveTransactions(CustomerId);
            var lastestDriveTrans = await _customerDashboardService.LastestDrivestarsTransaction(CustomerId);
            var keyevents = await _customerDashboardService.KeyEvents(CustomerId);

            CustomerDashboardModel customerDashbord = new CustomerDashboardModel();

            customerDashbord.accountSummaryResponseModels = dashboardLst;
            customerDashbord.verifyYourDetailsResponseModels = verifyDetailsLst;
            customerDashbord.reminderResponseModels = reminderLst;
            customerDashbord.LastFiveTransactionsResponseModels = lastfivetran;
            customerDashbord.LastestDrivestarsTransactionResponseModel = lastestDriveTrans;
            customerDashbord.KeyEventsResponseModels = keyevents;

             return View(customerDashbord);
        }
   
    }
}
