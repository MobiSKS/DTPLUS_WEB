using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.DICV;
using HPCL.Common.Resources;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class DICVController : Controller
    {
        private readonly IDICVService _dicvService;
        private readonly ICommonActionService _commonActionService;
        public DICVController(IDICVService dicvService, ICommonActionService commonActionService)
        {
            _dicvService = dicvService;
            _commonActionService = commonActionService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> DICVDealerEnrollment()
        {
            var modals = await _dicvService.DICVDealerEnrollment();
            return View(modals);
        }
        [HttpPost]
        public async Task<JsonResult> DICVDealerEnrollment(string str)
        {
            var result = await _dicvService.InsertDICVDealerEnrollment(str);
            return Json(new { result = result });
        }
        [HttpPost]
        public async Task<JsonResult> SearchDICVDealer(string dealerCode, string dtpCode, string OfficerType)
        {
            var searchList = await _dicvService.SearchDICVDealer(dealerCode, dtpCode, OfficerType);
            return Json(new { searchList = searchList });
        }
        [HttpPost]
        public async Task<JsonResult> DICVDealerEnrollmentUpdate(string getAllData)
        {
            var result = await _dicvService.DICVDealerEnrollmentUpdate(getAllData);
            return Json(new { result = result });
        }
        [HttpPost]
        public async Task<JsonResult> CheckDICVDealerCodeExists(string DealerCode)
        {
            var responseData = await _dicvService.CheckDICVDealerCodeExists(DealerCode);

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
        public async Task<IActionResult> DICVDealerOTCCardRequest()
        {
            var modals = await _dicvService.DICVDealerOTCCardRequest();
            return View(modals);
        }
        [HttpPost]
        public async Task<JsonResult> CheckDICVDealerBalanceQty(string DealerCode)
        {
            var responseData = await _dicvService.CheckDICVDealerBalanceQty(DealerCode);

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
        public async Task<IActionResult> DICVDealerOTCCardRequest(DICVOTCCardRequestModel model)
        {
            model = await _dicvService.DICVDealerOTCCardRequest(model);

            if (model.Internel_Status_Code == 1000)
            {
                return RedirectToAction("SuccessRedirectDICVOTCCardRequest", new { Message = model.Remarks });
            }

            return View(model);
        }

        public async Task<IActionResult> SuccessRedirectDICVOTCCardRequest(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }

    }
}
