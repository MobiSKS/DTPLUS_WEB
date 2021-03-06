using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.CCMSRecharge;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<JsonResult> CCMSRechargePage(string mobNo, string customerId, string useriprecharge)
        {
            var searchList = await _cCMSRechargeService.GetDetailsByMObNoCust(mobNo, customerId, useriprecharge);

            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> RedirectToPG(string customerId, string mobNo, string controlCardNo, string amount, string useriprecharge)
        {
            var redirectDetails = await _cCMSRechargeService.RedirectToPG(customerId, mobNo, controlCardNo, amount, useriprecharge);

            return Json(new { redirectDetails = redirectDetails });
        }

        public IActionResult RedirectToPaymentGateway(string inputTxtValues)
        {
            RedirectToPGResponseData arrs = JsonConvert.DeserializeObject<RedirectToPGResponseData>(inputTxtValues);
            return View(arrs);
        }

        [HttpPost]
        public async Task<JsonResult> CCCMSRecGenerateOtp(string mobNo, string customerId, string useriprecharge)
        {
            var getOtp = await _cCMSRechargeService.CCCMSRecGenerateOtp(mobNo, customerId, useriprecharge);

            return Json(new { getOtp = getOtp });
        }

        [HttpPost]
        public async Task<JsonResult> CCCMSRecVerifyOtp(string mobNo, string otp, string customerId, string useriprecharge)
        {
            var verifyOtp = await _cCMSRechargeService.CCCMSRecVerifyOtp(mobNo, otp, customerId, useriprecharge);

            return Json(new { verifyOtp = verifyOtp });
        }
    }
}
