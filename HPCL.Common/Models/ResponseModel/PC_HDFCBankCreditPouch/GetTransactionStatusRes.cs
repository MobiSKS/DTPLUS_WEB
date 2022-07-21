using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;
using System.Numerics;

namespace HPCL.Common.Models.ResponseModel.PC_HDFCBankCreditPouch
{
    public class GetTransactionStatusRes : ResponseMsg
    {
        public List<GetTransactionStatusResData> data { get; set; }
    }

    public class GetTransactionStatusResData
    {
        public string ZO { get; set; }
        public string RO { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string NameOnCard { get; set; }
        public int TrackID { get; set; }
        public string PaymentID { get; set; }
        public string TransactionId { get; set; }
        public string PlanName { get; set; }
        public string RequestAmount { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionStatus { get; set; }
    }
}
