using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.AshokLayland
{
    public class SalesExecutiveEmployeeIDResponse : CommonResponseBase
    {
        public List<SalesExecutiveEmployeeIDResponseData> Data { get; set; }
    }

    public class SalesExecutiveEmployeeIDResponseData
    {
        public string SalesExecutiveEmployeeID { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }
}