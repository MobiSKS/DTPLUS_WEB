using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.ManageRbe;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class ManageRbeController : Controller
    {
        private readonly IManageRbeService _manageRbeService;

        public ManageRbeController(IManageRbeService manageRbeService)
        {
            _manageRbeService = manageRbeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangeRbeMapping()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ChangeRbeMapping(RbeMapping entity)
        {
            var searchList = await _manageRbeService.ChangeRbeMapping(entity);
            return Json(new { searchList = searchList });
        }

        public IActionResult RbeUserList()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> RbeUserList(RbeUserList entity)
        {
            var searchList = await _manageRbeService.RbeUserList(entity);
            return Json(new { searchList = searchList });
        }

        public IActionResult RbeDeviceIdResetRequest()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> RbeDeviceIdResetRequest(RbeDeviceIdResetRequest entity)
        {
            var searchList = await _manageRbeService.RbeDeviceIdResetRequestService(entity);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> ChangeRbeMappingByUserName(string newUserName, string userName)
        {
            var changeList = await _manageRbeService.ChangeRbeMappingByUserName(newUserName, userName);
            return Json(new { changeList = changeList });
        }

        [HttpPost]
        public async Task<JsonResult> UserNameVerifyOtp(string newUserName, string userName, string otp)
        {
            var resMsg = await _manageRbeService.UserNameVerifyOtp(newUserName, userName, otp);
            return Json(new { resMsg = resMsg });
        }

        public IActionResult ApproveChangedRbeMapping()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ApproveChangedRbeMapping(GetApproveChangedRbeMapping entity)
        {
            var searchList = await _manageRbeService.ApproveChangedRbeMapping(entity);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> ApproveRejectChangedRbeMapping(string userName, string actionPress)
        {
            var resp = await _manageRbeService.ApproveRejectChangedRbeMappingSerivce(userName, actionPress);
            return Json(new { resp = resp });
        }

        public IActionResult RbeMobileChangeRequest()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> RbeMobileChangeRequest(RbeMobileChange entity)
        {
            var searchList = await _manageRbeService.RbeMobileChangeRequestService(entity);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> GetOtpMobileChangeReq(string newMobileNo)
        {
            var resp = await _manageRbeService.GetOtpMobileChangeReqService(newMobileNo);
            return Json(new { resp = resp });
        }

        [HttpPost]
        public async Task<JsonResult> VerifyOtpMobileChangeReq(string existMobNo, string newMobileNo, string otp)
        {
            var resp = await _manageRbeService.VerifyOtpMobileChangeReqService(existMobNo, newMobileNo, otp);
            return Json(new { resp = resp });
        }

        public IActionResult ApproveChangeRbeMobile()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ApproveChangeRbeMobile(GetApproveChangeRbeMobile entity)
        {
            var searchList = await _manageRbeService.ApproveChangeRbeMobileService(entity);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> ApproveRejectChangedRbeMobile(string existMobNo, string mappingStatus)
        {
            var resp = await _manageRbeService.ApproveRejectChangedRbeMobileService(existMobNo, mappingStatus);
            return Json(new { resp = resp });
        }

        public IActionResult DeviceIdResetRequest()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> DeviceIdResetRequest(GetDeviceIdResetRequest entity)
        {
            var searchList = await _manageRbeService.GetDeviceIdResetRequestSerivce(entity);
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> GetOtpRbeDeviceResetService(string mobNo)
        {
            var resp = await _manageRbeService.GetOtpRbeDeviceResetService(mobNo);
            return Json(new { resp = resp });
        }

        [HttpPost]
        public async Task<JsonResult> ValidateOtpRbeDeviceResetService(string MobileNo, string otp)
        {
            var resp = await _manageRbeService.ValidateOtpRbeDeviceResetService(MobileNo, otp);
            return Json(new { resp = resp });
        }
    }
}
