using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ResponseModel.TatkalCardCustomer;
using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.TatkalCardCustomer;
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
    public class TatkalCardCustomerService : ITatkalCardCustomerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;

        public TatkalCardCustomerService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }
        public async Task<TatkalCustomerCardRequestInfo> RequestForTatkalCard()
        {
            TatkalCustomerCardRequestInfo custMdl = new TatkalCustomerCardRequestInfo();
            custMdl.Remarks = "";
            custMdl.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());

            return custMdl;
        }

        public async Task<TatkalCustomerCardRequestInfo> RequestForTatkalCard(TatkalCustomerCardRequestInfo tatkalCustomerCardRequestInfo)
        {
            var driversRequestData = new Dictionary<string, string>
                {
                    {"Useragent", CommonBase.useragent},
                    {"Userip", CommonBase.userip},
                    {"UserId", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                    {"RegionalId", tatkalCustomerCardRequestInfo.CustomerRegionID.ToString()},
                    {"NoofCards", tatkalCustomerCardRequestInfo.NoofCards.ToString()},
                    {"CreatedBy", _httpContextAccessor.HttpContext.Session.GetString("UserName")}
                };

            StringContent content = new StringContent(JsonConvert.SerializeObject(driversRequestData), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.insertTatkalCardRequest);


            CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(response);

            tatkalCustomerCardRequestInfo.Internel_Status_Code = customerResponse.Internel_Status_Code;
            tatkalCustomerCardRequestInfo.Remarks = customerResponse.Message;

            if (customerResponse.Internel_Status_Code != 1000)
            {
                if (customerResponse.Data != null)
                    tatkalCustomerCardRequestInfo.Remarks = customerResponse.Data[0].Reason;
                else
                    tatkalCustomerCardRequestInfo.Remarks = customerResponse.Message;

                tatkalCustomerCardRequestInfo.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());
            }

            return tatkalCustomerCardRequestInfo;
        }

        public async Task<TatkalCardCustomerModel> CreateTatkalCustomer(TatkalCardCustomerModel custModel)
        {
            custModel.Remarks = "";
            custModel.CustomerZonalOfficeMdl.AddRange(await _commonActionService.GetZonalOfficeListForDropdown());
            custModel.CustomerStateMdl.AddRange(await _commonActionService.GetCustStateList());
            custModel.CustomerSecretQueMdl.AddRange(await _commonActionService.GetCustomerSecretQuestionListForDropdown());

            return custModel;
        }
        public async Task<List<TatkalCardCustomerViewResponse>> GetAllTatkalCustomerCard(TatkalViewRequest entity)
        {
            var searchBody = new TatkalViewRequest();
            if (entity.RegionalId != null)
            {
                searchBody = new TatkalViewRequest
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    RegionalId = entity.RegionalId,

                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new TatkalViewRequest
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    RegionalId = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
            }
            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ViewRequestDriverCard);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<TatkalCardCustomerViewResponse> searchList = jarr.ToObject<List<TatkalCardCustomerViewResponse>>();
            return searchList;
        }


        public async Task<TatkalViewRequestModel> ViewAllocatedMapCard()
        {
            TatkalViewRequestModel custModel = new TatkalViewRequestModel();
            custModel.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());

            return custModel;

        }
            
      


    }
}
