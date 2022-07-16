using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.JCB
{
    public class JCBOTCCardDealerAllocationResponse : CommonResponseBase
    {
        public JCBOTCCardResponseData Data { get; set; }

        public JCBOTCCardDealerAllocationResponse()
        {
            Data = new JCBOTCCardResponseData();
        }
    }

    public class JCBOTCCardResponseData
    {
        public JCBOTCCardResponseData()
        {
            ObjJCBTotalCardDetail = new List<JCBOTCCardDealerTotalCardDetail>();
            ObjJCBViewCardDetail = new List<JCBOTCCardDealerViewCardDetail>();
        }
        public List<JCBOTCCardDealerTotalCardDetail> ObjJCBTotalCardDetail { get; set; }
        public List<JCBOTCCardDealerViewCardDetail> ObjJCBViewCardDetail { get; set; }
    }

    public class JCBOTCCardDealerViewCardDetail
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
    public class JCBOTCCardDealerTotalCardDetail
    {
        public string TotalAllocatedCards { get; set; }
        public string TotalMappedCards { get; set; }
        public string TotalUnmappedCards { get; set; }
    }
}