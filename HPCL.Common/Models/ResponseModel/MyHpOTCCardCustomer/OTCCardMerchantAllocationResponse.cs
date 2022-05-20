using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer
{
    public class OTCCardMerchantAllocationResponse : CommonResponseBase
    {
        public OTCCardResponseData Data { get; set; }
    }

    public class OTCCardResponseData
    {
        public OTCCardResponseData()
        {
            ObjMerchantTotalCardDetail = new List<OTCCardMerchantTotalCardDetail>();
            ObjMerchantViewCardDetail = new List<OTCCardMerchantViewCardDetail>();
        }
        public List<OTCCardMerchantTotalCardDetail> ObjMerchantTotalCardDetail { get; set; }
        public List<OTCCardMerchantViewCardDetail> ObjMerchantViewCardDetail { get; set; }
    }

    public class OTCCardMerchantViewCardDetail
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
    }
    public class OTCCardMerchantTotalCardDetail
    {
        public string TotalAllocatedCards { get; set; }
        public string TotalMappedCards { get; set; }
        public string TotalUnmappedCards { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
