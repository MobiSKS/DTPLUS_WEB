using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.ManageRbe;
using HPCL.Common.Models.ViewModel.ManageRbe;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class ManageRbeService : IManageRbeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public ManageRbeService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor=httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<ChangeRbeMappingResponse> ChangeRbeMapping(RbeMapping entity)
        {
            var reqBody = new RbeMapping
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FirstName = entity.FirstName,
                UserName = entity.UserName
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetMangeRbeMappingUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            ChangeRbeMappingResponse searchList = obj.ToObject<ChangeRbeMappingResponse>();
            return searchList;
        }

        public async Task<RbeUserListResponse> RbeUserList(RbeUserList entity)
        {
            var reqBody = new RbeUserList
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FirstName = entity.FirstName,
                UserName = entity.UserName
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetRbeUserListUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            RbeUserListResponse searchList = obj.ToObject<RbeUserListResponse>();
            return searchList;
        }

        public async Task<RbeDeviceIdResetRequestResponse> RbeDeviceIdResetRequestService(RbeDeviceIdResetRequest entity)
        {
            var reqBody = new RbeDeviceIdResetRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FirstName = entity.FirstName ?? "",
                MobileNo = entity.MobileNo ?? ""
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetRbeDeviceIdResetRequest);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            RbeDeviceIdResetRequestResponse searchList = obj.ToObject<RbeDeviceIdResetRequestResponse>();
            return searchList;
        }

        public async Task<ChangeRbeMappingByUserNameResponse> ChangeRbeMappingByUserName(string newUserName, string userName)
        {
            var reqBody = new ChangeRbeMapping
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                PreRBEUserName = userName,
                NewRBEUserName = newUserName ?? "",
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ChangeRbeMappingByUserNameUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            ChangeRbeMappingByUserNameResponse changeList = obj.ToObject<ChangeRbeMappingByUserNameResponse>();
            return changeList;
        }

        public async Task<List<SuccessResponse>> UserNameVerifyOtp(string newUserName, string userName, string otp)
        {
            var reqBody = new UserNameVerifyOtpResponse
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                PreRBEUserName = userName ?? "",
                NewRBEUserName = newUserName ?? "",
                OTP = otp,
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UserNameVerifyOtpUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var successRes = obj["Data"].Value<JArray>();
            List<SuccessResponse> resMsg = successRes.ToObject<List<SuccessResponse>>();
            return resMsg;
        }

        public async Task<GetApproveChnagedRbeMappingResponse> ApproveChangedRbeMapping(GetApproveChangedRbeMapping entity)
        {
            var reqBody = new GetApproveChangedRbeMapping
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                MappingStatus = entity.MappingStatus ?? "",
                FirstName = entity.FirstName ?? "",
                MobileNo = entity.MobileNo
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetApproveChangedRbeMappingUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetApproveChnagedRbeMappingResponse searchList = obj.ToObject<GetApproveChnagedRbeMappingResponse>();
            return searchList;
        }

        public async Task<List<ApproveRejectChangedRbeMappingResponse>> ApproveRejectChangedRbeMappingSerivce(string userName, string actionPress)
        {
            var reqBody = new ApproveRejectChangedRbeMapping
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                PreRBEUserName = userName ?? "",
                MappingStatus = actionPress ?? ""
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ApproveRejectChangedRbeMappingUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var successRes = obj["Data"].Value<JArray>();
            List<ApproveRejectChangedRbeMappingResponse> resp = successRes.ToObject<List<ApproveRejectChangedRbeMappingResponse>>();
            return resp;
        }

        public async Task<RbeMobileChangeResponse> RbeMobileChangeRequestService(RbeMobileChange entity)
        {
            var reqBody = new RbeMobileChange
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FirstName = entity.FirstName ?? "",
                MobileNo = entity.MobileNo ?? ""
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.RbeMobileChangeRequestUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            RbeMobileChangeResponse searchList = obj.ToObject<RbeMobileChangeResponse>();
            return searchList;
        }

        public async Task<GetSendOtpChangeRbeMobileResponse> GetOtpMobileChangeReqService(string newMobileNo)
        {
            var reqBody = new GetSendOtpChangeRbeMobile
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                NewMobileNo = newMobileNo ?? "",
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetOtpMobileChangeReqUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetSendOtpChangeRbeMobileResponse resp = obj.ToObject<GetSendOtpChangeRbeMobileResponse>();
            return resp;
        }

        public async Task<List<SuccessResponse>> VerifyOtpMobileChangeReqService(string existMobNo, string newMobileNo, string otp)
        {
            var reqBody = new VerifyManageMobileOtp
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                ExistingMobileNo = existMobNo ?? "",
                NewMobileNo = newMobileNo ?? "",
                OTP = otp ?? "",
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.VerifyOtpMobileChangeReqUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var successRes = obj["Data"].Value<JArray>();
            List<SuccessResponse> resp = successRes.ToObject<List<SuccessResponse>>();
            return resp;
        }

        public async Task<GetApproveChangeRbeMobileResponse> ApproveChangeRbeMobileService(GetApproveChangeRbeMobile entity)
        {
            var reqBody = new GetApproveChangeRbeMobile
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                ApprovalStatus = entity.ApprovalStatus ?? "",
                FirstName = entity.FirstName ?? "",
                RBEId = entity.RBEId ?? ""
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ApproveChangeRbeMobileUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetApproveChangeRbeMobileResponse searchList = obj.ToObject<GetApproveChangeRbeMobileResponse>();
            return searchList;
        }

        public async Task<List<ApproveRejectChangedRbeMappingResponse>> ApproveRejectChangedRbeMobileService(string existMobNo, string mappingStatus)
        {
            var reqBody = new ApproveRejectChangedRbeMobile
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                ExistingMobile = existMobNo ?? "",
                MappingStatus = mappingStatus ?? ""
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ApproveRejectChangedRbeMobileUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var successRes = obj["Data"].Value<JArray>();
            List<ApproveRejectChangedRbeMappingResponse> resp = successRes.ToObject<List<ApproveRejectChangedRbeMappingResponse>>();
            return resp;
        }

        public async Task<GetDeviceIdResetRequestRespose> GetDeviceIdResetRequestSerivce(GetDeviceIdResetRequest entity)
        {
            var reqBody = new GetDeviceIdResetRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                FirstName = entity.FirstName ?? "",
                MobileNo = entity.MobileNo ?? ""
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetDeviceIdResetRequestUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetDeviceIdResetRequestRespose resp = obj.ToObject<GetDeviceIdResetRequestRespose>();
            return resp;
        }

        public async Task<GetSendOtpChangeRbeMobileResponse> GetOtpRbeDeviceResetService(string mobNo)
        {
            var reqBody = new GetOtpRbeDeviceReset
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                MobileNo = mobNo ?? "",
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetOtpRbeDeviceResetUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetSendOtpChangeRbeMobileResponse resp = obj.ToObject<GetSendOtpChangeRbeMobileResponse>();
            return resp;
        }

        public async Task<List<SuccessResponse>> ValidateOtpRbeDeviceResetService(string MobileNo, string otp)
        {
            var reqBody = new ValidateOtpRbeDeviceReset
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                MobileNo = MobileNo ?? "",
                OTP = otp ?? "",
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ValidateOtpRbeDeviceResetUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var successRes = obj["Data"].Value<JArray>();
            List<SuccessResponse> resp = successRes.ToObject<List<SuccessResponse>>();
            return resp;
        }

        public async Task<GetApproveChangeRbeDeviceResetResponse> ApproveRbeDeviceResetRequestSerivce(GetApproveChangeRbeDeviceReset entity)
        {
            var reqBody = new GetApproveChangeRbeDeviceReset
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                ApprovalStatus = entity.ApprovalStatus ?? "",
                FirstName = entity.FirstName ?? "",
                RBEId = entity.RBEId ?? ""
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetApproveDeviceIdResetRequestUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetApproveChangeRbeDeviceResetResponse resp = obj.ToObject<GetApproveChangeRbeDeviceResetResponse>();
            return resp;
        }

        public async Task<List<ApproveRejectChangedRbeMappingResponse>> GetApproveRejectRbeDeviceResetService(string mobNo, string mappingStatus)
        {
            var reqBody = new GetApproveRejectRbeDeviceReset
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                MobileNo = mobNo ?? "",
                MappingStatus = mappingStatus ?? ""
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetApproveRejectDeviceResetUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var successRes = obj["Data"].Value<JArray>();
            List<ApproveRejectChangedRbeMappingResponse> resp = successRes.ToObject<List<ApproveRejectChangedRbeMappingResponse>>();
            return resp;
        }
    }
}
