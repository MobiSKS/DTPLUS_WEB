using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.ICICIBankCreditPouch
{
    public class ReqAvailEnrollReq : BaseEntity
    {
        public string CustomerId { get; set; }
        public int FuleConsumptionCapacity { get; set; }
        public int PlanTypeId { get; set; }
        public string ReferenceNo { get; set; }
        public string CustomerRemarks { get; set; }
        public int ActionType { get; set; }
        public string RequestedBy { get; set; }
    }
}
