using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.MobileDispenser;
using HPCL.Common.Models.ResponseModel.MobileDispenser;
using HPCL.Common.Models.ViewModel.MobileDispenser;
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
    public  class MobileDispenser:IMobileDispenser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly ICommonActionService _commonActionService;
        private readonly IRequestService _requestService;
       // private readonly IConfiguration _configuration;


       

        public MobileDispenser(IHttpContextAccessor httpContextAccessor, IRequestService requestService)
        {
            _httpContextAccessor = httpContextAccessor;
           // _commonActionService = commonActionService;
            _requestService = requestService;
            //_configuration = configuration;
        }

        public async Task<List<GetStatusMobileDispenserRetailOutletMappingResponse>> GetStatusMobileDispenserRetailOutletMapping(string Status)
        {
            var StatusMobileDispenserRetailOutlet = new GetStatusMobileDispenserRetailOutletMappingRequest()
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                Status = "0"
            };
            StringContent StatusMobileDispenserContent = new StringContent(JsonConvert.SerializeObject(StatusMobileDispenserRetailOutlet), Encoding.UTF8, "application/json");

            var StatusMobileDispenserContentResponse = await _requestService.CommonRequestService(StatusMobileDispenserContent, WebApiUrl.StatusMobileDispenserRetailOutletMapping);

            JObject StatusmobiledispenserObj = JObject.Parse(JsonConvert.DeserializeObject(StatusMobileDispenserContentResponse).ToString());
            var StatusmobiledispenserJarr = StatusmobiledispenserObj["Data"].Value<JArray>();
            List<GetStatusMobileDispenserRetailOutletMappingResponse> mobiledispenserApprovalLst = StatusmobiledispenserJarr.ToObject<List<GetStatusMobileDispenserRetailOutletMappingResponse>>();
            return mobiledispenserApprovalLst;

        }

        public async Task<List<MobileDispenserRetailOutletMappingResponse>> MobileDispenserRetailOutletMapping(string MobileDispenserID, int Status)
        
        {
            var MobileDispenserRetailOutlet = new MobileDispenserRetailOutletMappingRequest()
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                MobileDispenserId = "3411120001",
                Status = 0
            };
            StringContent MobileDispenserContent = new StringContent(JsonConvert.SerializeObject(MobileDispenserRetailOutlet), Encoding.UTF8, "application/json");

            var MobileDispenserContentResponse = await _requestService.CommonRequestService(MobileDispenserContent, WebApiUrl.MobileDispenserRetailOutletMapping);

            JObject mobiledispenserObj = JObject.Parse(JsonConvert.DeserializeObject(MobileDispenserContentResponse).ToString());
            //mobiledispenser = JsonConvert.DeserializeObject<MobileDispenserRetailOutletMappingRequest>(MobileDispenserContentResponse);
            var mobiledispenserJarr = mobiledispenserObj["Data"].Value<JArray>();
            List<MobileDispenserRetailOutletMappingResponse> mobiledispenserApprovalLst = mobiledispenserJarr.ToObject<List<MobileDispenserRetailOutletMappingResponse>>();
            // mobiledispenser.MobileDispenserContentResponse.AddRange(mobiledispenserApprovalLst);
            return mobiledispenserApprovalLst;

        }

        //public async Task<List<MobileDispenserViewModel>> SearchMobileDispenserRetailOutletMapping(string MobileDispenserId, int Status)
        //{
        //    var MobileDispenserRetailOutlet = new MobileDispenserRetailOutletMappingRequest()
        //    {
        //        UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
        //        UserAgent = CommonBase.useragent,
        //        UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
        //        MobileDispenserId = "3411120001",
        //        Status = 0
        //    };
        //    StringContent MobileDispenserContent = new StringContent(JsonConvert.SerializeObject(MobileDispenserRetailOutlet), Encoding.UTF8, "application/json");

        //    var MobileDispenserContentResponse = await _requestService.CommonRequestService(MobileDispenserContent, WebApiUrl.MobileDispenserRetailOutletMapping);

        //    JObject mobiledispenserObj = JObject.Parse(JsonConvert.DeserializeObject(MobileDispenserContentResponse).ToString());
        //    //mobiledispenser = JsonConvert.DeserializeObject<MobileDispenserRetailOutletMappingRequest>(MobileDispenserContentResponse);
        //    var mobiledispenserJarr = mobiledispenserObj["Data"].Value<JArray>();
        //    List<MobileDispenserViewModel> mobiledispenserApprovalLst = mobiledispenserJarr.ToObject<List<MobileDispenserViewModel>>();
        //    // mobiledispenser.MobileDispenserContentResponse.AddRange(mobiledispenserApprovalLst);
        //    return mobiledispenserApprovalLst;
        //}






        public async Task<List<MobileDispenserRetailOutletMappingResponse>> SearchMobileDispenserRetailOutletMapping(string MobileDispenserID, int Status)
        {
            var MobileDispenserRetailOutlet = new MobileDispenserRetailOutletMappingRequest()
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                MobileDispenserId = "3411120001",
                Status = 0
            };
            StringContent MobileDispenserContent = new StringContent(JsonConvert.SerializeObject(MobileDispenserRetailOutlet), Encoding.UTF8, "application/json");

            var MobileDispenserContentResponse = await _requestService.CommonRequestService(MobileDispenserContent, WebApiUrl.MobileDispenserRetailOutletMapping);

            JObject mobiledispenserObj = JObject.Parse(JsonConvert.DeserializeObject(MobileDispenserContentResponse).ToString());
            //mobiledispenser = JsonConvert.DeserializeObject<MobileDispenserRetailOutletMappingRequest>(MobileDispenserContentResponse);
            var mobiledispenserJarr = mobiledispenserObj["Data"].Value<JArray>();
            List<MobileDispenserRetailOutletMappingResponse> mobiledispenserApprovalLst = mobiledispenserJarr.ToObject<List<MobileDispenserRetailOutletMappingResponse>>();
            // mobiledispenser.MobileDispenserContentResponse.AddRange(mobiledispenserApprovalLst);
            return mobiledispenserApprovalLst;

        }


    }
}
