using HPCL.Common.Helper;
using HPCL.Common.Models.ViewModel.EFT;
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

    public class EFTService : IEFTService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        private readonly IConfiguration _configuration;
        public EFTService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
            _configuration = configuration;
        }
        public async Task<CCMSRechargeSummaryViewModel> CCMSRechargethroughEFT(CCMSRechargethroughEFTModel entity)
        {
            CCMSRechargeSummaryViewModel responseEntity = new CCMSRechargeSummaryViewModel();
            MultipartFormDataContent form = new MultipartFormDataContent();

           
            form.Add(new StreamContent(entity.TransactionDetailFile.OpenReadStream()), "TransactionDetailFile", entity.TransactionDetailFile.FileName);
            form.Add(new StringContent(_httpContextAccessor.HttpContext.Session.GetString("UserId")), "CreatedBy");
            form.Add(new StringContent(_httpContextAccessor.HttpContext.Session.GetString("UserId")), "Userid");
            form.Add(new StringContent(CommonBase.useragent), "Useragent");
            form.Add(new StringContent(_httpContextAccessor.HttpContext.Session.GetString("IpAddress")), "Userip");

            var response = await _requestService.FormDataRequestService(form, WebApiUrl.eftrechargedetailentry);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            responseEntity = obj.ToObject<CCMSRechargeSummaryViewModel>();
           
            return responseEntity;
        }
    }
}
