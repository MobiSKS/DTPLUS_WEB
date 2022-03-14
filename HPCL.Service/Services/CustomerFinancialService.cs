using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.CustomerFinancial;
using HPCL.Common.Models.ViewModel.CustomerFinancial;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class CustomerFinancialService : ICustomerFinancialService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public CustomerFinancialService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<CardToCCMSBalanceTransferSearchResponse> SearchCardToCCMSTransfer(BalanceTransferSearchModel entity)
        {
            var reqBody = new BalanceTransferSearchModel();

            if (entity.CustomerID != null)
            {
                reqBody = new BalanceTransferSearchModel
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerID = entity.CustomerID,
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                reqBody = new BalanceTransferSearchModel
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerID = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCardToCCMSTransferUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            CardToCCMSBalanceTransferSearchResponse searchList = obj.ToObject<CardToCCMSBalanceTransferSearchResponse>();
            return searchList;
        }

        public async Task<CCMSToCardBalanceTransferSearchResponse> SearchCCMSToCardTransfer(BalanceTransferSearchModel entity)
        {
            var reqBody = new BalanceTransferSearchModel();

            if (entity.CustomerID != null)
            {
                reqBody = new BalanceTransferSearchModel
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerID = entity.CustomerID,
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                reqBody = new BalanceTransferSearchModel
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerID = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCCMSToCardTransferUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            CCMSToCardBalanceTransferSearchResponse searchList = obj.ToObject<CCMSToCardBalanceTransferSearchResponse>();
            return searchList;
        }
    }
}
