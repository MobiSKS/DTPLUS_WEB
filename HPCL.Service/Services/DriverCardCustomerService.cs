using System;
using System.Collections.Generic;
using System.Text;
using HPCL.Service.Interfaces;
using System.Threading.Tasks;
using HPCL.Common.Models.ViewModel.DriverCardCustomer;
using Microsoft.AspNetCore.Http;
using HPCL.Common.Helper;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.CommonEntity;

namespace HPCL.Service.Services
{
    public class DriverCardCustomerService : IDriverCardCustomerService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public DriverCardCustomerService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<RequestForDriverCardModel> RequestForDriverCard()
        {
            RequestForDriverCardModel custMdl = new RequestForDriverCardModel();


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

        public async Task<RequestForDriverCardModel> RequestForDriverCard(RequestForDriverCardModel requestForDriverCardModel)
        {

            var driversRequestData = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"UserId", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                    {"RegionalId", requestForDriverCardModel.CustomerRegionID.ToString()},
                    {"NoofCards", requestForDriverCardModel.NoofCards.ToString()},
                    {"CreatedBy", _httpContextAccessor.HttpContext.Session.GetString("UserName")}
                };

            StringContent content = new StringContent(JsonConvert.SerializeObject(driversRequestData), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertDriverCardRequest);


            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            requestForDriverCardModel.Internel_Status_Code = customerResponse.Internel_Status_Code;
            requestForDriverCardModel.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null)
                    requestForDriverCardModel.Remarks = customerResponse.Data[0].Reason;
                else
                    requestForDriverCardModel.Remarks = customerResponse.Message;


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
                requestForDriverCardModel.RegionMdl.AddRange(lst);
            }

            return requestForDriverCardModel;
        }


    }
}
