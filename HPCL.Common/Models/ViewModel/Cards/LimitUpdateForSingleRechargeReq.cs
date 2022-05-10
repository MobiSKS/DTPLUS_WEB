using System;
using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class LimitUpdateForSingleRechargeReq : BaseEntity
    {
        public ObjCCMSLimitsRec[] objCCMSLimits { get; set; }
    }

    public class ObjCCMSLimitsRec
    {
        public string Cardno { get; set; }
        public int Limittype { get; set; }
        public Decimal Amount { get; set; }
    }
}
