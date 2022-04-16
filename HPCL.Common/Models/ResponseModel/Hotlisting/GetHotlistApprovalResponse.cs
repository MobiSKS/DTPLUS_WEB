using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.Hotlisting
{
    public  class GetHotlistApprovalResponse:CommonResponseBase
    {
        public virtual List<GetHotlistApprovalDetails> Data { get; set; }   
    }
    public class GetHotlistApprovalDetails
    {
        public string EntityCode { get; set; }
        public string CreationDate { get; set; }
    }
}
