using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.AMEXBankCreditPouch
{
    public class GetRequestAuthorizationReq : BaseEntity
    {
        public string CustomerId { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }
    }
}
