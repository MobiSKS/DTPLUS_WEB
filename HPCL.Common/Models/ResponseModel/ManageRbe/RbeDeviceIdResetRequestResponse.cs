using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.ManageRbe
{
    public class RbeDeviceIdResetRequestResponse : ResponseMsg
    {
        public List<RbeDeviceIdResetRequestResponseData> data { get; set; }
    }

    public class RbeDeviceIdResetRequestResponseData
    {
        public string RBEID { get; set; }
        public string MobileNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string EmailId { get; set; } 
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Action { get; set; }
    }
}
