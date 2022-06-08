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
        Task<List<SuccessResponse>> UserResetPassword(string userName);
        Task<List<SuccessResponse>> DisableUpdateManageUser(string userName, string action);
        Task<List<SuccessResponse>> DeleteManageUser(string userList);
        Task<List<SuccessResponse>> AddUser(AddNewUserReq entity);
        Task<GetUserRoleLocationResponse> GetUserRoleLocation(string UserName);
        Task<UserCreationApprovalNonRBEModel> UserCreationApprovalNonRBE(UserCreationApprovalNonRBEModel model);
        Task<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse> UserApprovalRejectionNonRBE([FromBody] UserApprovalRejectionRequest model);
        Task<GetUserManageRoleModel> GetUserManageRoleList(string RoleId);
        Task<ManageRolesViewModel> SelectUserManageRolesRequest(ManageRolesRequestModel manageRolesRequestModel);
        Task<ManageRolesViewModel> GetUserManageMenuList(ManageRolesRequestModel manageRolesRequestModel);
        Task<List<SuccessResponse>> DeleteRoles(ManageRolesRequestModel manageRolesRequestModel);
        Task<List<SuccessResponse>> UpdateManageRole(ManageRolesRequestModel manageRolesRequestModel);
        Task<List<SuccessResponse>> InsertManageRole(ManageRolesRequestModel manageRolesRequestModel);
    }
}