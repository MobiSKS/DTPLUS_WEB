using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.MerchantFinancial
{
    public class BatchDetailsResponse : CommonResponseBase
    {
        public BatchDetailsResponse()
        {
            BatchDetailsResponseData = new List<BatchDetailsResponseData>();
        }
        public virtual List<BatchDetailsResponseData> BatchDetailsResponseData { get; set; }
        public string BatchId { get; set; }
        public string TerminalId { get; set; }
    }

    public class BatchDetailsResponseData
    {
        public string InvoiceNo { get; set; }
        public string CardNo { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string InvoiceAmount { get; set; }
        public string ProductName { get; set; }
        public string FuelPrice { get; set; }
        public string ServiceCharge { get; set; }
        public string CcmsCashBalance { get; set; }
        public string VoidedRoc { get; set; }
        public string VoidedByRoc { get; set; }
        public string Volume { get; set; }

    }
}
