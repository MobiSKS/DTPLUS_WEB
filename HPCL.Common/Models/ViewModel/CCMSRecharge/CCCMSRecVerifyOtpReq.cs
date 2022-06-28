using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.CCMSRecharge
{
    public class CCCMSRecVerifyOtpReq : BaseEntity
    {
        public string MobileNo { get;set; }
        public string otp { get;set; }
    }
}
