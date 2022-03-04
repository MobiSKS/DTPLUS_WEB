using HPCL.Common.Models.ResponseModel.Approvals;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Approvals
{
    public class TerminalDeInstallationRequestApprovalWithRemark
    {
        public TerminalDeInstallationRequestApprovalWithRemark()
        {
            TerminalDeInstallationRequestApprovalTbl = new List<GetTerminalDeInstallationRequestApprovalReponseModal>();
        }
        public string Remark { get; set; }
        public virtual List<GetTerminalDeInstallationRequestApprovalReponseModal> TerminalDeInstallationRequestApprovalTbl{ get; set; }
    }
}
