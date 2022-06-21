using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.HDFCBankCreditPouch
{
    public class AuthorizationActionReq : BaseEntity
    {
        public ObjBankAuthEntryDetail[] objBankAuthEntryDetail { get; set; }
    }

    public class ObjBankAuthEntryDetail
    {
        public string CustomerId { get; set; }
        public int RequestId { get; set; }
        public string AuthorizationRemark { get; set; }
        public int ActionType { get; set; }
        public int CreditLimitAssigned { get; set; }
        public string AuthorizBy { get; set; }
    }
}
