using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.ManageRbe
{
    public class GetApproveChnagedRbeMappingResponse : ResponseMsg
    {
        public List<GetApproveChnagedRbeMappingResponseData> data { get; set; }
    }

    public class GetApproveChnagedRbeMappingResponseData
    {
        public string RBEID { get; set; }
        public string NewRBEUser { get; set; }
        public string NewUserName { get; set; }
        public string PreRBEUser { get; set; }
        public string PreUserName { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string status { get; set; }
        public string ChangeRBE { get; set; }
    }
}
