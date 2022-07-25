using HPCL.Common.Models.CommonEntity;
using System;

namespace HPCL.Common.Models.ViewModel.PC_ICICIBankCreditPouch
{
    public class EnrollExceptionRequest : BaseEntity
    {
        public string CustomerId { get; set; }
        public Decimal FuleConsumptionCapacity { get; set; }
        public int PlanTypeId { get; set; }
        public string ReferenceNo { get; set; }
        public string MoComment { get; set; }
        public string RequestedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
