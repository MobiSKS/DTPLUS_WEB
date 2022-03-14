
using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class UpdateCardLimit : BaseEntity
    {
        public ObjCardLimits[] objCardLimits { get; set; }
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
