using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.HDFCBankCreditPouch
{
    public class CustomerDetailsReq : BaseEntity
    {
        public string CustomerId { get; set; }
        public string RequestedBy { get; set; }
    }
}
