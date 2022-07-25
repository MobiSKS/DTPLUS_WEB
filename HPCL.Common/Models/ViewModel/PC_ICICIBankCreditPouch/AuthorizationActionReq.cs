using HPCL.Common.Models.CommonEntity;
using System;

namespace HPCL.Common.Models.ViewModel.PC_ICICIBankCreditPouch
{
    public class AuthorizationActionReq : BaseEntity
    {
        public ObjBankAuthEntryDetail[] ObjBankAuthEntryDetail { get; set; }
    }

    public class ObjBankAuthEntryDetail
    {
        public string CustomerId { get; set; }
        public int RequestId { get; set; }
        public string AuthorizationRemark { get; set; }
        public int ActionType { get; set; }
        public Decimal CreditLimitAssigned { get; set; }
        public string AuthorizBy { get; set; }
    }
}
