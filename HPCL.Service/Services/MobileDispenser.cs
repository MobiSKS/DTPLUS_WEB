using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
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
                Status = "1"
            };
            StringContent StatusMobileDispenserContent = new StringContent(JsonConvert.SerializeObject(StatusMobileDispenserRetailOutlet), Encoding.UTF8, "application/json");

            var StatusMobileDispenserContentResponse = await _requestService.CommonRequestService(StatusMobileDispenserContent, WebApiUrl.StatusMobileDispenserRetailOutletMapping);

            JObject StatusmobiledispenserObj = JObject.Parse(JsonConvert.DeserializeObject(StatusMobileDispenserContentResponse).ToString());
            var StatusmobiledispenserJarr = StatusmobiledispenserObj["Data"].Value<JArray>();
            List<GetStatusMobileDispenserRetailOutletMappingResponse> mobiledispenserApprovalLst = StatusmobiledispenserJarr.ToObject<List<GetStatusMobileDispenserRetailOutletMappingResponse>>();
            return mobiledispenserApprovalLst;

        }

        public async Task<List<MobileDispenserRetailOutletMappingResponse>> MobileDispenserRetailOutletMapping()
        
        
        
        
        
        {
            var MobileDispenserRetailOutlet = new MobileDispenserRetailOutletMappingRequest()
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                MobileDispenserId = "", //"3411120001",
                Status = ""
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







        //public async Task<List<MobileDispenserRetailOutletMappingResponse>> SearchMobileDispenserRetailOutletMapping(string merchantId, string status)
        //{
        //    var MobileDispenserRetailOutlet = new MobileDispenserRetailOutletMappingRequest()
        //    {
        //        UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
        //        UserAgent = CommonBase.useragent,
        //        UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
        //        MobileDispenserId = "3411120002",//MobileDispenserID,"3411120001",
        //        Status = status
        //    };
        //    StringContent MobileDispenserContent = new StringContent(JsonConvert.SerializeObject(MobileDispenserRetailOutlet), Encoding.UTF8, "application/json");

        //    var MobileDispenserContentResponse = await _requestService.CommonRequestService(MobileDispenserContent, WebApiUrl.MobileDispenserRetailOutletMapping);

        //    JObject mobiledispenserObj = JObject.Parse(JsonConvert.DeserializeObject(MobileDispenserContentResponse).ToString());
        //    //mobiledispenser = JsonConvert.DeserializeObject<MobileDispenserRetailOutletMappingRequest>(MobileDispenserContentResponse);
        //    var mobiledispenserJarr = mobiledispenserObj["Data"].Value<JArray>();
        //    List<MobileDispenserRetailOutletMappingResponse> mobiledispenserApprovalLst = mobiledispenserJarr.ToObject<List<MobileDispenserRetailOutletMappingResponse>>();
        //    // mobiledispenser.MobileDispenserContentResponse.AddRange(mobiledispenserApprovalLst);
        //    return mobiledispenserApprovalLst;

        //}

     public async   Task<List<GetStatusMobileDispenserResponse>> GetStatusMobileDispenser()
        
        {
            var StatusMobileDispenser = new GetStatusMobileDispenserRequest()
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),

            };
            StringContent StatusMobileDispenserContent = new StringContent(JsonConvert.SerializeObject(StatusMobileDispenser), Encoding.UTF8, "application/json");

            var StatusMobileDispenserContentRespons = await _requestService.CommonRequestService(StatusMobileDispenserContent, WebApiUrl.StatusMobileDispenser);

            JObject StatusmobiledispenserObj = JObject.Parse(JsonConvert.DeserializeObject(StatusMobileDispenserContentRespons).ToString());
            var StatusmobiledispenserJarr = StatusmobiledispenserObj["Data"].Value<JArray>();
            List<GetStatusMobileDispenserResponse> mobiledispenserApprovalLst = StatusmobiledispenserJarr.ToObject<List<GetStatusMobileDispenserResponse>>();
            return mobiledispenserApprovalLst;
        }

        public async Task<List<MobileDispenserRetailOutletMappingResponse>> SearchMobileDispenserRetailOutletMapping(string merchantId, string status)
        {
            var MobileDispenserRetailOutlet = new MobileDispenserRetailOutletMappingRequest()
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                MobileDispenserId = merchantId,
                Status = status
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

        public async Task<Tuple<string, string>> CreateMobileDispenserRetailOutletMapping(InsertMobileDispenserRetailOutletMappingRequest merchantMdl)
        {
            string url;

            url = WebApiUrl.InsertMobileDispenserRetailOutletMapping;
            var MobileDispenserContentForms = new InsertMobileDispenserRetailOutletMappingRequest();
            MobileDispenserContentForms = new InsertMobileDispenserRetailOutletMappingRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                MobileDispenserId = merchantMdl.MobileDispenserId,
                RetailOutletsId = merchantMdl.RetailOutletsId,
               
            
               

               // CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),

            };

            StringContent MobileDispenserContent = new StringContent(JsonConvert.SerializeObject(MobileDispenserContentForms), Encoding.UTF8, "application/json");
            var MobileDispenserResponse = await _requestService.CommonRequestService(MobileDispenserContent, url);
            JObject CreateMobileDispenserObj = JObject.Parse(JsonConvert.DeserializeObject(MobileDispenserResponse).ToString());

            if (CreateMobileDispenserObj["Status_Code"].ToString() == "200")
            {
                var CreateMobileDispenserJarr = CreateMobileDispenserObj["Data"].Value<JArray>();
                List<SuccessResponse> CreateMobileDispenserLst = CreateMobileDispenserJarr.ToObject<List<SuccessResponse>>();
                return Tuple.Create(CreateMobileDispenserLst.First().Status.ToString(), CreateMobileDispenserLst.First().Reason.ToString());
            }
            else
            {
                return Tuple.Create("0", CreateMobileDispenserObj["Message"].ToString());
            }
        }
    }
}
