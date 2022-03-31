using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.Interface;
using HPCL.Common.Models.ViewModel.Interface;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class InterfaceService:IInterfaceService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public InterfaceService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }
        
        public async Task<GetCustomerandCardFormResponse> GetCustomerandCardFormDetails(string EntityId, string FormNumber, string StateID, string CityName)
        {
            GetCustomerandCardFormResponse customerandCardFormResponse = new GetCustomerandCardFormResponse();
            var searchRequest = new SearchCustomerandCardFormModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                EntityId= EntityId,
                FormNumber=FormNumber,
                StateID=StateID!="0"? StateID:"",
                CityName= CityName

            };

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(searchRequest), Encoding.UTF8, "application/json");
            var Response = await _requestService.CommonRequestService(requestContent, WebApiUrl.searchcustomerandcardform);
            JObject ResponseObj = JObject.Parse(JsonConvert.DeserializeObject(Response).ToString());

            customerandCardFormResponse = JsonConvert.DeserializeObject<GetCustomerandCardFormResponse>(Response);
            var customerReqObjJarr = ResponseObj["Data"].Value<JObject>();
            var CustObjJarr = customerReqObjJarr["GetCustomerSearchOutput"].Value<JArray>();
            var CardObjJarr = customerReqObjJarr["GetCardSearchOutput"].Value<JArray>();
            List<GetCustomerFormDetails> customerFormDetails = CustObjJarr.ToObject<List<GetCustomerFormDetails>>();
            customerandCardFormResponse.CustomerFormDetails.AddRange(customerFormDetails);
            List<GetCardFormDetails> cardFormDetails = CardObjJarr.ToObject<List<GetCardFormDetails>>();
            customerandCardFormResponse.CardFormDetails.AddRange(cardFormDetails);
            return customerandCardFormResponse;

        }
    }
}
