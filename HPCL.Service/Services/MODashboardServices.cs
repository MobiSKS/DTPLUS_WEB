using System;
using System.Collections.Generic;
using HPCL.Common.Models.ResponseModel.MODashboard;
using HPCL.Common.Models.RequestModel.MODashboard;
using HPCL.Common.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using HPCL.Common.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Helper;
using Microsoft.AspNetCore.Http;
using HPCL.Service.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace HPCL.Service.Services
{
    public class MODashboardServices : IMODashboardService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        public MODashboardServices(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }

        public async Task<List<PendingTerminalResponseModel>> PendingTerminal(string UserName)
        {
            var PendingTerminalDetails = new MODashboard
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserName = UserName
            };

            StringContent PendingTerminalDetailsTableContent = new StringContent(JsonConvert.SerializeObject(PendingTerminalDetails), Encoding.UTF8, "application/json");

            var PendingTerminalResponse = await _requestService.CommonRequestService(PendingTerminalDetailsTableContent, WebApiUrl.PendingTerminal);

            JObject PendingTerminalTableObj = JObject.Parse(JsonConvert.DeserializeObject(PendingTerminalResponse).ToString());
            var PendingTerminalTableJarr = PendingTerminalTableObj["Data"].Value<JArray>();
            List<PendingTerminalResponseModel> PendingTerminalResponseModel = PendingTerminalTableJarr.ToObject<List<PendingTerminalResponseModel>>();

            return PendingTerminalResponseModel;
        }

        public async Task<List<RegionInformationResponseModel>> RegionInformation(string UserName)
        {
            var RegionInformationDetails = new MODashboard
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserName = UserName
            };

            StringContent RegionInformationDetailsTableContent = new StringContent(JsonConvert.SerializeObject(RegionInformationDetails), Encoding.UTF8, "application/json");

            var RegionInformationResponse = await _requestService.CommonRequestService(RegionInformationDetailsTableContent, WebApiUrl.RegionInformation);

            JObject RegionInformationTableObj = JObject.Parse(JsonConvert.DeserializeObject(RegionInformationResponse).ToString());
            var RegionInformationTableJarr = RegionInformationTableObj["Data"].Value<JArray>();
            List<RegionInformationResponseModel> RegionInformationResponseModel = RegionInformationTableJarr.ToObject<List<RegionInformationResponseModel>>();

            return RegionInformationResponseModel;
        }

        public async Task<List<UserInformationResponseModel>> UserInformation(string UserName)
        {
            var userinformationDetails = new MODashboard
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserName = "3010000000"
            };

            StringContent UserInformationDetailsTableContent = new StringContent(JsonConvert.SerializeObject(userinformationDetails), Encoding.UTF8, "application/json");

            var UserInformationResponse = await _requestService.CommonRequestService(UserInformationDetailsTableContent, WebApiUrl.userinformation);

            JObject UserInformationTableObj = JObject.Parse(JsonConvert.DeserializeObject(UserInformationResponse).ToString());
            var UserInformationTableJarr = UserInformationTableObj["Data"].Value<JArray>();
            List<UserInformationResponseModel> UserInformationResponseModel = UserInformationTableJarr.ToObject<List<UserInformationResponseModel>>();

            return UserInformationResponseModel;
        }

    }
}