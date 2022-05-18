using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class MapEmergencyAddOnCardReq : BaseEntity
    {
        public objEmergencyReplacementCards[] objEmergencyReplacementCards { get; set; }
    }

    public class objEmergencyReplacementCards
    {
        public string CustomerId { get; set; }
        public string OldCardNo { get; set; }
        public string NewCardNo { get; set; }
    }
}
