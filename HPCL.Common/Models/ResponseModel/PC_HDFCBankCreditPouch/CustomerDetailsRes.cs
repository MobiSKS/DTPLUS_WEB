using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.PC_HDFCBankCreditPouch
{
    public class CustomerDetailsRes : ResponseMsg
    {
        public List<CustomerDetailsResData> data { get; set; }
    }

    public class CustomerDetailsResData
    {
        public string CustomerName { get; set; }
        public string NameOnCard { get; set; }
        public string LastTransactionDate { get; set; }
        public string TotalSpend { get; set; }
        public string RO { get; set; }
        public string ZO { get; set; }
        public string CustomerType { get; set; }
        public string RequestedBy { get; set; }
        public string PlanName { get; set; }
        public string ReqStatus { get; set; }
        public string EnrolledDate { get; set; }
        public string Comment { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
