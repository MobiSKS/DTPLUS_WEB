using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.ManageRbe
{
    public class VerifyManageMobileOtp : BaseEntity
    {
        public string ExistingMobileNo { get; set; }
        public string NewMobileNo { get; set; }
        public string OTP { get; set; }
    }
}
