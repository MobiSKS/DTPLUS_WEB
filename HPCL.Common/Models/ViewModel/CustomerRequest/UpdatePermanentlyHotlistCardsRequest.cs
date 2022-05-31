using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.CustomerRequest
{
    public class UpdatePermanentlyHotlistCardsRequest : BaseEntity
    {
        public string CustomerId { get; set; }
        public TypePermanentlyHotlistCards[] TypePermanentlyHotlistCards { get; set; }
    }

    public class TypePermanentlyHotlistCards
    {
        public string CardNo { get; set; }
        public int StatusId { get; set; }
    }
}
