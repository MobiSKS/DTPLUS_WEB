using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.MerchantFinancial
{
    public class TransactionlDetailsResponse : ResponseMsg
    {
        public List<TransactionlDetailsResponseData> data { get; set; }
    }

    public class TransactionlDetailsResponseData
    { 
        public int BatchId { get; set; }
        public int InvoiceNo { get; set; }
        public string ToDate { get; set; }
        public string RetailOutletName { get; set; }
        public string TerminalId { get; set; }
        public string CardNo { get; set; }
        public string NameOnCard { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal DriveStars { get; set; }
        public string VoidedByRoc { get; set; }
        public string VoidedRoc { get; set; }
        public string FSMName { get; set; }
        public string MobileNo { get; set; }
        public string Status { get; set; }
    }
}
