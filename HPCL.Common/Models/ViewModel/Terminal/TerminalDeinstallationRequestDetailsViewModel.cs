using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel.Terminal
{
    public class TerminalDeinstallationRequestDetailsViewModel
    {

        public string MerchantID { get; set; }
        public string TerminalID { get; set; }
        public string RetailOutletName { get; set; }
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }

        public string Status { get; set; }
        public string Remark { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedDate { get; set; }

        public string AuthorizedBy { get; set; }

        public string AuthorizedDate { get; set; }
    }
}
