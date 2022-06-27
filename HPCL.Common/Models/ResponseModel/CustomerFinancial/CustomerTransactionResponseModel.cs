using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.CustomerFinancial
{
    public class CustomerTransactionResponseModel :CommonResponseBase
    {
        public CustomerTransactionResponseModel()
        {
            GetTransactionsSaleSummary = new List<CustomerTransactionSummary>();
            GetTransactionsDetailSummary = new List<CustomerTransactionDetails>();
        }
        public List<CustomerTransactionSummary> GetTransactionsSaleSummary { get; set; }
        public List<CustomerTransactionDetails> GetTransactionsDetailSummary { get; set; }
    }
    public class CustomerTransactionSummary
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public string Sale { get; set; }
        public string ReloadCcmsCash { get; set; }
        public string CcmsRecharge { get; set; }
     
    }
    public class CustomerTransactionDetails
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
