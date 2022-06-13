using HPCL.Common.Models.ResponseModel.CommonResponse;
using System;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.HDFCBankCreditPouch
{
    public class SearchRequestApprovalRes : ResponseMsg
    {
        public List<SearchRequestApprovalResData> data { get; set; }
    }

    public class SearchRequestApprovalResData
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string NameOnCard { get; set; }
        public string LastTransactionDate { get; set; }
        public Decimal TotalSpend { get; set; }
        public string RO { get; set; }
        public string ZO { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedDate { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }
        public string ReferenceNo { get; set; }
        public int RequestNo { get; set; }
        public string ActionStatus { get; set; }
        public string PlanName { get; set; }
    }
}
