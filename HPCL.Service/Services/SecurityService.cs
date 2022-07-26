using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.RequestEnities;
using HPCL.Common.Models.RequestModel.Security;
using HPCL.Common.Models.ResponseModel.Security;
using HPCL.Common.Models.ViewModel.Security;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public SecurityService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<BindGridResponse> UserCreationApproval(BindGrid entity)
        {
            var bindDetails = new BindGrid();

            if (entity.UserName != null || entity.FirstName != null)
            {
                bindDetails = new BindGrid
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    FirstName = entity.FirstName,
                    UserName=entity.UserName,
                    StatusId=entity.StatusId
                };
            }
            else
            {
                bindDetails = new BindGrid
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    FirstName = "",
                    UserName="",
                    StatusId=""
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(bindDetails), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.BindRbeDetailsUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            BindGridResponse rbeDetails = obj.ToObject<BindGridResponse>();
            return rbeDetails;
        }

        public async Task<ViewRbeDetailsResponse> ViewRbeDetails(string userName)
        {
            var bindDetails = new ViewRbeDetails
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserName = userName
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(bindDetails), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.ViewRbeDataUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            ViewRbeDetailsResponse viewRbeDetailsList = obj.ToObject<ViewRbeDetailsResponse>();
            return viewRbeDetailsList;
        }

        public async Task<string> ApproveRbeUser(string userName, string comments)
        {
            var forms = new ApproveRejectRbeUser
            {
                UserAgent = CommonBase.useragent,
                UserIp= _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserName = userName,
                Approvalstatus = 4,
                Comments = comments,
                ApprovedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.ApproveRejectRbeUserUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();

            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            return responseMsg[0].Reason;
        }

        public async Task<string> RejectRbeUser(string userName, string comments)
        {
            var forms = new ApproveRejectRbeUser
            {
                UserAgent = CommonBase.useragent,
                UserIp= _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserName = userName,
                Approvalstatus = 13,
                Comments = comments,
                ApprovedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.ApproveRejectRbeUserUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();

            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            return responseMsg[0].Reason;
        }

        public async Task<UserCreationRequestViewModel> UserCreationRequestView(UserCreationRequestViewModel model)
        {
            if (string.IsNullOrEmpty(model.FromDate))
            {
                model.FromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                model.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            string strFromDate = "";
            string strToDate = "";
            if (!string.IsNullOrEmpty(model.FromDate))
            {
                string[] frmDate = model.FromDate.Split("-");
                strFromDate = frmDate[2] + "-" + frmDate[1] + "-" + frmDate[0];
            }
            if (!string.IsNullOrEmpty(model.ToDate))
            {
                string[] toDate = model.ToDate.Split("-");
                strToDate = toDate[2] + "-" + toDate[1] + "-" + toDate[0];
            }

            var reqBody = new UserCreationViewRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserName = string.IsNullOrEmpty(model.UserName) ? "" : model.UserName,
                FromDate = strFromDate,
                ToDate = strToDate,
                Status = (model.Status == -1 ? "" : model.Status.ToString())
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UserCreationRequestView);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            UserCreationRequestViewModel res = obj.ToObject<UserCreationRequestViewModel>();

            if (res != null && res.Data != null && res.Data.Count > 0)
            {
                model.Data = res.Data;
            }
            else
            {
                model.Data = new List<UserCreationRequestDetails>();
            }
            model.Message = res.Message;
            model.Internel_Status_Code = res.Internel_Status_Code;

            return model;
        }

        public async Task<GetManageUserResponse> GetManageUsers(GetManageUser entity)
        {
            var forms = new GetManageUser
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserName = entity.UserName,
                Email = entity.Email,
                LastLoginTime = entity.LastLoginTime,
                UserRole = entity.UserRole,
                ShowDisabled = entity.ShowDisabled
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetManageUserUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetManageUserResponse searchList = obj.ToObject<GetManageUserResponse>();
            return searchList;
        }

        public async Task<List<SuccessResponse>> UserResetPassword(string userName, string EmailId)
        {
            var forms = new GetUserResetPassword
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserName = userName,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                EmailId=EmailId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.UserResetPasswordUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            return responseMsg;
        }

        public async Task<List<SuccessResponse>> DisableUpdateManageUser(string userName, string action)
        {
            var forms = new UpdateManageUserRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserName = userName,
                Actions = action
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateManageUserUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            return responseMsg;
        }

        public async Task<List<SuccessResponse>> DeleteManageUser(string userList)
        {
            TypeDeleteUserManage[] arrs = JsonConvert.DeserializeObject<TypeDeleteUserManage[]>(userList);

            var forms = new DeleteManageUserRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                TypeDeleteUserManage = arrs
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.DeleteManageUserUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            return responseMsg;
        }

        public async Task<List<SuccessResponse>> AddUser([FromBody]AddNewUserReq entity)
        {
            var forms = new AddNewUserReq
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserName = entity.UserName,
                Email = entity.Email,
                Password = entity.Password,
                ConfirmPassword = entity.ConfirmPassword,
                SecretQuestion = entity.SecretQuestion,
                SecretQuestionAnswer = entity.SecretQuestionAnswer,
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FirstName=entity.FirstName,
                LastName=entity.LastName,
                ActionType="Insert",
                UserRole=entity.UserRole,
                TypeManageUsersAddUserRole=entity.TypeManageUsersAddUserRole,
                ModifiedBy= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                StateId=entity.StateId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.AddNewUserUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            return responseMsg;
        }
        public async Task<GetUserRoleLocationResponse> GetUserRoleLocation(string UserName)
        {
            var request = new GetUserRoleLocationRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserName = UserName
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetUserCreationApprovalRoleLocation);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetUserRoleLocationResponse roleLocationResponse = obj.ToObject<GetUserRoleLocationResponse>();
            if (roleLocationResponse != null)
            {
                roleLocationResponse.UserName = UserName;
            }
            return roleLocationResponse;
        }
        public async Task<UserCreationApprovalNonRBEModel> UserCreationApprovalNonRBE(UserCreationApprovalNonRBEModel model)
        {
            var reqBody = new UserCreationViewRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FirstName = String.IsNullOrEmpty(model.FirstName) ? "" : model.FirstName,
                UserName = String.IsNullOrEmpty(model.UserName) ? "" : model.UserName
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.userCreationApproval);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            UserCreationApprovalNonRBEModel res = obj.ToObject<UserCreationApprovalNonRBEModel>();

            if (res != null && res.Data != null && res.Data.Count > 0)
            {
                model.Data = res.Data;
            }
            else
            {
                model.Data = new List<UserCreationApprovalDetails>();
            }
            model.Message = res.Message;
            model.Internel_Status_Code = res.Internel_Status_Code;

            return model;
        }
        public async Task<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse> UserApprovalRejectionNonRBE([FromBody] UserApprovalRejectionRequest model)
        {
            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.UserAgent = CommonBase.useragent;
            model.UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress");
            model.ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(requestContent, WebApiUrl.userApprovalRejection);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            if (obj["Status_Code"].ToString() == "200")
            {
                var Jarr = obj["Data"].Value<JArray>();
                List<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse> updateResponse = Jarr.ToObject<List<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse>>();
                return updateResponse[0];
            }
            else
            {
                return new HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse();
            }
        }


        public async Task<GetUserManageRoleModel> GetUserManageRoleList(string RoleId)
        {
            var forms = new ManageRolesRequestModel
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                RoleId=RoleId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getusermanagerolelist);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetUserManageRoleModel searchList = obj.ToObject<GetUserManageRoleModel>();
            return searchList;
        }
        public async Task<ManageRolesViewModel> SelectUserManageRolesRequest(ManageRolesRequestModel manageRolesRequestModel)
        {
            var forms = new ManageRolesRequestModel
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                RoleName = manageRolesRequestModel.RoleName
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.SelectUserManageRolesRequest);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            ManageRolesViewModel searchList = obj.ToObject<ManageRolesViewModel>();
            return searchList;
        }
        public async Task<GetUserManageMenuModel> GetUserManageMenuList()
        {
            var forms = new ManageRolesRequestModel
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getusermanagemenulist);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetUserManageMenuModel searchList = obj.ToObject<GetUserManageMenuModel>();
            return searchList;
        }

        public async Task<List<SuccessResponse>> DeleteRoles([FromBody] ManageRolesRequestModel manageRolesRequestModel)
        {

            var forms = new ManageRolesRequestModel
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                TypeRoleNameAndRoleDescriptionMapping = manageRolesRequestModel.TypeRoleNameAndRoleDescriptionMapping
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.deleteroles);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            return responseMsg;
        }
        public async Task<List<SuccessResponse>> UpdateManageRole(ManageRolesRequestModel manageRolesRequestModel)
        {

            var forms = new ManageRolesRequestModel
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                RoleName= manageRolesRequestModel.RoleName,
                RoleDescription= manageRolesRequestModel.RoleDescription,
                ObjUpdate = manageRolesRequestModel.ObjUpdate,
                ModifiedBy= _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.updateinsertmanagerole);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            return responseMsg;
        }
        public async Task<List<SuccessResponse>> InsertManageRole([FromBody] ManageRolesRequestModel manageRolesRequestModel)
        {

            var forms = new ManageRolesRequestModel
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                RoleName = manageRolesRequestModel.RoleName,
                RoleDescription = manageRolesRequestModel.RoleDescription,
                TypeInsertAddManageUsers = manageRolesRequestModel.TypeInsertAddManageUsers,
                CreatedBy= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.InsertAddManageRole);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            return responseMsg;
        }
        public async Task<GetUserRolesAndRegions> GetUserRolesAndRegions()
        {
            var forms = new ManageRolesRequestModel
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getuserrolesandregions);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetUserRolesAndRegions searchList = obj.ToObject<GetUserRolesAndRegions>();
            return searchList;
        }
        public async Task<string> ValidateManageUserName(string UserName)
        {
            var validateUserNameForms = new ValidateUserNameRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserName = UserName,
            };

            StringContent validateUserNameContent = new StringContent(JsonConvert.SerializeObject(validateUserNameForms), Encoding.UTF8, "application/json");

            var validateUserNameResponse = await _requestService.CommonRequestService(validateUserNameContent, WebApiUrl.validateUserName);

            JObject validateUserNameObj = JObject.Parse(JsonConvert.DeserializeObject(validateUserNameResponse).ToString());
            var validateUserNameJarr = validateUserNameObj["Data"].Value<JArray>();
            List<SuccessResponse> validateUserNameLst = validateUserNameJarr.ToObject<List<SuccessResponse>>();

            return validateUserNameLst[0].Status.ToString();
        }
        public async Task<List<SuccessResponse>> UpdateUser([FromBody] AddNewUserReq entity)
        {
            var forms = new AddNewUserReq
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserName = entity.UserName,
                Email = entity.Email,
                UserRole = entity.UserRole,
                TypeManageUsersAddUserRoleWithStatusFlag = entity.TypeManageUsersAddUserRoleWithStatusFlag,
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                StateId = entity.StateId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.updateuserrolelocation);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> responseMsg = jarr.ToObject<List<SuccessResponse>>();
            return responseMsg;
        }
        public async Task<ManageNewUserViewModel> GetManageUserForEdit(string UserName)
        {
            var forms = new AddNewUserReq
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserName = UserName
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.manageeditusers);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            ManageNewUserViewModel searchList = obj.ToObject<ManageNewUserViewModel>();
            return searchList;
        }
        public async Task<List<SuccessResponse>> DeleteLocationMapping([FromBody] AddNewUserReq entity)
        {
        
            var forms = new AddNewUserReq
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserName = entity.UserName,
                RoleId = entity.RoleId,
                TypeManageUsersAddUserRole = entity.TypeManageUsersAddUserRole,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
            };  

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.manageusersrolelocationdelete);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res;
        }
        public async Task<List<SuccessResponse>> UserCreationRequest([FromBody] UserCreationReqModel reqEntity)
        {

            var forms = new UserCreationReqModel
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FirstName = reqEntity.FirstName,
                MiddleName = reqEntity.MiddleName,
                LastName  = reqEntity.LastName,
                Email = reqEntity.Email,
                Comments   = reqEntity.Comments,
                UserRole = reqEntity.UserRole,
                TypeUserCreationDetails  = reqEntity.TypeUserCreationDetails,
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.usercreationrequest);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res;
        }
    }
}
