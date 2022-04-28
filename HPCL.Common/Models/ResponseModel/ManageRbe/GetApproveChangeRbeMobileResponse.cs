using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.ManageRbe
{
    public class GetApproveChangeRbeMobileResponse : ResponseMsg
    {
        public List<GetApproveChangeRbeMobileResponseData> data { get; set; }
    }

    public class GetApproveChangeRbeMobileResponseData
    {
        public string RBEID { get; set; }
        public string RBEName { get; set; }
        public string ExistingMobileNo { get; set; }
        public string NewMobileNo { get; set; }
        public string Region { get; set; }
        public string Status { get; set; }
        public string ChangeRBEMobile { get; set; }
    }
}
