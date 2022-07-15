using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.Security;
using HPCL.Common.Models.ResponseModel.Security;
using HPCL.Common.Models.ViewModel.Security;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ISecurityService
    {
        Task<BindGridResponse> UserCreationApproval(BindGrid entity);
        Task<ViewRbeDetailsResponse> ViewRbeDetails(string userName);
        Task<string> ApproveRbeUser(string userName, string comments);
        Task<string> RejectRbeUser(string userName, string comments);
        Task<UserCreationRequestViewModel> UserCreationRequestView(UserCreationRequestViewModel model);
        Task<GetManageUserResponse> GetManageUsers(GetManageUser entity);
        Task<List<SuccessResponse>> UserResetPassword(string userName, string EmailId);
        Task<List<SuccessResponse>> DisableUpdateManageUser(string userName, string action);
        Task<List<SuccessResponse>> DeleteManageUser(string userList);
        Task<List<SuccessResponse>> AddUser([FromBody] AddNewUserReq entity);
        Task<GetUserRoleLocationResponse> GetUserRoleLocation(string UserName);
        Task<UserCreationApprovalNonRBEModel> UserCreationApprovalNonRBE(UserCreationApprovalNonRBEModel model);
        Task<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse> UserApprovalRejectionNonRBE([FromBody] UserApprovalRejectionRequest model);
        Task<GetUserManageRoleModel> GetUserManageRoleList(string RoleId);
        Task<ManageRolesViewModel> SelectUserManageRolesRequest(ManageRolesRequestModel manageRolesRequestModel);
        Task<GetUserManageMenuModel> GetUserManageMenuList();
        Task<List<SuccessResponse>> DeleteRoles([FromBody] ManageRolesRequestModel manageRolesRequestModel);
        Task<List<SuccessResponse>> UpdateManageRole(ManageRolesRequestModel manageRolesRequestModel);
        Task<List<SuccessResponse>> InsertManageRole([FromBody] ManageRolesRequestModel manageRolesRequestModel);
        Task<GetUserRolesAndRegions> GetUserRolesAndRegions();
        Task<string> ValidateManageUserName(string UserName);
        Task<List<SuccessResponse>> UpdateUser([FromBody] AddNewUserReq entity);
        Task<ManageNewUserViewModel> GetManageUserForEdit(string UserName);
        Task<List<SuccessResponse>> DeleteLocationMapping([FromBody] AddNewUserReq entity);
        Task<List<SuccessResponse>> UserCreationRequest([FromBody] UserCreationReqModel reqEntity);
    }
}