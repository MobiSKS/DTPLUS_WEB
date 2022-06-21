using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.HDFCBankCreditPouch;
using HPCL.Common.Models.ViewModel.HDFCBankCreditPouch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IHDFCBankCreditPouchService
    {
        Task<CustomerDetailsRes> GetCustomerDetails(CustomerDetailsReq entity);
        Task<GetPlanRes> GetPlan(string amount);
        Task<List<SuccessResponse>> InsertExceptionRequest(EnrollExceptionRequest entity);
        Task<SearchRequestApprovalRes> SearchRequestApproval(SearchRequestApprovalClone entity);
        Task<List<SuccessResponse>> SubmitRequestApproval(string bankEntryDetail);
        Task<SearchEnrollStatusRes> GetEnrollStatus(SearchEnrollStatusClone entity);
        Task<GetEnrollStatusReportRes> GetEnrollStatusReport(string customerId, int requestId);
        Task<CcmsRechargeHdfcRes> CCMSRechargeHDFC(string customerId, string amount);
        Task<string> GetRequestAuthorizationDetails(GetRequestAuthorizationReq entity);
        //Task<string> AuthorizationAction();
    }
}
