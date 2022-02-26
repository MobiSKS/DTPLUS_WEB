using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Helper
{
    public class CommonResponseBase
    {
        public Boolean Success { get; set; }
        public int Status_Code { get; set; }
        public int Internel_Status_Code { get; set; }
        public string Message { get; set; }
    }
}
