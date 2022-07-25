using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.PC_ICICIBankCreditPouch;
using HPCL.Common.Models.ViewModel.PC_ICICIBankCreditPouch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IPC_ICICIBankCreditPouchService
    {
        Task<CustomerDetailsRes> GetCustomerDetails(CustomerDetailsReq entity);
        Task<GetPlanRes> GetPlan(string amount);
        Task<List<SuccessResponse>> InsertExceptionRequest(EnrollExceptionRequest entity);
        Task<SearchRequestApprovalRes> SearchRequestApproval(SearchRequestApproval entity);
        Task<List<SuccessResponse>> SubmitRequestApproval(string bankEntryDetail);
        Task<SearchEnrollStatusRes> GetEnrollStatus(SearchEnrollStatus entity);
        Task<GetEnrollStatusReportRes> GetEnrollStatusReport(string customerId, int requestId);
        Task<GetRequestAuthorizationRes> GetRequestAuthorizationDetails(GetRequestAuthorizationReq entity);
        Task<List<SuccessResponse>> AuthorizationAction(string authReq);
        Task<CheckEligibleRes> CheckEligibility(CheckEligibleReq entity);
        Task<List<SuccessResponse>> ReqAvailEnroll(string customerId, string planTypeId);
    }
}