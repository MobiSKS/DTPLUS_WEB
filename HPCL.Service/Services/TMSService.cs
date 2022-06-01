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
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.CommonEntity.RequestEnities;
using HPCL.Common.Models;

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
        public async Task<CommonResponseData> EnrollToTransportManagementSystem(string CustomerId)
        {
            EnrollToTransportManagementSystemModel model = new EnrollToTransportManagementSystemModel();
            model.UserAgent = CommonBase.useragent;
            model.UserIp = CommonBase.userip;
            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.CustomerId = CustomerId;

            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.EnrollTransportManagementSystem);

            JObject linkedObj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            CommonResponseData responseData = new CommonResponseData();

            if (linkedObj["Status_Code"].ToString() == "200")
            {
                var Jarr = linkedObj["Data"].Value<JArray>();
                List<CommonResponseData> responseLst = Jarr.ToObject<List<CommonResponseData>>();
                responseData = responseLst[0];
                responseData.Internel_Status_Code = Convert.ToInt32(linkedObj["Internel_Status_Code"].ToString());
            }
            else
            {
                responseData.Internel_Status_Code = Convert.ToInt32(linkedObj["Internel_Status_Code"].ToString());
                responseData.Status = Convert.ToInt32(linkedObj["Status_Code"].ToString());
            }

            return responseData;
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
                model.Success = "";
                if (tmsUrlResponse.Internel_Status_Code == 1000)
                {
                    model.StatusCode = tmsUrlResponse.Internel_Status_Code;
                    model.Success = tmsUrlResponse.Message;

                    if (tmsUrlResponse != null && tmsUrlResponse.Data != null && tmsUrlResponse.Data.Count > 0)
                    {
                        model.Status = tmsUrlResponse.Data[0].Status;
                        model.Reason = tmsUrlResponse.Data[0].Reason;
                        model.Success = tmsUrlResponse.Data[0].Reason;
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

        public async Task<EnrollmentsApprovalModel> ApproveEnrollments(EnrollmentsApprovalModel model)
        {
            model.StatusResponseMdl.AddRange(await GetTMSEnrollmentStatus());
            if (model.TMSStatus == 0)
                model.TMSStatus = 1;//Pending

            if (string.IsNullOrEmpty(model.FromDate))
            {
                model.FromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                model.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }

            string strFromDate = "";
            string strToDate = "";
            if (!string.IsNullOrEmpty(model.FromDate))
            {
                string[] frmDate = model.FromDate.Split("-");
                strFromDate = frmDate[2] + "-" + frmDate[1] + "-" + frmDate[0];
            }
            if (!string.IsNullOrEmpty(model.ToDate))
            {
                string[] toDate = model.ToDate.Split("-");
                strToDate = toDate[2] + "-" + toDate[1] + "-" + toDate[0];
            }

            var reqBody = new GetCustomerDetailForEnrollmentApprovalRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = string.IsNullOrEmpty(model.CustomerID) ? "" : model.CustomerID,
                TMSUserId = string.IsNullOrEmpty(model.TMSUserId) ? "" : model.TMSUserId,
                FromDate = strFromDate,
                ToDate = strToDate,
                TMSStatus = model.TMSStatus.ToString()
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCustomerDetailForEnrollmentApproval);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            EnrollmentsApprovalModel res = obj.ToObject<EnrollmentsApprovalModel>();

            if (res != null && res.Data != null && res.Data.Count > 0)
            {
                model.Data = res.Data;
            }
            else
            {
                model.Data = new List<EnrollmentsApprovalDetails>();
            }
            model.Message = res.Message;
            model.Internel_Status_Code = res.Internel_Status_Code;

            return model;
        }

        public async Task<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse> UpdateCustomerDetailForEnrollmentApproval([FromBody] UpdateCustomerDetailForEnrollmentApprovalRequest model)
        {
            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.UserAgent = CommonBase.useragent;
            model.UserIp = CommonBase.userip;
            model.ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(requestContent, WebApiUrl.UpdateCustomerDetailForEnrollmentApproval);
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
        public async Task<List<StatusResponseModal>> GetTMSEnrollmentStatus()
        {
            var statusType = new StatusRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(statusType), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetTmsEnrollmentStatus);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<StatusResponseModal> lst = jarr.ToObject<List<StatusResponseModal>>();

            return lst;
        }

    }
}