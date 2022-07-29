using HPCL.Common.Models.ResponseModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.Interface
{
    public class RegenerateIACResponseModel 
    {
        public string IACID { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }

    }
}
