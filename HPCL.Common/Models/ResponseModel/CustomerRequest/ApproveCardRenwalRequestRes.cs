using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.CustomerRequest
{
    public class ApproveCardRenwalRequestRes : ResponseMsg
    {
        public List<ApproveCardRenwalRequestResData> data { get; set; }
    }

    public class ApproveCardRenwalRequestResData
    {
        public string CustomerId { get;set; }
        public string CardNo { get; set; }
        public string RequestDate { get; set; }
        public string StatusFlag { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedTime { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedTime { get; set; }
        public int RenewalStatus { get; set; }
        public int RenewalRemark { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
