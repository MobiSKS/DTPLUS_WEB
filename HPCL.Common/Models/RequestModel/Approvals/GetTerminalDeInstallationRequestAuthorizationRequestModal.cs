using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Approvals
{
    public class GetTerminalDeInstallationRequestAuthorizationRequestModal : BaseEntity
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string MerchantId { get; set; }
        public string TerminalId { get; set; }
        public string ZonalOfficeId { get; set; }
        public string RegionalOfficeId { get; set; }
    }
}
