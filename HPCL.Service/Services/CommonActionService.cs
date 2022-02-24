using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.RequestEnities;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class CommonActionService : ICommonActionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public CommonActionService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<List<DistrictResponseModal>> GetDistrictList(string stateId)
        {
            var officerDistrictForms = new DistrictRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid,
                StateID = stateId
            };

            StringContent officerDistrictContent = new StringContent(JsonConvert.SerializeObject(officerDistrictForms), Encoding.UTF8, "application/json");

            var officerDistrictResponse = await _requestService.CommonRequestService(officerDistrictContent, WebApiUrl.getDistrict);

            JObject officerDistrictObj = JObject.Parse(JsonConvert.DeserializeObject(officerDistrictResponse).ToString());
            var officerDistrictJarr = officerDistrictObj["Data"].Value<JArray>();
            List<DistrictResponseModal> districtLst = officerDistrictJarr.ToObject<List<DistrictResponseModal>>();

            return districtLst;
        }

        public async Task<List<HqResponseModal>> GetHqList()
        {
            var hqForms = new BaseEntity
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid
            };

            StringContent hqContent = new StringContent(JsonConvert.SerializeObject(hqForms), Encoding.UTF8, "application/json");

            var hqResponse = await _requestService.CommonRequestService(hqContent, WebApiUrl.getZonalOffice);

            JObject hqObj = JObject.Parse(JsonConvert.DeserializeObject(hqResponse).ToString());
            var hqJarr = hqObj["Data"].Value<JArray>();
            List<HqResponseModal> hqLst = hqJarr.ToObject<List<HqResponseModal>>();

            return hqLst;
        }

        public async Task<List<LimitTypeModal>> GetLimitType()
        {
            var forms = new Dictionary<string, string>
            {
                {"useragent", CommonBase.useragent},
                {"userip", CommonBase.userip},
                {"userid", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
            };


            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetLimitTypeUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<LimitTypeModal> limitType = jarr.ToObject<List<LimitTypeModal>>();
            var sortedtList = limitType.OrderBy(x => x.LimitId).ToList();
            return sortedtList;
        }

        public async Task<List<LocationMappingResponseModal>> GetLocationMappingList(int officerId)
        {
            var locationMappingForms = new LocationMappingRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                OfficerID = officerId
            };

            StringContent locationMappingContent = new StringContent(JsonConvert.SerializeObject(locationMappingForms), Encoding.UTF8, "application/json");

            var locationMappingResponse = await _requestService.CommonRequestService(locationMappingContent, WebApiUrl.getLocationMapping);

            JObject locationMappingObj = JObject.Parse(JsonConvert.DeserializeObject(locationMappingResponse).ToString());
            var locationMappingJarr = locationMappingObj["Data"].Value<JArray>();
            List<LocationMappingResponseModal> locationMappingLst = locationMappingJarr.ToObject<List<LocationMappingResponseModal>>();

            return locationMappingLst;
        }

        public async Task<List<OfficerTypeResponseModal>> GetOfficerTypeList()
        {
            var officerTypeForms = new BaseEntity
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid
            };

            StringContent officerTypeContent = new StringContent(JsonConvert.SerializeObject(officerTypeForms), Encoding.UTF8, "application/json");

            var officerTypeResponse = await _requestService.CommonRequestService(officerTypeContent, WebApiUrl.getOfficerType);

            JObject officerTypeObj = JObject.Parse(JsonConvert.DeserializeObject(officerTypeResponse).ToString());
            var officerTypeJarr = officerTypeObj["Data"].Value<JArray>();
            List<OfficerTypeResponseModal> officerTypeLst = officerTypeJarr.ToObject<List<OfficerTypeResponseModal>>();

            return officerTypeLst;
        }

        public async Task<List<RegionalOfficeResponseModal>> GetRegionalOfficeList(string zonalOfcID)
        {
            var regionalOfficeForms = new RegionalOfficeRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid,
                ZonalID = zonalOfcID
            };

            StringContent regionalOfficeContent = new StringContent(JsonConvert.SerializeObject(regionalOfficeForms), Encoding.UTF8, "application/json");

            var regionalOfficeResponse = await _requestService.CommonRequestService(regionalOfficeContent, WebApiUrl.getOfficerType);

            JObject regionalOfficeObj = JObject.Parse(JsonConvert.DeserializeObject(regionalOfficeResponse).ToString());
            var regionalOfficeJarr = regionalOfficeObj["Data"].Value<JArray>();
            List<RegionalOfficeResponseModal> regionalOfficeLst = regionalOfficeJarr.ToObject<List<RegionalOfficeResponseModal>>();

            return regionalOfficeLst;
        }

        public async Task<List<StateResponseModal>> GetStateList()
        {

            var officerStateForms = new StateRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid,
                CountryID = "0"
            };

            StringContent officeStateContent = new StringContent(JsonConvert.SerializeObject(officerStateForms), Encoding.UTF8, "application/json");

            var officeStateResponse = await _requestService.CommonRequestService(officeStateContent, WebApiUrl.getState);

            JObject officeStateObj = JObject.Parse(JsonConvert.DeserializeObject(officeStateResponse).ToString());
            var officeStateJarr = officeStateObj["Data"].Value<JArray>();
            List<StateResponseModal> stateLst = officeStateJarr.ToObject<List<StateResponseModal>>();

            return stateLst;
        }

        public async Task<List<ZonalOfficeResponseModal>> GetZonalOfficeList()
        {
            var zonalOfficeForms = new BaseEntity
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid
            };

            StringContent zonalOfficeContent = new StringContent(JsonConvert.SerializeObject(zonalOfficeForms), Encoding.UTF8, "application/json");

            var zonalOfficeResponse = await _requestService.CommonRequestService(zonalOfficeContent, WebApiUrl.getZonalOffice);

            JObject zonalOfficeObj = JObject.Parse(JsonConvert.DeserializeObject(zonalOfficeResponse).ToString());
            var zonalOfficeJarr = zonalOfficeObj["Data"].Value<JArray>();
            List<ZonalOfficeResponseModal> zonalOfficeLst = zonalOfficeJarr.ToObject<List<ZonalOfficeResponseModal>>();

            return zonalOfficeLst;
        }

        public async Task<string> ValidateUserName(string userName)
        {
            var validateUserNameForms = new ValidateUserNameRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid,
                UserName = userName
            };

            StringContent validateUserNameContent = new StringContent(JsonConvert.SerializeObject(validateUserNameForms), Encoding.UTF8, "application/json");

            var validateUserNameResponse = await _requestService.CommonRequestService(validateUserNameContent, WebApiUrl.getZonalOffice);

            JObject validateUserNameObj = JObject.Parse(JsonConvert.DeserializeObject(validateUserNameResponse).ToString());
            var validateUserNameJarr = validateUserNameObj["Data"].Value<JArray>();
            List<SuccessResponse> validateUserNameLst = validateUserNameJarr.ToObject<List<SuccessResponse>>();

            return validateUserNameLst.First().Status.ToString();
        }

        public async Task<List<RegionModel>> GetRegionList()
        {
            var CustomerRegion = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
                {"ZoneID",  "0" }
            };

            StringContent customerRegionContent = new StringContent(JsonConvert.SerializeObject(CustomerRegion), Encoding.UTF8, "application/json");

            var customerRegionResponse = await _requestService.CommonRequestService(customerRegionContent, WebApiUrl.getLocationRegion);

            JObject customerRegionObj = JObject.Parse(JsonConvert.DeserializeObject(customerRegionResponse).ToString());
            var customerRegionJarr = customerRegionObj["Data"].Value<JArray>();
            List<RegionModel> customerRegionLst = customerRegionJarr.ToObject<List<RegionModel>>();
                        
            return customerRegionLst;
        }

    }
}
