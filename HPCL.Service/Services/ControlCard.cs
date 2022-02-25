using HPCL.Common.Models.ViewModel.Cards;
using HPCL.Common.Models.ResponseModel.Cards;
using HPCL.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Helper;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HPCL.Common.Models.CommonEntity;
using System.Linq;

namespace HPCL.Service.Services
{
    public class ControlCard : IControlCardSearch
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public ControlCard(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<List<ControlCardSearchResponse>> ControlCardSearch(ControlCardRequest entity)
        {
            var searchBody = new ControlCardRequest();
            if (entity.CustomerId != null)
            {
                searchBody = new ControlCardRequest
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = entity.CustomerId,

                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new ControlCardRequest
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerId = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
            }
            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.CardControlUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<ControlCardSearchResponse> searchList = jarr.ToObject<List<ControlCardSearchResponse>>();
            return searchList;
        }
    }
}
