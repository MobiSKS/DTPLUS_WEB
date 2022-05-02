using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.TMS
{
    public class EnrollTMSResponse : CommonResponseBase
    {
        public EnrollTMSResponse()
        {
            Data = new EnrollTMSResponseData();
        }
        public EnrollTMSResponseData Data { get; set; }
    }

    public class EnrollTMSResponseData
    {
        public string message { get; set; }
    }
}