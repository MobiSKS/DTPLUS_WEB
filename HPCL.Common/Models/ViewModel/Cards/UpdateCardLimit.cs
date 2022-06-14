
using HPCL.Common.Models.CommonEntity;
using System;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class UpdateCardLimit : BaseEntity
    {
        public ObjCardLimits[] objCardLimits { get; set; }
    }

    public class ObjCardLimits
    {
        public string Cardno { get; set; }
        public Decimal SaleTxnLimit { get; set; }
        public Decimal DailySaleLimit { get; set; }
        public Decimal MonthlySaleLimit { get; set; }
    }
}
