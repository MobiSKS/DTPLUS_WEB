using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.JCB
{
    public class GetJCBDealerOTCCardStatusResponse : CommonResponseBase
    {
        public GetJCBDealerOTCCardStatusResponse()
        {
            Data = new List<JCBDealerOTCCardStatusDetails>();
        }
        public List<JCBDealerOTCCardStatusDetails> Data { get; set; }
    }

    public class JCBDealerOTCCardStatusDetails
    {
        public string TotalAllocatedCards { get; set; }
        public string TotalMappedCards { get; set; }
        public string TotalUnmappedCards { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string RequestID { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
        public string NoofCards { get; set; }
        public string RequestedDate { get; set; }
    }
}