using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class MyHpOTCCardCustomerService : IMyHpOTCCardCustomerService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public MyHpOTCCardCustomerService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<RequestForOTCCardModel> RequestForOTCCard()
        {
            RequestForOTCCardModel custMdl = new RequestForOTCCardModel();


            //Fetching Customer Region
            var CustomerRegion = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                    {"ZoneID",  "0" }
                };


            StringContent content = new StringContent(JsonConvert.SerializeObject(CustomerRegion), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getLocationRegion);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<RegionModel> lst = jarr.ToObject<List<RegionModel>>();
            custMdl.RegionMdl.AddRange(lst);


            return custMdl;
        }

        public async Task<RequestForOTCCardModel> RequestForOTCCard(RequestForOTCCardModel requestForOTCCardModel)
        {
            var driversRequestData = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"UserId", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                    {"RegionalId", requestForOTCCardModel.CustomerRegionID.ToString()},
                    {"NoofCards", requestForOTCCardModel.NoofCards.ToString()},
                    {"CreatedBy", _httpContextAccessor.HttpContext.Session.GetString("UserName")}
                };

            StringContent content = new StringContent(JsonConvert.SerializeObject(driversRequestData), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertOTCCardRequest);


            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            requestForOTCCardModel.Internel_Status_Code = customerResponse.Internel_Status_Code;
            requestForOTCCardModel.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null)
                    requestForOTCCardModel.Remarks = customerResponse.Data[0].Reason;
                else
                    requestForOTCCardModel.Remarks = customerResponse.Message;


                var CustRegion = new Dictionary<string, string>
                                {
                                    {"Useragent", CommonBase.useragent},
                                    {"Userip", CommonBase.userip},
                                    {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                                    {"ZoneID",  "0" }
                                };

                StringContent contentCustomerRegion = new StringContent(JsonConvert.SerializeObject(CustRegion), Encoding.UTF8, "application/json");

                var responseRegion = await _requestService.CommonRequestService(contentCustomerRegion, WebApiUrl.getLocationRegion);


                JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseRegion).ToString());
                var jarr = obj["Data"].Value<JArray>();
                List<RegionModel> lst = jarr.ToObject<List<RegionModel>>();
                requestForOTCCardModel.RegionMdl.AddRange(lst);
            }

            return requestForOTCCardModel;
        }
    }
}
