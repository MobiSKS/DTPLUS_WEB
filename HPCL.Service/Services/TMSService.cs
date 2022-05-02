﻿using HPCL.Common.Models.ResponseModel.TMS;
using HPCL.Common.Models.ViewModel.TMS;
using HPCL.Common.Models.ViewModel.ViewCard;
using HPCL.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Helper;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;

namespace HPCL.Service.Services
{
    public class TMSService: ITMSService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public TMSService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<EnrollToTransportManagementSystemModel> EnrollToTransportManagementSystem()
        {
            EnrollToTransportManagementSystemModel Model = new EnrollToTransportManagementSystemModel();
            Model.Message = "";

            return Model;
        }

        public async Task<ViewCustomerSearch> ViewCustomerDetails(string CustomerId)
        {
            ViewCustomerSearch viewCardSearch = new ViewCustomerSearch();

            var requestBody = new ViewCardDetails()
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                Customerid = CustomerId,
             };
                      

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCustomerSearchDetails);

            viewCardSearch = JsonConvert.DeserializeObject<ViewCustomerSearch>(response);
            return viewCardSearch;
        }
        public async Task<EnrollToTransportManagementSystemModel> EnrollToTransportManagementSystem(EnrollToTransportManagementSystemModel model)
        {
            model.UserAgent = CommonBase.useragent;
            model.UserIp = CommonBase.userip;
            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.EnrollTransportManagementSystem);

            EnrollTMSResponse enrollTMSResponse = JsonConvert.DeserializeObject<EnrollTMSResponse>(response);
            model.Internel_Status_Code = enrollTMSResponse.Internel_Status_Code;

            if (model.Internel_Status_Code == 1000)
            {
                model.Message = enrollTMSResponse.Message;
                if (enrollTMSResponse != null && enrollTMSResponse.Data != null)
                {
                    model.Message = enrollTMSResponse.Data.message;
                }
            }
            else
            {
                model.Message = enrollTMSResponse.Message;
                model.StatusCode = enrollTMSResponse.Internel_Status_Code;
            }

            return model;
        }

    }
}