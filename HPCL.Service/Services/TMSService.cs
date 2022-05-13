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
                if (enrollTMSResponse != null && enrollTMSResponse.Data != null && enrollTMSResponse.Data.Count > 0)
                {
                    model.Message = enrollTMSResponse.Data[0].message;
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
            List<EnrollVehicleDetailsModel> vehicleDetails = jarr.ToObject<List<EnrollVehicleDetailsModel>>();
            var sortedtList = vehicleDetails.OrderBy(x => x.CardNo).ToList();

            model.CustomerID = customerId;

            foreach (EnrollVehicleDetailsModel details in sortedtList)
            {
                if (string.IsNullOrEmpty(details.VehicleNo))
                {
                    details.VehicleNo = "";
                }
                if (string.IsNullOrEmpty(details.TMSUserId))
                {
                    details.TMSUserId = "";
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
            }

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(enrollVehicleViewModel), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(requestContent, WebApiUrl.InsertVehicleEnrollmentStatus);
            JObject responseObj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            if (responseObj["Status_Code"].ToString() == "200")
            {
                var responseJarr = responseObj["Data"].Value<JArray>();
                List<SuccessResponse> closeRequestResponseList = responseJarr.ToObject<List<SuccessResponse>>();
                return closeRequestResponseList.First().Reason.ToString();
            }
            else
            {
                return responseObj["Message"].ToString();
            }

        }

        public async Task<ManageEnrollmentsModel> ManageEnrollments()
        {
            ManageEnrollmentsModel Model = new ManageEnrollmentsModel();
            Model.Message = "";

            return Model;
        }
        public async Task<ViewCustomerSearch> ViewCustomerDetailsForManageEnrollments(string CustomerId)
        {
            ViewCustomerSearch viewCardSearch = new ViewCustomerSearch();

            var requestBody = new ViewCardDetails()
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                Customerid = CustomerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetManageEnrollments);

            viewCardSearch = JsonConvert.DeserializeObject<ViewCustomerSearch>(response);

            if (viewCardSearch != null && viewCardSearch.Data != null && viewCardSearch.Data.Count > 0)
            {
                if (viewCardSearch.Data[0].TMSStatus == "2")
                {
                    viewCardSearch.Data[0].TMSStatusID = "4";
                }
                else if (viewCardSearch.Data[0].TMSStatus == "4")
                {
                    viewCardSearch.Data[0].TMSStatusID = "2";
                }
            }

            return viewCardSearch;
        }

        public async Task<string> UpdateTMSEnrollmentStatus([FromBody] ManageEnrollmentsModel manageEnrollmentsModel)
        {
            manageEnrollmentsModel.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            manageEnrollmentsModel.UserIp = CommonBase.userip;
            manageEnrollmentsModel.UserAgent = CommonBase.useragent;

            foreach (UpdateEnrollmentCustomer enrollmentCustomer in manageEnrollmentsModel.tmsUpdateEnrollmentCustomerList)
            {
                enrollmentCustomer.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            }

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(manageEnrollmentsModel), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(requestContent, WebApiUrl.UpdateTmsEnrollmentTmsStatus);
            JObject responseObj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            if (responseObj["Status_Code"].ToString() == "200")
            {
                var responseJarr = responseObj["Data"].Value<JArray>();
                List<SuccessResponse> closeRequestResponseList = responseJarr.ToObject<List<SuccessResponse>>();
                return closeRequestResponseList.First().Reason.ToString();
            }
            else
            {
                return responseObj["Message"].ToString();
            }

        }
        public async Task<NavigateToTransportManagementSystemModel> SwitchToCargoFL()
        {
            NavigateToTransportManagementSystemModel Model = new NavigateToTransportManagementSystemModel();
            Model.Message = "";
            Model.Success = "";

            if (_httpContextAccessor.HttpContext.Session.GetString("LoginType").ToUpper() == "CUSTOMER")
            {
                Model.CustomerId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            }
            else
            {
                Model.CustomerId = "";
            }

            return Model;
        }
        public async Task<NavigateToTransportManagementSystemModel> SwitchToCargoFL(NavigateToTransportManagementSystemModel model)
        {
            if (string.IsNullOrEmpty(model.CustomerId))
            {
                model.Message = "Logged in user is not a customer";
                model.Success = "";
            }
            else
            {
                model.UserAgent = CommonBase.useragent;
                model.UserIp = CommonBase.userip;
                model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
                model.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");

                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var responseCustomer = await _requestService.CommonRequestService(content, WebApiUrl.GetTransportManagementSystemUrl);

                GetTMSUrlResponse tmsUrlResponse = JsonConvert.DeserializeObject<GetTMSUrlResponse>(responseCustomer);

                model.url = "";
                model.access_token = "";
                model.refresh_token = "";
                model.Message = "";
                if (tmsUrlResponse.Internel_Status_Code == 1000)
                {
                    model.StatusCode = tmsUrlResponse.Internel_Status_Code;
                    model.Message = tmsUrlResponse.Message;

                    if (tmsUrlResponse != null && tmsUrlResponse.Data != null && tmsUrlResponse.Data.Count > 0)
                    {
                        model.Status = tmsUrlResponse.Data[0].Status;
                        model.Reason = tmsUrlResponse.Data[0].Reason;
                        model.Message = tmsUrlResponse.Data[0].Reason;
                        model.url = tmsUrlResponse.Data[0].url;
                        model.access_token = tmsUrlResponse.Data[0].access_token;
                        model.refresh_token = tmsUrlResponse.Data[0].refresh_token;
                    }
                }
                else
                {
                    model.Message = tmsUrlResponse.Message;
                    model.StatusCode = tmsUrlResponse.Internel_Status_Code;

                    if (tmsUrlResponse != null && tmsUrlResponse.Data != null && tmsUrlResponse.Data.Count > 0)
                    {
                        model.Status = tmsUrlResponse.Data[0].Status;
                        model.Reason = tmsUrlResponse.Data[0].Reason;
                        model.Message = tmsUrlResponse.Data[0].Reason;
                    }
                }
            }

            return model;
        }


    }
}