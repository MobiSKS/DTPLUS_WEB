using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.DriverCardCustomer
{
    public class DriverCardMerchantAllocationResponse : CommonResponseBase
    {
        public DriverCardResponseData Data { get; set; }
    }

    public class DriverCardResponseData
    {
        public DriverCardResponseData()
        {
            ObjMerchantTotalCardDetail = new List<DriverCardMerchantTotalCardDetail>();
            ObjMerchantViewCardDetail = new List<DriverCardMerchantViewCardDetail>();
        }
        public List<DriverCardMerchantTotalCardDetail> ObjMerchantTotalCardDetail { get; set; }
        public List<DriverCardMerchantViewCardDetail> ObjMerchantViewCardDetail { get; set; }
    }

    public class DriverCardMerchantViewCardDetail
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
    public class DriverCardMerchantTotalCardDetail
    {
        public string TotalAllocatedCards { get; set; }
        public string TotalMappedCards { get; set; }
        public string TotalUnmappedCards { get; set; }
    }

}
