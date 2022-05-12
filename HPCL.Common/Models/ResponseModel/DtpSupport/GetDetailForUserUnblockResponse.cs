using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.DtpSupport
{
    public class GetDetailForUserUnblockResponse: CommonResponseBase
    {
        public List<GetDetailForUserUnblockResponseData> data { get; set; }
    }
    public class GetDetailForUserUnblockResponseData
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string CreatedTime { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}