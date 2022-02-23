using HPCL_Web.Models.CommonEntity;

namespace HPCL_Web.Models.ViewModel.Cards
{
    public class UpdateCardLimit : BaseEntity
    {
        public ObjCardLimits[] objCardLimits { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class ObjCardLimits
    {
        public string CardNo { get; set; }
        public int CashPurse { get; set; }
        public int SaleTxn { get; set; }
        public int DailySale { get; set; }
        public int MonthlySale { get; set; }
    }
}
