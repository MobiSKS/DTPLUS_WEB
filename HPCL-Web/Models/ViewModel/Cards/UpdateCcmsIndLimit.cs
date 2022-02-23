using HPCL_Web.Models.CommonEntity;

namespace HPCL_Web.Models.ViewModel.Cards
{
    public class UpdateCcmsIndLimit : BaseEntity
    {
        public ObjCCMSLimits[] ObjCCMSLimits { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class ObjCCMSLimits
    {
        public string Cardno { get; set; }
        public string Mobileno { get; set; }
        public int Limittype { get; set; }
        public int Amount { get; set; }
    }
}
