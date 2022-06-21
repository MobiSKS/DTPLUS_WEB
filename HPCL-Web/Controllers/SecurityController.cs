﻿using HPCL.Common.Helper;
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
        public async Task<IActionResult> UserCreationApprovalNonRBE(UserCreationApprovalNonRBEModel model, string reset, string success, string error, string FirstName, string UserName)
        {
            var searchResult = await _securityService.UserCreationApprovalNonRBE(model);
            ViewBag.Reset = String.IsNullOrEmpty(reset) ? "" : reset;
            ViewBag.SuccessMessage = success;
            ViewBag.ErrorMessage = error;
            if (!String.IsNullOrEmpty(reset))
            {
                model.FirstName = "";
                model.UserName = "";
            }
            return View(searchResult);
        }
        [HttpPost]
        public async Task<JsonResult> UserApprovalRejectionNonRBE([FromBody] UserApprovalRejectionRequest model)
        {
            var updateKycResponse = await _securityService.UserApprovalRejectionNonRBE(model);

            return Json(new { customer = updateKycResponse });
        }
        public async Task<IActionResult> ManageRole(ManageRolesRequestModel manageRolesRequestModel,string success,string error)
        {
            var modals = await _securityService.SelectUserManageRolesRequest(manageRolesRequestModel);
            ViewBag.SuccessMessage = success;
            ViewBag.ErrorMessage = error;
            return View(modals);
        }
        public async Task<IActionResult> RolePermissionSummaryView (string RoleName,string RoleDescription,string RoleId)
        {
            var modals = await _securityService.GetUserManageRoleList(RoleId);
            modals.RoleDescription = RoleDescription;
            modals.RoleName = RoleName;
            return View(modals);
        }
        public async Task<IActionResult> AddRolesandPermissions()
        {
            var modals = await _securityService.GetUserManageMenuList();
            
            return View(modals);
        }
        [HttpPost]
        public async Task<JsonResult> InsertManageRole([FromBody] ManageRolesRequestModel manageRolesRequestModel)
        {
            var result = await _securityService.InsertManageRole(manageRolesRequestModel);
            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> DeleteRoles([FromBody] ManageRolesRequestModel manageRolesRequestModel)
        {
            var result = await _securityService.DeleteRoles(manageRolesRequestModel);
            return Json(result);
        }
        public async Task<IActionResult> DeleteRoleRow(string RoleName,string RoleDescription)
        {
            ManageRolesRequestModel manageRolesRequestModel = new ManageRolesRequestModel();
            List<DeleteRoleModel> roleModelList = new List<DeleteRoleModel>();
            DeleteRoleModel deleteRoleModel = new DeleteRoleModel();
            deleteRoleModel.RoleDescription = RoleDescription;
            deleteRoleModel.RoleName = RoleName;
            roleModelList.Add(deleteRoleModel);
            manageRolesRequestModel.TypeRoleNameAndRoleDescriptionMapping = roleModelList;
            var result = await _securityService.DeleteRoles(manageRolesRequestModel);
            string succesMsg = "", errorMsg = "";
            if (result[0].Status == 1)
                succesMsg = result[0].Reason;
            else if (result[0].Status == 0)
                errorMsg = result[0].Reason;
            return RedirectToAction("ManageRole", "Security", new { success = succesMsg, error = errorMsg });
        }
        public async Task<IActionResult> UpdateRolesandPermissions(string RoleName, string RoleDescription, string RoleId)
        {
            var modals = await _securityService.GetUserManageRoleList(RoleId);
            modals.RoleDescription = RoleDescription;
            modals.RoleName = RoleName;
            return View(modals);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateManageRole([FromBody] ManageRolesRequestModel manageRolesRequestModel)
        {
            var result = await _securityService.UpdateManageRole(manageRolesRequestModel);
            return Json(result);
        }
    }
}
