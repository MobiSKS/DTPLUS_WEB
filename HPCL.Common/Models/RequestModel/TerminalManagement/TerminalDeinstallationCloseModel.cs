using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.TerminalManagement
{
    public class TerminalDeinstallationCloseModel:BaseEntity
    {
        public TerminalDeinstallationCloseModel()
        {
            ObjMerchantTerminalMapInput = new List<MerchantTerminalDeInstallationCloseDetail>();
        }
        public string Status { get; set; }
        public string Comments { get; set; }
        public List<MerchantTerminalDeInstallationCloseDetail> ObjMerchantTerminalMapInput { get; set; }
    }
    public class MerchantTerminalDeInstallationCloseDetail
    {
        public string MerchantId { get; set; }
        public string TerminalID { get; set; }
    }
}

