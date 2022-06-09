using HPCL.Common.Helper;
using Microsoft.AspNetCore.Mvc;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class CustomerApprovalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}