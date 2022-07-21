using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.ParentCustomer
{
    public  class ViewParentChildTransactionDetailsModel:CommonResponseBase
    {
        public ViewParentChildTransactionDetailsModel()
        {
           
            FromDate = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
            ChildCustomerIds = new List<ChildCustomerDetails>();
            SelectedChildCustomerIds = new List<ChildCustomerDetails>();
            Data = new TransactionData();
        }
        public string ParentCustomerID { get; set; }
        public string ChildCustomerId { get; set; }
       
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public virtual List<ChildCustomerDetails> ChildCustomerIds { get; set; }
        public virtual List<ChildCustomerDetails> SelectedChildCustomerIds { get; set; }
        public TransactionData Data { get; set; }

    }
    public class TransactionData
    {
        public TransactionData()
        {
            GetParentTransactionsSaleDetails = new List<TransactionDetailsSummary>();
            GetParentTransactionsDetailSummary = new List<TransactionDetails>();
            GetChildTransactionsDetailSummary = new List<TransactionDetails>();
        }
        public List<TransactionDetailsSummary> GetParentTransactionsSaleDetails { get; set; }
        public List<TransactionDetails> GetParentTransactionsDetailSummary { get; set; }
        public List<TransactionDetails> GetChildTransactionsDetailSummary { get; set; }
    }
    public class TransactionDetails
    {
        public string TransactionID { get; set; }
        public string CustomerID { get; set; }
        public string BatchIdandROC { get; set; }
        public string RetailOutletName { get; set; }
        public string TerminalId { get; set; }
        public string Merchant { get; set; }
        public string AccountNumber { get; set; }
        public string VechileNo { get; set; }
        public string TxnDate { get; set; }
        public string TxnType { get; set; }
        public string Volume { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public string ServiceCharge { get; set; }
        public string Amount { get; set; }
        public string Discount { get; set; }
        public string OdometerReading { get; set; }
        public string TrStatus { get; set; }

    }

   
    public class TransactionDetailsSummary
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public string Sale { get; set; }
        public string ReloadCcmsCash { get; set; }
        public string CcmsRecharge { get; set; }
    }
   
}
