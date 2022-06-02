using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.Security
{
    public class GetManageUserResponse : ResponseMsg
    {
        public List<GetManageUserResponseData> data { get; set; }
    }

    public class GetManageUserResponseData
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string LastLoginTime { get; set; }
        public string UserRole { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
