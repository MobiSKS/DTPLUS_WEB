using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.VolvoEicher
{
    public class VEOTCCardDealerAllocationResponse : CommonResponseBase
    {
        public VEOTCCardResponseData Data { get; set; }
    }

    public class VEOTCCardResponseData
    {
        public VEOTCCardResponseData()
        {
            ObjVETotalCardDetail = new List<VEOTCCardDealerTotalCardDetail>();
            ObjVEViewCardDetail = new List<VEOTCCardDealerViewCardDetail>();
        }
        public List<VEOTCCardDealerTotalCardDetail> ObjVETotalCardDetail { get; set; }
        public List<VEOTCCardDealerViewCardDetail> ObjVEViewCardDetail { get; set; }
    }

    public class VEOTCCardDealerViewCardDetail
    {
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CardNo { get; set; }
        public string MerchantId { get; set; }
        public string AllocationDate { get; set; }
        public string MappingDate { get; set; }
        public string Status { get; set; }
        public string RetailOutletName { get; set; }
        public string DealerCode { get; set; }
    }
    public class VEOTCCardDealerTotalCardDetail
    {
        public string TotalAllocatedCards { get; set; }
        public string TotalMappedCards { get; set; }
        public string TotalUnmappedCards { get; set; }
    }
}