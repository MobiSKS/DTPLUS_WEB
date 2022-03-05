using HPCL.Common.Models.RequestModel.Approvals;
using HPCL.Common.Models.ResponseModel.Approvals;
using HPCL.Common.Models.ViewModel.Approvals;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IApprovalService
    {
        Task<TerminalDeInstallationRequestApprovalRequestModal> TerminalDeInstallationRequestApproval();
        Task<TerminalDeInstallationRequestApprovalWithRemark> GetTerminalsForApproval(string zonalOfcID, string regionalOfcID, string fromDate, string toDate, string merchantId, string terminalId);
        Task<string> TerminalDeInstallRequestApprovalRejection([FromBody] TerminalDeInstallationApprovalSubmit approvalRejectionMdl);
        Task<TerminalDeInstallationRequestAuthorizationRequestModal> TerminalDeInstallationRequestAuthorization();
        Task<TerminalDeInstallationRequestAuthorizationWithRemark> GetTerminalsForAuthorization(string zonalOfcID, string regionalOfcID, string fromDate, string toDate, string merchantId, string terminalId);
        Task<string> TerminalDeInstallRequestApprovalRejectionAuth([FromBody] TerminalDeInstallationAuthorizationSubmit approvalRejectionMdl);
    }
}
