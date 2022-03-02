using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel
{
   public class TerminalManagement:BaseEntity
    {
        public string MerchantId { get; set; }

        public string RetailOutletName { get; set; }

        public string DistrictName { get; set; }

        public string ZonalOfficerId { get; set; }

        public string ReginalOfficerId { get; set; }

        public int StatusFlag { get; set; }

        public string TerminalTypeRequested { get; set; }

        public string TerminalIssuanceType { get; set; }

        public string CreatedBy { get; set; }

        public string Justification { get; set; }
        public int Flag { get; set; }
    }
}
