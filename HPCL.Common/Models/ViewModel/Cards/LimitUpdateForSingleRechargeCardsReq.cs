using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class LimitUpdateForSingleRechargeCardsReq : BaseEntity
    {
        public string CustomerID { get; set; }
        public string Cardno { get; set; }
    }
}
