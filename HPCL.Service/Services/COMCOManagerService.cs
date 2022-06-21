using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.COMCOManager;
using HPCL.Common.Models.ViewModel.COMCOManager;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class COMCOManagerService: ICOMCOManagerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        private readonly IConfiguration _configuration;

        public COMCOManagerService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
            _configuration = configuration;
        }

        public async Task<COMCOCustomerMappingViewModel> COMCOCustomerMapping(COMCOCustomerMappingViewModel Model)
        {
            if (!string.IsNullOrEmpty(Model.MerchantID))
            {
                var reqBody = new GetCOMCOMapCustomerDetailsRequest
                {
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    CustomerID = string.IsNullOrEmpty(Model.CustomerID) ? "" : Model.CustomerID,
                    MerchantID = string.IsNullOrEmpty(Model.MerchantID) ? "" : Model.MerchantID
                };

                StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
                var response = await _requestService.CommonRequestService(content, WebApiUrl.getcomcomapcustomerdetails);

                JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
                COMCOCustomerMappingViewModel res = obj.ToObject<COMCOCustomerMappingViewModel>();

                if (res != null && res.Data != null)
                {
                    Model.Data = res.Data;
                }

                Model.Message = res.Message;
                Model.Internel_Status_Code = res.Internel_Status_Code;
                if (res != null && res.Data != null && res.Data.MerchantDetails.Count > 0 && res.Data.MerchantDetails[0].Status == 1)
                {
                    Model.RegionalOfficeName = res.Data.MerchantDetails[0].RegionalOfficeName;
                    Model.ZonalOfficeName = res.Data.MerchantDetails[0].ZonalOfficeName;
                    Model.RetailOutletName = res.Data.MerchantDetails[0].RetailOutletName;
                }
                else if (res != null && res.Data != null && res.Data.MerchantDetails.Count > 0 && res.Data.MerchantDetails[0].Status == 0)
                {
                    Model.RegionalOfficeName = "";
                    Model.ZonalOfficeName = "";
                    Model.RetailOutletName = "";
                    Model.Message = res.Data.MerchantDetails[0].Reason;
                    Model.Data = new GetCOMCOMapCustomerDetails();
                }
            }
            else
            {
                Model.MerchantID = "";
            }

            return Model;
        }

    }
}
