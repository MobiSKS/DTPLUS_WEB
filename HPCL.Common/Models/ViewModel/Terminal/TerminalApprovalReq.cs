using HPCL.Common.Models.CommonEntity;
using System;

namespace HPCL.Common.Models.ViewModel.Terminal
{
    public class TerminalApprovalReq : BaseEntity
    {
        public TerminalApprovalReq()
        {
            FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Category { get; set; }
        public string ZonalOfficeId { get; set; }
        public string RegionalOfficeId { get; set; }
    }
}
