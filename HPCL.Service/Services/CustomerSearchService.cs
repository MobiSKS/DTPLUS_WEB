using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.CustomerSearch;
using HPCL.Common.Models.ViewModel.CustomerSearch;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class CustomerSearchService : ICustomerSearchService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public CustomerSearchService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<ControlCardPinResetRes> CCPinReset(ControlCardPinResetReq entity)
        {
            var reqBody = new ControlCardPinResetReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID = entity.CustomerID ?? "",
                ControlCardNo = entity.ControlCardNo ?? ""
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.CCPinResetUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            ControlCardPinResetRes searchList = obj.ToObject<ControlCardPinResetRes>();
            return searchList;
        }
    }
}
