using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.JCB;
using HPCL.Common.Resources;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class JCBController : Controller
    {
        private readonly IJCBService _jcbService;
        private readonly ICommonActionService _commonActionService;
        public JCBController(IJCBService jcbService, ICommonActionService commonActionService)
        {
            _jcbService = jcbService;
            _commonActionService = commonActionService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult JCBDealerEnrollment()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> JCBDealerEnrollment(string str)
        {
            var result = await _jcbService.InsertJCBDealerEnrollment(str);
            return Json(new { result = result });
        }

        [HttpPost]
        public async Task<JsonResult> JCBDealerEnrollmentUpdate(string getAllData)
        {
            var result = await _jcbService.JCBDealerEnrollmentUpdate(getAllData);
            return Json(new { result = result });
        }

        [HttpPost]
        public async Task<JsonResult> CheckJCBDealerCodeExists(string DealerCode)
        {
            var responseData = await _jcbService.CheckJCBDealerCodeExists(DealerCode);

            ModelState.Clear();

            if (responseData.Internel_Status_Code.ToString() == Constants.SuccessInternelStatusCode)
            {
                return Json(responseData);
            }
            else
            {
                return Json("Failed to load Dealer Details");
            }
        }

        [HttpPost]
        public async Task<JsonResult> SearchJCBDealer(string dealerCode, string dtpCode, string OfficerType)
        {
            var searchList = await _jcbService.SearchJCBDealer(dealerCode, dtpCode, OfficerType);
            return Json(new { searchList = searchList });
        }
        public async Task<IActionResult> JCBDealerOTCCardRequest()
        {
            var modals = await _jcbService.JCBDealerOTCCardRequest();
            return View(modals);
        }

        [HttpPost]
        public async Task<JsonResult> CheckJCBDealerBalanceQty(string DealerCode)
        {
            var responseData = await _jcbService.CheckJCBDealerBalanceQty(DealerCode);

            ModelState.Clear();

            if (responseData.Internel_Status_Code.ToString() == Constants.SuccessInternelStatusCode)
            {
                return Json(responseData);
            }
            else
            {
                return Json("Failed to load Dealer Details");
            }
        }
        [HttpPost]
        public async Task<IActionResult> JCBDealerOTCCardRequest(JCBOTCCardRequestModel model)
        {
            model = await _jcbService.JCBDealerOTCCardRequest(model);

            if (model.Internel_Status_Code == 1000)
            {
                return RedirectToAction("SuccessRedirectJCBOTCCardRequest", new { Message = model.Remarks });
            }

            return View(model);
        }

        public async Task<IActionResult> SuccessRedirectJCBOTCCardRequest(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }

    }
}
