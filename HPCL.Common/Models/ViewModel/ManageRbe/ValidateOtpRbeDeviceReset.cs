using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.ManageRbe
{
    public class ValidateOtpRbeDeviceReset : BaseEntity
    {
        public string MobileNo { get;set; }
        public string OTP { get; set; }
    }
}
