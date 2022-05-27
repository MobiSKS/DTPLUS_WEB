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
        Task<EnrollToTransportManagementSystemModel> EnrollToTransportManagementSystem(EnrollToTransportManagementSystemModel model);
        Task<EnrollVehicleViewModel> EnrollVehicle();
        Task<EnrollVehicleViewModel> GetEnrollVehicleManagementDetail(string customerId, int enrollmentStatus, string vehicleNo, string cardNo);
        Task<string> SubmitVehicleEnrollment([FromBody] EnrollVehicleViewModel enrollVehicleViewModel);
        Task<ManageEnrollmentsModel> ManageEnrollments();
        Task<ViewCustomerSearch> ViewCustomerDetailsForManageEnrollments(string CustomerId);
        Task<string> UpdateTMSEnrollmentStatus([FromBody] ManageEnrollmentsModel manageEnrollmentsModel);
        Task<NavigateToTransportManagementSystemModel> SwitchToCargoFL();
        Task<NavigateToTransportManagementSystemModel> SwitchToCargoFL(NavigateToTransportManagementSystemModel model);
        Task<EnrollmentsApprovalModel> ApproveEnrollments(EnrollmentsApprovalModel model);
    }
}
