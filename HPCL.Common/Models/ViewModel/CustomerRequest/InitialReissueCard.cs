using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.CustomerRequest
{
    public class InitialReissueCard : BaseEntity
    {
        public string CustomerId { get; set; }
        public TypeHotlistReissueCardRequests[] TypeHotlistReissueCardRequest { get; set; }
    }

    public class TypeHotlistReissueCardRequests
    {
        public string CardNo { get; set; }
        public string VechileNo { get; set; }
        public string ReasonId { get; set; }
    }
}
