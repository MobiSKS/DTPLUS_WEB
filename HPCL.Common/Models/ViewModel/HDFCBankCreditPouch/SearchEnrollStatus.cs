using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.HDFCBankCreditPouch
{
    public class SearchEnrollStatus : BaseEntity
    {
        public string CustomerId { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }
        public int ZO { get; set; }
        public int RO { get; set; }
    }

    public class SearchEnrollStatusClone
    {
        public string CustomerId { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }
        public string ZO { get; set; }
        public string RO { get; set; }
    }
}
