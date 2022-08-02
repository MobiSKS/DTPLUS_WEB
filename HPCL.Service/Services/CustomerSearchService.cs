using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.CustomerSearch;
using HPCL.Common.Models.ViewModel.CustomerSearch;
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
    public class CustomerSearchService : ICustomerSearchService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public CustomerSearchService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<ControlCardPinResetRes> CCPinReset(ControlCardPinResetReq entity)
        {
            var reqBody = new ControlCardPinResetReq
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID = entity.CustomerID ?? "",
                ControlCardNo = entity.ControlCardNo ?? "",
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.CCPinResetUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            ControlCardPinResetRes searchList = obj.ToObject<ControlCardPinResetRes>();
            return searchList;
        }




        #region Search Customer
        public async Task<BasicSearchModel> SearchCustomer()
        {
            BasicSearchModel searchCustomerModel = new BasicSearchModel();

            return searchCustomerModel;
        }

        public async Task<List<SearchDetailsTableModel>> SearchCustomerDetails(string customerId, string mobileNo, string formNumber, string nameonCard, string CustomerName, string communicationStateId, string communicationCityName)
        {
            var searchDetailsTableForms = new BasicSearchModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerId = null,
                MobileNo = null,
                FormNumber = null,
                NameonCard = null,
                CustomerName = null,
                CommunicationStateId = null,
                CommunicationCityName = null


                // CustomerId = string.IsNullOrEmpty(customerId) ? "" : customerId,
                //MobileNo = string.IsNullOrEmpty(mobileNo) ? "" : mobileNo,
                //FormNumber = string.IsNullOrEmpty(formNumber) ? "" : formNumber,
                //NameonCard = string.IsNullOrEmpty(nameonCard) ? "" : nameonCard,
                //CustomerName = string.IsNullOrEmpty(CustomerName) ? "" : CustomerName,
                //CommunicationStateId = string.IsNullOrEmpty(communicationStateId) ? "" : communicationStateId,
                //CommunicationCityName = string.IsNullOrEmpty(communicationCityName) ? "" : communicationCityName
            };

            StringContent searchDetailsTableContent = new StringContent(JsonConvert.SerializeObject(searchDetailsTableForms), Encoding.UTF8, "application/json");

            var searchDetailsTableResponse = await _requestService.CommonRequestService(searchDetailsTableContent, WebApiUrl.searchcustomer);

            JObject searchDetailsTableObj = JObject.Parse(JsonConvert.DeserializeObject(searchDetailsTableResponse).ToString());
            var searchDetailsTableJarr = searchDetailsTableObj["Data"].Value<JArray>();
            List<SearchDetailsTableModel> searchDetailsTableModels = searchDetailsTableJarr.ToObject<List<SearchDetailsTableModel>>();
            return searchDetailsTableModels;
        }

        #endregion







    }
}
