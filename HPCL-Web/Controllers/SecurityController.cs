using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.Security;
using HPCL.Common.Models.ViewModel.Security;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
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

        public async Task<IActionResult> UserCreationRequestView(UserCreationRequestViewModel model, string reset, string success, string error, string UserName, string Status, string FromDate, string ToDate)
        {
            var searchResult = await _securityService.UserCreationRequestView(model);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            ViewBag.SuccessMessage = success;
            ViewBag.ErrorMessage = error;
            if (!String.IsNullOrEmpty(reset))
            {
                model.Status = -1;
                model.FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
                model.ToDate = DateTime.Now.ToString("dd-MM-yyyy");
            }
            return View(searchResult);
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
        public async Task<ActionResult> GetUserRoleLocation(string UserName)
        {
            var model = await _securityService.GetUserRoleLocation(UserName);

            return PartialView("~/Views/Security/_ViewUserRoleLocationTbl.cshtml", model);
        }

        public IActionResult AddNewUser()
        {
            return View();
        }
        public async Task<IActionResult> ManageRole(ManageRolesRequestModel manageRolesRequestModel)
        {
            var modals = await _securityService.SelectUserManageRolesRequest(manageRolesRequestModel);
            return View(modals);
        }
        public async Task<IActionResult> RolePermissionSummaryView (string RoleName,string RoleDescription)
        {
            var modals = await _securityService.GetUserManageRoleList("1");
            modals.RoleDescription = RoleDescription;
            modals.RoleName = RoleName;
            return View(modals);
        }
    }
}
