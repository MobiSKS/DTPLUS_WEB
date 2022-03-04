using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ViewModel.Officers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel.Approvals
{
    public class TerminalDeInstallationRequestApprovalRequestModal
    {
        public TerminalDeInstallationRequestApprovalRequestModal()
        {
            ZoneMdl = new List<ZonalOfficeResponseModal>();
            RegionMdl = new List<RegionalOfficeResponseModal>();
            ZoneMdl.Add(new ZonalOfficeResponseModal
            {
                ZonalOfficeID = 0,
                ZonalOfficeName = "--Select--"
            });
        }
        public string ZonalOfficeId { get; set; }
        public string RegionalOfficeId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string MerchantID { get; set; }
        public string TerminalID { get; set; }
        public virtual List<RegionalOfficeResponseModal> RegionMdl { get; set; }
        public virtual List<ZonalOfficeResponseModal> ZoneMdl { get; set; }
    }
}
