using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.CustomerFinancial;
using HPCL.Common.Models.ResponseModel.MerchantFinancial;
using HPCL.Common.Models.ViewModel.MerchantFinancials;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class MerchantFinancialService : IMerchantFinancialService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public MerchantFinancialService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<UploadMerchantCautionLimitResponse> ViewUploadMerchantCautionLimit(GetUploadMerchantCautionLimit entity)
        {
            var reqBody = new GetUploadMerchantCautionLimit();

            if (entity.FromDate != null && entity.ToDate != null)
            {
                reqBody = new GetUploadMerchantCautionLimit
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    MerchantId = entity.MerchantId,
                    MerchantStatus = entity.MerchantStatus,
                    FromDate = entity.FromDate,
                    ToDate = entity.ToDate,
                    RegionalOffice = entity.RegionalOffice,
                    SalesArea = entity.SalesArea
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Merchant")
            {
                reqBody = new GetUploadMerchantCautionLimit
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    MerchantId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    MerchantStatus = entity.MerchantStatus,
                    FromDate = entity.FromDate,
                    ToDate = entity.ToDate,
                    RegionalOffice = entity.RegionalOffice,
                    SalesArea = entity.SalesArea
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetUploadMerchantCautionLimitUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            UploadMerchantCautionLimitResponse searchList = obj.ToObject<UploadMerchantCautionLimitResponse>();
            return searchList;
        }
    }
}
