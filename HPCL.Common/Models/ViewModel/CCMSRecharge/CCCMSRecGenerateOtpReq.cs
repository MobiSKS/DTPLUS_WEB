using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.CCMSRecharge
{
    public class CCCMSRecGenerateOtpReq : BaseEntity
    {
        public string Mobileno { get; set; }
        public string CustomerId { get; set; }
    }
}
