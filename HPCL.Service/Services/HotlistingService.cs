using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.Hotlisting;
using HPCL.Common.Models.ResponseModel.Hotlisting;
using HPCL.Common.Models.ViewModel.Hotlisting;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HPCL.Service.Services
{
    public class HotlistingService : IHotlistingService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        public HotlistingService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }
        public async Task<List<string>> ApplyHotlistorReactivate([FromBody] HotlistorReactivateViewModel hotlistorReactivateViewModel)
        {
            var hotlistRequest = new HotlistingRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                EntityTypeId = hotlistorReactivateViewModel.EntityTypeId != "" ? Convert.ToInt32(hotlistorReactivateViewModel.EntityTypeId) : 0,
                EntityIdVal = hotlistorReactivateViewModel.EntityIdVal,
                ActionId = hotlistorReactivateViewModel.ActionId != "" ? Convert.ToInt32(hotlistorReactivateViewModel.ActionId) : 0,
                ReasonId = hotlistorReactivateViewModel.ReasonId != "" ? Convert.ToInt32(hotlistorReactivateViewModel.ReasonId) : 0,
                ReasonDetails = hotlistorReactivateViewModel.ReasonDetails,
                Remarks = hotlistorReactivateViewModel.Remarks,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(hotlistRequest), Encoding.UTF8, "application/json");
            var Response = await _requestService.CommonRequestService(requestContent, WebApiUrl.updatehotlistorreactivate);
            JObject ResponseObj = JObject.Parse(JsonConvert.DeserializeObject(Response).ToString());
            List<string> messageList = new List<string>();
            if (ResponseObj["Status_Code"].ToString() == "200")
            {
                var responseJarr = ResponseObj["Data"].Value<JArray>();
                List<HotlistSuccessResponse> responseList = responseJarr.ToObject<List<HotlistSuccessResponse>>();
                messageList.Add(responseList[0].Status.ToString());
                messageList.Add(responseList[0].ActionName.ToString());
            }
            else
            {
                messageList.Add(ResponseObj["Message"].ToString());
            }
            return messageList;
        }
        public async Task<ViewHotlistingorReactivateResponse> GetHotlistedorReactivatedData(string EntityTypeId, string EntityId)
        {
            ViewHotlistingorReactivateResponse ViewHotlistingorReactivateResponse = new ViewHotlistingorReactivateResponse();
            var Request = new HotlistingRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                EntityTypeId = EntityTypeId != "" ? Convert.ToInt32(EntityTypeId) : 0,
                EntityIdVal = EntityId,
            };
            StringContent ResponseContent = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");
            var Response = await _requestService.CommonRequestService(ResponseContent, WebApiUrl.gethotlistedorreactivateddetails);
            ViewHotlistingorReactivateResponse = JsonConvert.DeserializeObject<ViewHotlistingorReactivateResponse>(Response);
            return ViewHotlistingorReactivateResponse;
        }
        public async Task<GetHotlistApprovalResponse> GetHotlistApproval(string EntityTypeId, string ActionId, string FromDate, string ToDate)
        {
            string fromDate = "", toDate = "";

            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(FromDate))
            {
                fromDate = await _commonActionService.changeDateFormat(FromDate);
                toDate = await _commonActionService.changeDateFormat(ToDate);
            }
            GetHotlistApprovalResponse getHotlistApprovalResponse = new GetHotlistApprovalResponse();
            var Request = new HotlistingRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                EntityTypeId = Convert.ToInt32(EntityTypeId),
                ActionId = Convert.ToInt32(ActionId),
                FromDate = fromDate,
                ToDate = toDate
            };
            StringContent ResponseContent = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");
            var Response = await _requestService.CommonRequestService(ResponseContent, WebApiUrl.gethotlistapproval);
            getHotlistApprovalResponse = JsonConvert.DeserializeObject<GetHotlistApprovalResponse>(Response);
            if (getHotlistApprovalResponse.Data != null)
            {
                foreach (var item in getHotlistApprovalResponse.Data)
                {
                    if (!string.IsNullOrEmpty(item.CreationDate))
                    {
                        string[] subs = item.CreationDate.Split('T');
                        string[] date = subs[0].Split('-');
                        item.CreationDate= date[1] + "-" + date[2] + "-" + date[0];
                    }
                }
            }
            
            return getHotlistApprovalResponse;
        }
        public async Task<List<HotlistSuccessResponse>> UpdateHotlistApproval([FromBody] HotlistApprovalRequest hotlistApprovalRequest)
        {
            var hotlistRequest = new HotlistApprovalRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                EntityTypeId = Convert.ToInt32(hotlistApprovalRequest.EntityTypeId),
                ActionId = Convert.ToInt32(hotlistApprovalRequest.ActionId),
                ObjUpdateHotlistApprovalEntityCode = hotlistApprovalRequest.ObjUpdateHotlistApprovalEntityCode,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(hotlistRequest), Encoding.UTF8, "application/json");
            var Response = await _requestService.CommonRequestService(requestContent, WebApiUrl.updatehotlistapproval);
            JObject ResponseObj = JObject.Parse(JsonConvert.DeserializeObject(Response).ToString());


            var responseJarr = ResponseObj["Data"].Value<JArray>();
            List<HotlistSuccessResponse> responseList = responseJarr.ToObject<List<HotlistSuccessResponse>>();
            return responseList;

        }
        public async Task<List<HotlistSuccessResponse>> CheckEntityAlreadyHotlisted(string EntityTypeId,string EntityId)
        {
            var hotlistRequest = new HotlistingRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                EntityIdVal = EntityId,
                EntityTypeId = EntityTypeId != "" ? Convert.ToInt32(EntityTypeId) : 0,
            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(hotlistRequest), Encoding.UTF8, "application/json");
            var Response = await _requestService.CommonRequestService(requestContent, WebApiUrl.checkentityalreadyhotlisted);
            JObject ResponseObj = JObject.Parse(JsonConvert.DeserializeObject(Response).ToString());
            var responseJarr = ResponseObj["Data"].Value<JArray>();
            List<HotlistSuccessResponse> responseList = responseJarr.ToObject<List<HotlistSuccessResponse>>();
            return responseList;

        }
    }
}
