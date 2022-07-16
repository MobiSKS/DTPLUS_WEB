using HPCL.Common.Helper;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
