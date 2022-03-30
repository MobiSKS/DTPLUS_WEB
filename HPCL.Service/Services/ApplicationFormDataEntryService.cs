using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.ApplicationFormDataEntry;
using HPCL.Common.Models.ViewModel.ApplicationFormDataEntry;
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
    public class ApplicationFormDataEntryService : IApplicationFormDataEntryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public ApplicationFormDataEntryService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor=httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<GetCustomerNameResponse> GetCustomerName(string customerId)
        {
            var searchBody = new GetCustomerName
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = customerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCustomerNameUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            GetCustomerNameResponse searchList = obj.ToObject<GetCustomerNameResponse>();

            return searchList;
        }

        public async Task<List<SuccessResponse>> CheckAddOnForm(string formNumber)
        {
            var searchBody = new CheckAddOnForm
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FormNumber = formNumber
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.CheckAddOnFormUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var updateRes = obj["Data"].Value<JArray>();
            List<SuccessResponse> updateResponse = updateRes.ToObject<List<SuccessResponse>>();
            return updateResponse;
        }
    }
}
