using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.DtpSupport
{
    public class GetEntityOldFieldValueResponse : CommonResponseBase
    {
        public List<GetEntityOldFieldValueResponsedata> data { get; set; }
    }
    public class GetEntityOldFieldValueResponsedata
    {
        public string OldValue { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }        
    }
}