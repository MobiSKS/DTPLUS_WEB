using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.ManageRbe;
using HPCL.Common.Models.ViewModel.ManageRbe;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                UserIp = CommonBase.userip,
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
                UserIp = CommonBase.userip,
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
                UserIp = CommonBase.userip,
                FirstName = entity.FirstName ?? "",
                MobileNo = entity.MobileNo ?? ""
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetRbeDeviceIdResetRequest);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            RbeDeviceIdResetRequestResponse searchList = obj.ToObject<RbeDeviceIdResetRequestResponse>();
            return searchList;
        }
    }
}
