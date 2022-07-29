using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.DICV
{
    public class GetDICVDealerOTCCardStatusResponse : CommonResponseBase
    {
        public GetDICVDealerOTCCardStatusResponse()
        {
            Data = new List<DICVDealerOTCCardStatusDetails>();
        }
        public List<DICVDealerOTCCardStatusDetails> Data { get; set; }
    }
    public class DICVDealerOTCCardStatusDetails
    {
        public string TotalAllocatedCards { get; set; }
        public string TotalMappedCards { get; set; }
        public string TotalUnmappedCards { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string RequestID { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}