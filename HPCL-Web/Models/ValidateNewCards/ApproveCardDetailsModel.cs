using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models.ValidateNewCards
{
    public class ApproveCardDetailsModel : BaseEntity
    {
        public string CustomerReferenceNo { get; set; }
        public string Comments { get; set; }
        public string Approvalstatus { get; set; }
        public string ApprovedBy { get; set; }
    }
}
