using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.AshokLayland;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.AshokLeyLand;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class AshokLeyLandService : IAshokLeyLandService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;

        public AshokLeyLandService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }

        public async Task<SearchAlResult> SearchDealer(string dealerCode, string dtpCode)
        {
            var searchBody = new SearchDealer
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                DealerCode = dealerCode,
                DTPDealerCode = dtpCode
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getDelerNameUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            SearchAlResult searchList = obj.ToObject<SearchAlResult>();

            return searchList;
        }

        public async Task<InsertResponse> InsertAlEnroll(string str)
        {
            JArray objs = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            List<AlEnrollment> arrs = objs.ToObject<List<AlEnrollment>>();

            var insertServiceBody = new AlEnrollment
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                DealerCode = arrs[0].DealerCode,
                DealerName = arrs[0].DealerName,
                ZonalOfficeId = arrs[0].ZonalOfficeId,
                RegionalOfficeId = arrs[0].RegionalOfficeId,
                Address1 = arrs[0].Address1,
                Address2 = arrs[0].Address2,
                Address3 = arrs[0].Address3,
                StateId = arrs[0].StateId,
                CityName = arrs[0].CityName,
                DistrictId = arrs[0].DistrictId,
                Pin = arrs[0].Pin,
                MobileNo = arrs[0].MobileNo,
                EmailId = arrs[0].EmailId,
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(insertServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.InsertAlDetailsUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }

        public async Task<InsertResponse> AlEnrollUpdate(string getAllData)
        {

            JArray objs= JArray.Parse(JsonConvert.DeserializeObject(getAllData).ToString());
            List<AlEnrollment> arrs = objs.ToObject<List<AlEnrollment>>();

            var insertServiceBody = new AlEnrollment
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                DealerCode = arrs[0].DealerCode,
                ZonalOfficeId = arrs[0].ZonalOfficeId,
                RegionalOfficeId = arrs[0].RegionalOfficeId,
                Address1 =  arrs[0].Address1,
                Address2 =  arrs[0].Address2,
                Address3 =  arrs[0].Address3,
                StateId =  arrs[0].StateId,
                CityName =  arrs[0].CityName,
                DistrictId =  arrs[0].DistrictId,
                Pin =  arrs[0].Pin,
                MobileNo =  arrs[0].MobileNo,
                EmailId =  arrs[0].EmailId,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(insertServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.UpdateAlDetailsUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            InsertResponse result = obj.ToObject<InsertResponse>();
            return result;
        }

        public async Task<ALOTCCardRequestModel> DealerOTCCardRequest()
        {
            ALOTCCardRequestModel alOTCCardRequestModel = new ALOTCCardRequestModel();
            alOTCCardRequestModel.Remarks = "";
            return alOTCCardRequestModel;
        }
        public async Task<ALOTCCardRequestModel> DealerOTCCardRequest(ALOTCCardRequestModel alOTCCardRequestModel)
        {
            alOTCCardRequestModel.UserAgent = CommonBase.useragent;
            alOTCCardRequestModel.UserIp = CommonBase.userip;
            alOTCCardRequestModel.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            alOTCCardRequestModel.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserName");

            StringContent content = new StringContent(JsonConvert.SerializeObject(alOTCCardRequestModel), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertDealerWiseAlOtcCardRequest);


            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            alOTCCardRequestModel.Internel_Status_Code = customerResponse.Internel_Status_Code;
            alOTCCardRequestModel.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null)
                    alOTCCardRequestModel.Remarks = customerResponse.Data[0].Reason;
                else
                    alOTCCardRequestModel.Remarks = customerResponse.Message;
            }

            return alOTCCardRequestModel;
        }

        public async Task<AshokLeylandCardCreationModel> CreateMultipleOTCCard()
        {
            AshokLeylandCardCreationModel ashokLeylandCardCreationModel = new AshokLeylandCardCreationModel();
            ashokLeylandCardCreationModel.Remarks = "";
            ashokLeylandCardCreationModel.CustomerStateMdl.AddRange(await _commonActionService.GetCustStateList());
            ashokLeylandCardCreationModel.VehicleTypeMdl.AddRange(await _commonActionService.GetVehicleTypeDropdown());
            return ashokLeylandCardCreationModel;
        }

    }
}
