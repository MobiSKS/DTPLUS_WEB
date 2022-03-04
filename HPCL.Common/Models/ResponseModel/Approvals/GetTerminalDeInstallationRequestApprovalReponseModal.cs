using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.Approvals
{
    public class GetTerminalDeInstallationRequestApprovalReponseModal
    {
        public string MerchantId { get; set; }
        public string TerminalId { get; set; }
        public string RetailOutletName { get; set; }
        public string SpendsOfLastQuater { get; set; }
        public string NoOfTransactionsOfLastQuater { get; set; }
        public string ZonalOfficeName { get; set; }
        public string ApprovalRemark { get; set; }
        public string RegionalOfficeName { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedDate { get; set; }
    }
}
