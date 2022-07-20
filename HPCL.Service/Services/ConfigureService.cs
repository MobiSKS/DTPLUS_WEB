using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.Configure;
using HPCL.Common.Models.ViewModel.Configure;
using HPCL.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using HPCL.Common.Models;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.CommonEntity.RequestEnities;
using Microsoft.Extensions.Configuration;

namespace HPCL.Service.Services
{
    public class ConfigureService: IConfigureService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        private readonly IConfiguration _configuration;

        public ConfigureService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
            _configuration = configuration;
        }

        public async Task<SMSAlertstoIndividualCardUsersModel> ConfCardPhone(SMSAlertstoIndividualCardUsersModel Model)
        {
            if (!string.IsNullOrEmpty(Model.CustomerID))
            {
                var reqBody = new GetSMSAlertstoIndividualCardUsersRequest
                {
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    CustomerID = Model.CustomerID,
                    CardNo = string.IsNullOrEmpty(Model.CardNo) ? "" : Model.CardNo
                };

                StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
                var response = await _requestService.CommonRequestService(content, WebApiUrl.GetSmsAlertsToIndividualCardUsersDetails);

                JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
                SMSAlertstoIndividualCardUsersModel res = obj.ToObject<SMSAlertstoIndividualCardUsersModel>();

                if (res != null && res.Data != null)
                {
                    Model.Data = res.Data;
                }

                Model.Message = res.Message;
                Model.Internel_Status_Code = res.Internel_Status_Code;
                if (res != null && res.Data != null && res.Data.CustomerDetails.Count > 0 && res.Data.CustomerDetails[0].Status == 1)
                {
                    Model.IndividualOrgName = res.Data.CustomerDetails[0].IndividualOrgName;
                    Model.NameOnCard = res.Data.CustomerDetails[0].NameOnCard;
                }
                else if (res != null && res.Data != null && res.Data.CustomerDetails.Count > 0 && res.Data.CustomerDetails[0].Status == 0)
                {
                    Model.IndividualOrgName = "";
                    Model.NameOnCard = "";
                    Model.Message = res.Data.CustomerDetails[0].Reason;
                    Model.Data = new SMSAlertDetails();
                }
                if (Model.Data != null)
                {
                    foreach(CustCardDetails card in Model.Data.CardDetails)
                    {
                        if(string.IsNullOrEmpty(card.Mobileno))
                        {
                            card.Mobileno = "";
                        }
                    }
                }
            }
            else
            {
                Model.CustomerID = "";
            }

            return Model;
        }
        public async Task<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse> DeleteSMSAlertsToIndividualCardUsers(string customerID, string cardNo, string mobileNo)
        {
            var requestBody = new DeleteSMSAlertsToIndividualCardUsersRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                CustomerID= customerID,
                CardNo= cardNo,
                MobileNo= mobileNo
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.DeleteSmsAlertsToIndividualCardUsers);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var updateRes = obj["Data"].Value<JArray>();

            List<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse> updateResponse = updateRes.ToObject<List<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse>>();
            
            return updateResponse[0];
        }
        public async Task<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse> SubmitSMSAlertstoIndividualCardUsers([FromBody] UpdateSMSAlertstoIndividualCardUsersRequest model)
        {
            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.UserAgent = CommonBase.useragent;
            model.UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress");
            model.ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            
            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(requestContent, WebApiUrl.UpdateSmsAlertsToIndividualCardUsers);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            if (obj["Status_Code"].ToString() == "200")
            {
                var Jarr = obj["Data"].Value<JArray>();
                List<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse> updateResponse = Jarr.ToObject<List<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse>>();
                return updateResponse[0];
            }
            else
            {
                return new HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse();
            }
        }
    }
}
