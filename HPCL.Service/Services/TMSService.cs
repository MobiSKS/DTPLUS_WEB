using HPCL.Common.Models.ResponseModel.TMS;
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
using HPCL.Common.Models.RequestModel.TMS;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using HPCL.Common.Models.CommonEntity;

namespace HPCL.Service.Services
{
    public class TMSService: ITMSService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        
        public TMSService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
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

        public async Task<EnrollVehicleViewModel> EnrollVehicle()
        {
            EnrollVehicleViewModel Model = new EnrollVehicleViewModel();
            Model.Message = "";
            Model.StatusList = await _commonActionService.GetVehicleEnrollmentStatusList();

            return Model;
        }
        public async Task<EnrollVehicleViewModel> GetEnrollVehicleManagementDetail(string customerId, int enrollmentStatus, string vehicleNo, string cardNo)
        {
            EnrollVehicleViewModel model = new EnrollVehicleViewModel();

            var request = new GetEnrollVehicleManagementDetailRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = customerId,
                EnrollmentStatus = enrollmentStatus,
                VehicleNo = string.IsNullOrEmpty(vehicleNo) ? "" : vehicleNo,
                CardNo = string.IsNullOrEmpty(cardNo) ? "" : cardNo
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetVehicleEnrollmentDetail);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<EnrollVehicleDetailsModel> limitType = jarr.ToObject<List<EnrollVehicleDetailsModel>>();
            var sortedtList = limitType.OrderBy(x => x.CardNo).ToList();

            model.CustomerID = customerId;

            foreach (EnrollVehicleDetailsModel details in sortedtList)
            {
                if (string.IsNullOrEmpty(details.VehicleNo))
                {
                    details.VehicleNo = "";
                }
            }

            if (sortedtList.Count > 0)
            {
                if (sortedtList.Count == 1)
                {
                    if (string.IsNullOrEmpty(sortedtList[0].VehicleNo))
                    {
                        model.Message = sortedtList[0].Reason;
                    }
                    else
                    {
                        model.vehicleDetailsModel = sortedtList;
                    }
                }
                else
                {
                    model.vehicleDetailsModel = sortedtList;
                }
            }
            else
            {
                model.Message = "Vehicle Not Found";
            }

            return model;
        }

        public async Task<string> SubmitVehicleEnrollment([FromBody] EnrollVehicleViewModel enrollVehicleViewModel)
        {
            enrollVehicleViewModel.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            enrollVehicleViewModel.UserIp = CommonBase.userip;
            enrollVehicleViewModel.UserAgent = CommonBase.useragent;

            foreach (EnrollVehicleDetailsModel vehicleDetailsModel in enrollVehicleViewModel.vehicleDetailsModel)
            {
                vehicleDetailsModel.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
                vehicleDetailsModel.TMSUserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            }

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(enrollVehicleViewModel), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(requestContent, WebApiUrl.insertVehicleEnrollmentStatus);
            JObject responseObj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            if (responseObj["Status_Code"].ToString() == "200")
            {
                var closeRequestResponseJarr = responseObj["Data"].Value<JArray>();
                List<SuccessResponse> closeRequestResponseList = closeRequestResponseJarr.ToObject<List<SuccessResponse>>();
                return closeRequestResponseList.First().Reason.ToString();
            }
            else
            {
                return responseObj["Message"].ToString();
            }

        }

    }
}