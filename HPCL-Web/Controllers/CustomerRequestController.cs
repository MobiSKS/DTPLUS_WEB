using HPCL.Common.Helper;
using Microsoft.AspNetCore.Mvc;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class CustomerRequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ConfigureSMSAlertstoMultipleMobileNumber()
        {
            return View();
        }
    }
}
