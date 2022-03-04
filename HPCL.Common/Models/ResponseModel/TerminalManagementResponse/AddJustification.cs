using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ResponseModel.TerminalManagementResponse
{
    public class AddJustification : BaseEntity
    {
        public string MerchantId { get; set; }
        public string TerminalTypeRequested { get; set; }
        public string TerminalIssuanceType { get; set; }
        public string Justification { get; set; }
    }
}
