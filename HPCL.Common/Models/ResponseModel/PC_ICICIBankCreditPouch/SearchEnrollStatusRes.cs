using HPCL.Common.Models.ResponseModel.CommonResponse;
using System;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.PC_ICICIBankCreditPouch
{
    public class SearchEnrollStatusRes : ResponseMsg
    {
        public List<SearchEnrollStatusResData> data { get; set; }
    }

    public class SearchEnrollStatusResData
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string RO { get; set; }
        public string ZO { get; set; }
        public string ApproverRemark { get; set; }
        public string EnrolledBy { get; set; }
        public string EnrolledDate { get; set; }
        public string AuthorizerRemark { get; set; }
        public int RequestNo { get; set; }
        public string Status { get; set; }
        public Decimal CreditLimitAssigned { get; set; }
        public string BankName { get; set; }
        public string PlanName { get; set; }
    }
}
