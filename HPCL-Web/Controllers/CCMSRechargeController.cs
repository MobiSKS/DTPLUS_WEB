using HPCL.Common.Helper;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    public class CCMSRechargeController : Controller
    {
        private readonly ICCMSRechargeService _cCMSRechargeService;

        public CCMSRechargeController(ICCMSRechargeService cCMSRechargeService)
        {
            _cCMSRechargeService = cCMSRechargeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CCMSRechargePage()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CCMSRechargePage(string mobNo)
        {
            var searchList = await _cCMSRechargeService.GetDetailsByMObNo(mobNo);

            return Json(new { searchList = searchList });
        }
    }
}
