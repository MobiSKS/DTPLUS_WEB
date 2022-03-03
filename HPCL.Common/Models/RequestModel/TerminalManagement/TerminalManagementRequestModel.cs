using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.TerminalManagement
{
    public class TerminalManagementRequestModel:BaseEntity
    {
        public TerminalManagementRequestModel()
        {
            ObjMerchantTerminalInstallationRequestCloseDetail = new List<MerchantTerminalInstallationRequestCloseDetail>();
        }
        public string  ReasonId { get; set; }
        public string ReasonName { get; set; }
        public string ModifiedBy { get; set; }
        public List<MerchantTerminalInstallationRequestCloseDetail> ObjMerchantTerminalInstallationRequestCloseDetail { get; set; }
    }
    public class MerchantTerminalInstallationRequestCloseDetail
    {
        public string MerchantId { get; set; }
        public string TerminalId { get; set; }
    }
}
