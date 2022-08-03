using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.EFT
{
    public class CCMSRechargeSummaryViewModel:CommonResponseBase
    {
        public CCMSRechargeSummaryViewModel()
        {
            Data = new List<CCMSRechargeSummaryDetails>();
        }
        public string TotalRecords { get; set; }
        public string TotalValidRecords { get; set; }
        public string TotalAmount { get; set; }
        public string TotalInvalidRecords { get; set; }
        public string TotalAmtInvalidRecords { get; set; }
        public string InvalidRecordsRowNumber { get; set; }
        public List<CCMSRechargeSummaryDetails> Data { get; set; }

    }
    public class CCMSRechargeSummaryDetails
    {
        public string ControlCardNumber { get; set; }
        public string UTRNumber { get; set; }
        public string TransactionDate { get; set; }
        public string PaymentDate { get; set; }
        public string Amount { get; set; }
        public string TransactionAmount { get; set; }
        public string ICICIRefNumber{ get; set; }
        public string TransRefNumber { get; set; }
        public string IFSCCode { get; set; }
        public string SenderName { get; set; }
        public string Reason{ get; set; }
        public string Status { get; set; }
    }
}
