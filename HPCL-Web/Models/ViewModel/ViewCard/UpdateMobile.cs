using HPCL_Web.Models.CommonEntity;

namespace HPCL_Web.Models.ViewModel.ViewCard
{
    public class UpdateMobile : BaseEntity
    {


        public ObjCardLimits[] objCardLimits { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class ObjCardLimits
    {
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
        public string FastagNo { get; set; }
    }

    public class UpdateMobileResponse
    {
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
