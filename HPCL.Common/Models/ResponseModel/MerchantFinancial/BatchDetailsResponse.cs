using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.MerchantFinancial
{
    public class BatchDetailsResponse : ResponseMsg
    {
        public BatchDetailsResponse()
        {
            data = new List<BatchDetailsResponseData>();
        }
        public virtual List<BatchDetailsResponseData> data { get; set; }
    }

    public class BatchDetailsResponseData
    {
        public int InvoiceNo { get; set; }
        public string CardNo { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string InvoiceAmount { get; set; }
        public string ProductName { get; set; }
        public decimal FuelPrice { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal CcmsCashBalance { get; set; }
        public int VoidedRoc { get; set; }
        public int VoidedByRoc { get; set; }
        public decimal Volume { get; set; }

    }
}
