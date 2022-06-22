using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.VolvoEicher
{
    public class VEDealerOTCCardStatusViewModel : CommonResponseBase
    {
        public string DealerCode { get; set; }
        public List<VEDealerOTCCardStatusData> Data { get; set; }
    }
    public class VEDealerOTCCardStatusData
    {
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string TotalAllocatedCards { get; set; }
        public string TotalMappedCards { get; set; }
        public string TotalUnmappedCards { get; set; }
    }
}
