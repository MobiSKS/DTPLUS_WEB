using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.ManageRbe
{
    public class GetSendOtpChangeRbeMobileResponse:ResponseMsg
    {
       public List<GetSendOtpChangeRbeMobileResponseData> data { get; set; }
    }
    public class GetSendOtpChangeRbeMobileResponseData
    {
        public string OTP { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
