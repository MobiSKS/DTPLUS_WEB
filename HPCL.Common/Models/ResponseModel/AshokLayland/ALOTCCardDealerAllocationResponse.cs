using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.AshokLayland
{
    public class ALOTCCardDealerAllocationResponse : CommonResponseBase
    {
        public OTCCardResponseData Data { get; set; }
    }

    public class OTCCardResponseData
    {
        public OTCCardResponseData()
        {
            ObjMerchantTotalCardDetail = new List<ALOTCCardDealerTotalCardDetail>();
            ObjMerchantViewCardDetail = new List<ALOTCCardDealerViewCardDetail>();
        }
        public List<ALOTCCardDealerTotalCardDetail> ObjMerchantTotalCardDetail { get; set; }
        public List<ALOTCCardDealerViewCardDetail> ObjMerchantViewCardDetail { get; set; }
    }

    public class ALOTCCardDealerViewCardDetail
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
    }
    public class ALOTCCardDealerTotalCardDetail
    {
        public string TotalAllocatedCards { get; set; }
        public string TotalMappedCards { get; set; }
        public string TotalUnmappedCards { get; set; }
    }
}
