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
        private readonly ICommonActionService _commonActionService;

        public DriverCardCustomerService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }

        public async Task<RequestForDriverCardModel> RequestForDriverCard()
        {
            RequestForDriverCardModel custMdl = new RequestForDriverCardModel();
            custMdl.Remarks = "";
            custMdl.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());

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

                requestForDriverCardModel.RegionMdl.AddRange(await _commonActionService.GetregionalOfficeList());
            }

            return requestForDriverCardModel;
        }

        public async Task<DriverCardCustomerModel> CreateDriverCardCustomer()
        {
            DriverCardCustomerModel custMdl = new DriverCardCustomerModel();

            custMdl.CustomerStateMdl.AddRange(await _commonActionService.GetCustStateList());

            return custMdl;
        }


    }
}
