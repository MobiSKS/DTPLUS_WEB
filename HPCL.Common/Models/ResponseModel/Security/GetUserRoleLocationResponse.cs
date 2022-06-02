using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.Security
{
    public class GetUserRoleLocationResponse : CommonResponseBase
    {
        public List<UserRoleLocationResponseData> Data { get; set; }
        public GetUserRoleLocationResponse()
        {
            Data = new List<UserRoleLocationResponseData>();
        }
        public string UserName { get; set; }
    }
    public class UserRoleLocationResponseData
    {
        public string UserRole { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}