using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.MerchantFinancials
{
    public class GetBatchDetails : BaseEntity
    {
        public string TerminalId { get; set; }
        public int BatchId { get; set; }
    }
}
