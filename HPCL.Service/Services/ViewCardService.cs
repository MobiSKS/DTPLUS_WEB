using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.ViewCard;
using HPCL.Common.Models.ViewModel.ViewCard;
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
    class ViewCardService : IViewCardService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public ViewCardService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<List<ViewCardSearchResult>> ViewCardSearch(ViewCardDetails entity)
        {
            var searchBody = new ViewCardDetails();

            if (entity.Customerid != null)
            {
                searchBody = new ViewCardDetails
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    Customerid = entity.Customerid,
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new ViewCardDetails
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    Customerid = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ViewCardLimitsUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<ViewCardSearchResult> searchList = jarr.ToObject<List<ViewCardSearchResult>>();
            return searchList;
        }


        public async Task<List<ViewCardSearchResult>> SearchCardMapping(string Customerid)
        {
            var searchBody = new ViewCardDetails();

            if (Customerid != null)
            {
                searchBody = new ViewCardDetails
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    Customerid = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                  
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new ViewCardDetails
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    Customerid = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.SearchCardMappingUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<ViewCardSearchResult> searchList = jarr.ToObject<List<ViewCardSearchResult>>();
            return searchList;
        }
        public async Task<string> UpdateCards(ObjUpdateMobileandFastagNoInCard[] limitArray)
        {
            var updateServiceBody = new UpdateMobileRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ObjUpdateMobileandFastagNoInCard = limitArray,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(updateServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.MobileAddOrEditUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var updateRes = obj["Data"].Value<JArray>();
            List<SuccessResponse> updateResponse = updateRes.ToObject<List<SuccessResponse>>();
            return updateResponse[0].Reason;
        }



    }
}
