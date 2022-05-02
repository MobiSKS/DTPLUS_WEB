using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.ManageRbe
{
    public class GetApproveChangeRbeDeviceResetResponse : ResponseMsg
    {
        public List<GetApproveChangeRbeDeviceResetResponseData> data { get; set; }
    }

    public class GetApproveChangeRbeDeviceResetResponseData
    {
        public string RBEID { get; set; }
        public string RBEName { get; set; }
        public string MobileNo { get; set; }
        public string Region { get; set; }
        public string RBEDeviceReset { get; set; }
    }
}
