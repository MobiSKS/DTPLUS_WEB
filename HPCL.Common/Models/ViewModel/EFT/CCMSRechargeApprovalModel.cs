using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.EFT
{
    public class CCMSRechargeApprovalModel : CommonResponseBase
    {
        public CCMSRechargeApprovalModel()
        {
            RechargeRequestTypes = new List<RechargeRequestType>();
            FromDate = DateTime.Now.ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
            Data = new List<RechargeApprovalDetails>();
        }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string BatchCode { get; set; }
        public string ControlCardNumber { get; set; }
        public string UTRNumber { get; set; }
        public string ICICIRefNo { get; set; }
        public string RechargeRequestTypeId { get; set; }
        public List<RechargeRequestType> RechargeRequestTypes { get; set; }
        public List<RechargeApprovalDetails> Data { get; set; }
    }
    public class RechargeApprovalDetails
    {
        public string CustomerId { get; set; }
        public string ControlCardNumber { get; set; }
        public string UTRNumber { get; set; }
        public string TxnDate { get; set; }
        public string Amount { get; set; }
        public string ICICIRefNo { get; set; }
        public string BatchCode { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedOn { get; set; }
        public string Remarks { get; set; }

    }
    public class RechargeRequestType
    {
        public string RequestTypeId { get; set; }
        public string RequestType { get; set; }
    }
}
