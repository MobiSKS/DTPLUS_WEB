using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.TerminalManagement
{
    public class TerminalDeinstallationRequestUpdateModel:BaseEntity
    {
        public TerminalDeinstallationRequestUpdateModel()
        {
            ObjUpdateTerminalDeInstalRequest = new List<TerminalDeinstallationDetailRequestModel>();
        }
        public string MerchantId { get; set; }
        public string DeinstallationType { get; set; }
        public string ModifiedBy { get; set; }
        public string Comments { get; set; }
        public List<TerminalDeinstallationDetailRequestModel> ObjUpdateTerminalDeInstalRequest { get; set; }
    }
    public class TerminalDeinstallationDetailRequestModel
    {
        public string TerminalId { get; set; }
    }
}
