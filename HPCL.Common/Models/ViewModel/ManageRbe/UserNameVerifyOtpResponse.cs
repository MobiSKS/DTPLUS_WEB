using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.ManageRbe
{
    public class UserNameVerifyOtpResponse : BaseEntity
    {
        public string PreRBEUserName { get; set; }
        public string NewRBEUserName { get; set; }
        public string OTP { get; set; }
    }
}
