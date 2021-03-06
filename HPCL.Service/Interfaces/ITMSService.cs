using HPCL.Common.Models;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.RequestModel.TMS;
using HPCL.Common.Models.ResponseModel.TMS;
using HPCL.Common.Models.ViewModel.TMS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ITMSService
    {
        Task<EnrollToTransportManagementSystemModel> EnrollToTransportManagementSystem();
        Task<ViewCustomerSearch> ViewCustomerDetails(string CustomerId);
        Task<CommonResponseData> EnrollToTransportManagementSystem(string CustomerId);
        Task<EnrollVehicleViewModel> EnrollVehicle();
        Task<EnrollVehicleViewModel> GetEnrollVehicleManagementDetail(string customerId, int enrollmentStatus, string vehicleNo, string cardNo);
        Task<CommonResponseData> SubmitVehicleEnrollment([FromBody] EnrollVehicleViewModel enrollVehicleViewModel);
        Task<ManageEnrollmentsModel> ManageEnrollments();
        Task<ViewCustomerSearch> ViewCustomerDetailsForManageEnrollments(string CustomerId);
        Task<CommonResponseData> UpdateTMSEnrollmentStatus([FromBody] ManageEnrollmentsModel manageEnrollmentsModel);
        Task<NavigateToTransportManagementSystemModel> SwitchToCargoFL();
        Task<NavigateToTransportManagementSystemModel> SwitchToCargoFL(NavigateToTransportManagementSystemModel model);
        Task<EnrollmentsApprovalModel> ApproveEnrollments(EnrollmentsApprovalModel model);
        Task<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse> UpdateCustomerDetailForEnrollmentApproval([FromBody] UpdateCustomerDetailForEnrollmentApprovalRequest model);
        Task<List<StatusResponseModal>> GetTMSEnrollmentStatus();
    }
}