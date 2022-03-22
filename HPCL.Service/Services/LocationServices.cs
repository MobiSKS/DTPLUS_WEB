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
            var forms = new Dictionary<string, string>
            {
                {"useragent", CommonBase.useragent},
                {"userip", CommonBase.userip},
                {"userid", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
            };

            HeadOfficeDetailsResponse lst = new HeadOfficeDetailsResponse();

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getLocationHq);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<HODResData> hodRes = jarr.ToObject<List<HODResData>>();
            lst.data.AddRange(hodRes);
            return lst;


            //if
            //{
            //    int hqId = lst.Select(x => x.HQID).FirstOrDefault();

            //    var requestBody = new UpdateHeadOfficeDetails
            //    {
            //        HQID= hqId,
            //        HQCode= headOfficeDetails.HQCode,
            //        HQName=headOfficeDetails.HQName,
            //        HQShortName=headOfficeDetails.HQShortName,
            //        ModifiedBy= _httpContextAccessor.HttpContext.Session.GetString("UserId"),
            //        UserId=_httpContextAccessor.HttpContext.Session.GetString("UserId"),
            //        UserAgent=CommonBase.useragent,
            //        UserIp=CommonBase.userip
            //    };


            //    StringContent contentUpdate = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            //    var responseUpdate = await _requestService.CommonRequestService(contentUpdate, WebApiUrl.updateHeadOffice);

            //    JObject objUpdate = JObject.Parse(JsonConvert.DeserializeObject(responseUpdate).ToString());

            //    if (obj["Message"].Value<string>() == "Success")
            //    {
            //        var jarrUpdate = objUpdate["Data"].Value<JArray>();
            //        List<SuccessResponse> updateResponse = jarrUpdate.ToObject<List<SuccessResponse>>();
            //        return updateResponse[0].Reason;
            //    }
            //}
            //return "Isuue with the request";
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
    }
}
