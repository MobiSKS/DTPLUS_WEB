using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.TerminalManagementResponse
{
    public class SearchTerminalDetailsResponseModal
    {
        public string TerminalId { get; set; }
        public string MerchantId { get; set; }
        public string ApprovalDate { get; set; }
        public string Status { get; set; }
        public string DeploymentStatus { get; set; }
    }
}
