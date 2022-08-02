using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.JCB
{
    public class JCBCustomerTransactionResponseModel : CommonResponseBase
    {
        public JCBCustomerTransactionResponseModel()
        {
            GetTransactionsSaleSummary = new List<JCBCustomerTransactionSummary>();
            GetTransactionsDetailSummary = new List<JCBCustomerTransactionDetails>();
        }
        public List<JCBCustomerTransactionSummary> GetTransactionsSaleSummary { get; set; }
        public List<JCBCustomerTransactionDetails> GetTransactionsDetailSummary { get; set; }
    }
    public class JCBCustomerTransactionSummary
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public string Sale { get; set; }
        public string ReloadCcmsCash { get; set; }
        public string CcmsRecharge { get; set; }
    }
    public class JCBCustomerTransactionDetails
    {
        public string SrNumber { get; set; }
        public string TerminalId { get; set; }
        public string MerchantId { get; set; }
        public string BatchIdandROC { get; set; }
        public string AccountNumber { get; set; }
        public string VechileNo { get; set; }
        public string TxnDate { get; set; }
        public string TxnType { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string Volume { get; set; }
        public string Currency { get; set; }
        public string ServiceCharge { get; set; }
        public Decimal Amount { get; set; }
        public string Discount { get; set; }
        public string OdometerReading { get; set; }
        public string Status { get; set; }
        public string RetailOutletName { get; set; }

    }
}