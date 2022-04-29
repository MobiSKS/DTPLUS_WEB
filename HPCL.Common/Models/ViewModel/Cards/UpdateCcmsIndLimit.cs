
using HPCL.Common.Models.CommonEntity;
using System;
using System.ComponentModel;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class UpdateCcmsIndLimit : BaseEntity
    {
        public ObjCCMSLimits[] ObjCCMSLimits { get; set; }
    }

    public class ObjCCMSLimits
    {
        public string Cardno { get; set; }
        public string Mobileno { get; set; }
        public int Limittype { get; set; }
        [DefaultValue(0)]
        public Decimal Amount { get; set; }
    }
}
