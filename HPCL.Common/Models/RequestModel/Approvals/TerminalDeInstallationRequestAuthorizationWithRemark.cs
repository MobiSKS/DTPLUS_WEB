using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Approvals;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Approvals
{
    public class TerminalDeInstallationRequestAuthorizationWithRemark:CommonResponseBase
    {
        public TerminalDeInstallationRequestAuthorizationWithRemark()
        {
            TerminalDeInstallationRequestAuthorizationTbl = new List<GetTerminalDeInstallationRequestAuthorizationReponseModal>();
        }
        public string Remark { get; set; }
        public virtual List<GetTerminalDeInstallationRequestAuthorizationReponseModal> TerminalDeInstallationRequestAuthorizationTbl { get; set; }
    }
}
