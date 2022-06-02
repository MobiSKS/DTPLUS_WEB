using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ResponseModel.CustomerFeeWaiver;
using HPCL.Common.Models.ViewModel.CustomerFeeWaiver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ICustomerFeeWaiverServices
    {
        Task<PendingCustResponse> FeeWaiver(PendingCustomer entity);
        Task<BindPendingCustomerRes> BindPendingCustomer(string formNumber);
        Task<List<SuccessResponse>> ApproveCustomer(string CustomerReferenceNo, string comments);
        Task<List<SuccessResponse>> RejectCustomer(string CustomerReferenceNo, string comments);
        void ViewCustomer(string formNumber);
        Task<ViewCustomerDetailsResponse> ViewCustomerDetails(string formNumber);
    }
}
