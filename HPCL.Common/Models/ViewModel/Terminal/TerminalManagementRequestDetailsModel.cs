using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel
{
    public class TerminalManagementRequestDetailsModel
    {
        public string MerchantID { get; set; }
        public string TerminalID { get; set; }
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }

        public string Status { get; set; }
        public string Remark { get; set; }
        public string TerminalTypeRequested { get; set; }
        public string TerminalType { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedDate { get; set; }
    }
}
