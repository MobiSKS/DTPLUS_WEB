using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.RequestEnities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Approvals
{
    public class TerminalDeInstallationApprovalSubmit : BaseEntity
    {
        public string Remark { get; set; }
        public string Action { get; set; }
        public List<MerchantTerminalMapModal> ObjTerminalDeInstallationInsertInput { get; set; }
    }
}
