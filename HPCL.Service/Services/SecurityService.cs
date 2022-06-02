using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.Security;
using HPCL.Common.Models.ResponseModel.Security;
using HPCL.Common.Models.ViewModel.Security;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
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
                    UserIp = CommonBase.userip,
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
                    UserIp = CommonBase.userip,
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
                UserIp = CommonBase.userip,
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
                UserIp= CommonBase.userip,
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
                UserIp= CommonBase.userip,
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
                UserIp = CommonBase.userip,
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
                UserIp = CommonBase.userip,
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

        public async Task<List<SuccessResponse>> UserResetPassword(string userName)
        {
            var forms = new GetUserResetPassword
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserName = userName,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
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
                UserIp = CommonBase.userip,
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
                UserIp = CommonBase.userip,
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
        public async Task<GetUserRoleLocationResponse> GetUserRoleLocation(string UserName)
        {
            var request = new GetUserRoleLocationRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
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
    }
}
