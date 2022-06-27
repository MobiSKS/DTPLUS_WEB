using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.CCMSRecharge
{
    public class CCCMSRecGenerateOtpReq : BaseEntity
    {
        public string Merchantid { get; set; }
        public string Terminalid { get; set; }
        public string Mobileno { get; set; }
        public int OTPtype { get; set; }
    }
}
