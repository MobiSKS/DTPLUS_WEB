using Microsoft.AspNetCore.Mvc;

namespace HPCL_Web.Controllers
{
    public class HDFCBankCreditPouchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ExceptionRequestToAddCustomer()
        {
            return View();
        }
    }
}
