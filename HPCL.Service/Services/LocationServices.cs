using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Locations;
using HPCL.Common.Models.ViewModel.Locations;
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
    public class LocationServices : ILocationServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public LocationServices(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor=httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<HeadOfficeDetailsResponse> HeadOfficeDetails()
        {
            var forms = new BaseEntity
            {
                UserAgent=CommonBase.useragent,
                UserIp=CommonBase.userip,
                UserId=_httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            HeadOfficeDetailsResponse lst = new HeadOfficeDetailsResponse();

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getLocationHq);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<HODResData> hodRes = jarr.ToObject<List<HODResData>>();
            lst.data.AddRange(hodRes);
            return lst;
        }

        public async Task<string> UpdateHod(HeadOfficeDetailsResponse entity)
        {
            var requestBody = new UpdateHeadOfficeDetails
            {
                UserAgent=CommonBase.useragent,
                UserIp=CommonBase.userip,
                UserId=_httpContextAccessor.HttpContext.Session.GetString("UserId"),
                HQID= entity.data[0].HQID,
                HQCode= entity.data[0].HQCode,
                HQName=entity.data[0].HQName,
                HQShortName=entity.data[0].HQShortName,
                ModifiedBy= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
            };

            StringContent contentUpdate = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var responseUpdate = await _requestService.CommonRequestService(contentUpdate, WebApiUrl.updateHeadOffice);

            JObject objUpdate = JObject.Parse(JsonConvert.DeserializeObject(responseUpdate).ToString());

            var jarrUpdate = objUpdate["Data"].Value<JArray>();
            List<SuccessResponse> updateResponse = jarrUpdate.ToObject<List<SuccessResponse>>();
            return updateResponse[0].Reason;
        }

        public async Task<string> DeleteCity(int cityId)
        {
            var requestData = new DeleteCItyReq()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CityID = cityId,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.DeleteCityUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res[0].Reason;
        }

        public async Task<string> DeleteZonalOffice(int zonalOfficeID)
        {
            var requestData = new DeleteZonalOfficeReq()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ZonalOfficeID = zonalOfficeID,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.DeleteZonalOfficeUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res[0].Reason;
        }

        public async Task<string> DeleteRegionalOffice(int regionalOfficeID)
        {
            var requestData = new DeleteRegionalOfficeReq()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                RegionalOfficeID = regionalOfficeID,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.DeleteRegionalOfficeUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res[0].Reason;
        }

        public async Task<string> DeleteCountryRegion(int regionID)
        {
            var requestData = new DeleteCountryRegionReq()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                RegionID = regionID,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.DeleteCountryRegionUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res[0].Reason;
        }

        public async Task<string> DeleteState(int stateID)
        {
            var requestData = new DeleteStateReq()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                StateID = stateID,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.DeleteStateUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res[0].Reason;
        }

        public async Task<string> DeleteDistrict(int districtID)
        {
            var requestData = new DeleteDistrictReq()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                DistrictID = districtID,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.DeleteDistrictUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res[0].Reason;
        }
    }
}
