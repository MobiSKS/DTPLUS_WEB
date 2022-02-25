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
        private readonly ICommonActionService _commonActionService;

        public MyHpOTCCardCustomerService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }

        public async Task<RequestForOTCCardModel> RequestForOTCCard()
        {
            RequestForOTCCardModel custMdl = new RequestForOTCCardModel();

            custMdl.RegionMdl.AddRange(await _commonActionService.GetRegionList());

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

                requestForOTCCardModel.RegionMdl.AddRange(await _commonActionService.GetRegionList());
            }

            return requestForOTCCardModel;
        }
    }
}
