using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.HDFCBankCreditPouch
{
    public class ActionApproveRejectReq : BaseEntity
    {
       public ObjBankEntryDetail[] ObjBankEntryDetail { get; set; }
    }

    public class ObjBankEntryDetail
    {
        public string CustomerId { get; set; }
        public int RequestId { get; set; }
        public string BankRemark { get; set; }
        public int ActionType { get; set; }
        public string ApprovedBy { get; set; }
    }
}
