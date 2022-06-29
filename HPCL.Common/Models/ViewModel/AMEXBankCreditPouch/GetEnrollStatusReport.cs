using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.AMEXBankCreditPouch
{
    public class GetEnrollStatusReport : BaseEntity
    {
        public string CustomerId { get; set; }
        public int RequestId { get; set; }
    }
}
