using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.ICICIBankCreditPouch
{
    public class GetEnrollStatusReport : BaseEntity
    {
        public string CustomerId { get; set; }
        public int RequestId { get; set; }
    }
}
