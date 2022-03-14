using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class UpdateService : BaseEntity
    {
        public string CustomerId { get; set; }
        public string CardNo { get; set; }
        public int ServiceId { get; set; }
        public int Flag { get; set; }
    }
}
