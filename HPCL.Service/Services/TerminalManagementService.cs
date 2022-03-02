using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.TerminalManagementResponse;
using HPCL.Common.Models.ViewModel;
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
   public class TerminalManagementService :ITerminalManagement
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private bool flag;

        public TerminalManagementService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<Tuple<List<ObjMerchantDetail>, List<ObjTerminalDetail>>> TerminalInstallationRequest(TerminalManagement entity)
        {
            var searchBody = new TerminalManagement();

            if (entity.MerchantId != null)
            {
                searchBody = new TerminalManagement
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    MerchantId = entity.MerchantId,
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new TerminalManagement
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    MerchantId = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.TerminalInstallationRequestUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var searchRes = obj["Data"].Value<JObject>();
            var ObjMerchant = searchRes["ObjMerchantDetail"].Value<JArray>();
            var ObjTerminal = searchRes["ObjTerminalDetail"].Value<JArray>();

            List<ObjMerchantDetail> objMerchantList = ObjMerchant.ToObject<List<ObjMerchantDetail>>();
            List<ObjTerminalDetail> searchList = ObjTerminal.ToObject<List<ObjTerminalDetail>>();
            return Tuple.Create(objMerchantList,searchList);
        }

        public async Task<string> AddJustification(TerminalManagement entity)
        {
            var updateServiceBody = new TerminalManagement
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                MerchantId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                TerminalTypeRequested= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                TerminalIssuanceType= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                Flag = Convert.ToInt32(flag),
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                Justification= _httpContextAccessor.HttpContext.Session.GetString("UserId")

            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(updateServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.InsertInstallationRequestUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var updateRes = obj["Data"].Value<JArray>();
            List<SuccessResponse> updateResponse = updateRes.ToObject<List<SuccessResponse>>();
            return updateResponse[0].Reason;
        }
    }
}
