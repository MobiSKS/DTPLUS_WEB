using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.HDFCBankCreditPouch;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class HDFCBankCreditPouchService : IHDFCBankCreditPouchService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public HDFCBankCreditPouchService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<string> GetCustomerDetails(CustomerDetailsReq entity)
        {
            var searchBody = new CustomerDetailsReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = entity.CustomerId,
                RequestedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };


            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCustomerDetailsUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            //SearchManageCards searchList = obj.ToObject<SearchManageCards>();
            return "";
        }
    }
}
