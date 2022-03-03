using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models
{
    public class CommonResponse : CommonResponseBase
    {
        public List<CommonResponseData> Data { get; set; }
    }

    public class CommonResponseData
    {
        public int Status { get; set; }
        public string Reason { get; set; }
        public int Internel_Status_Code { get; set; }
    }

}
