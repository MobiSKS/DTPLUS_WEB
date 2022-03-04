using HPCL.Common.Models.ResponseModel.Approvals;
using HPCL.Common.Models.ViewModel.Approvals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IApprovalService
    {
        Task<TerminalDeInstallationRequestApprovalRequestModal> TerminalDeInstallationRequestApproval();
        Task<List<GetTerminalDeInstallationRequestApprovalReponseModal>> GetTerminalsForApproval(string zonalOfcID, string regionalOfcID, string fromDate, string toDate, string merchantId, string terminalId);
    }
}
