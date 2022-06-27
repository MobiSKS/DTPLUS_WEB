using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.CCMSRecharge
{
    public class CCCMSRecGenerateOtpRes : ResponseMsg
    {
        public List<CCCMSRecGenerateOtpResData> data { get; set; }
    }

    public class CCCMSRecGenerateOtpResData
    {
        public string OTP { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
