using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.ManageRbe
{
    public class ChangeRbeMappingByUserNameResponse : ResponseMsg
    {
        public List<ChangeRbeMappingByUserNameResponseData> data { get; set; }
    }

    public class ChangeRbeMappingByUserNameResponseData
    {
        public string OTP { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
