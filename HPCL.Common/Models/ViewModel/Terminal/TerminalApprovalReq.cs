using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.Terminal
{
    public class TerminalApprovalReq : BaseEntity
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Category { get; set; }
        public string ZonalOfficeId { get; set; }
        public string RegionalOfficeId { get; set; }
    }
}
