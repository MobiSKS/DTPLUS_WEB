using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.Security;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class SecurityController : Controller
    {
        private readonly ISecurityService _securityService;
        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public IActionResult UserCreationApproval()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> UserCreationApproval(BindGrid entity)
        {
            var rbeDetails = await _securityService.UserCreationApproval(entity);

            ModelState.Clear();
            return Json(new { rbeDetails = rbeDetails });
        }

        [HttpPost]
        public async Task<JsonResult> ViewRbeDetails(string userName)
        {
            var viewRbeDetailsList = await _securityService.ViewRbeDetails(userName);

            ModelState.Clear();
            return Json(new { viewRbeDetailsList = viewRbeDetailsList });
        }

        [HttpPost]
        public async Task<JsonResult> ApproveRbeUser(string userName, string comments)
        {
            var reason = await _securityService.ApproveRbeUser(userName, comments);

            ModelState.Clear();
            return Json(reason);
        }

        [HttpPost]
        public async Task<JsonResult> RejectRbeUser(string userName, string comments)
        {
            var reason = await _securityService.ApproveRbeUser(userName, comments);

            ModelState.Clear();
            return Json(reason);
        }

        public IActionResult ManageUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ManageUser(string searchItemList)
        {
            List<GetManageUser> arrs = JsonConvert.DeserializeObject<List<GetManageUser>>(searchItemList);

            var searchList = await _securityService.GetManageUsers(arrs[0]);

            ModelState.Clear();
            return Json(new { searchList = searchList });
        }

        [HttpPost]
        public async Task<JsonResult> DisableUpdateManageUser(string userName, string action)
        {
            var reasonList = await _securityService.DisableUpdateManageUser(userName,action);

            ModelState.Clear();
            return Json(new { reasonList = reasonList });
        }

        [HttpPost]
        public async Task<JsonResult> UserResetPassword(string userName)
        {
            var reasonList = await _securityService.UserResetPassword(userName);

            ModelState.Clear();
            return Json(new { reasonList = reasonList });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteManageUser(string userList)
        {
            var reasonList = await _securityService.DeleteManageUser(userList);

            ModelState.Clear();
            return Json(new { reasonList = reasonList });
        }
    }
}
