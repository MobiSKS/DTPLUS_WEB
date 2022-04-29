using HPCL.Common.Models.ResponseModel.TMS;
using HPCL.Common.Models.ViewModel.TMS;
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
    }
}
