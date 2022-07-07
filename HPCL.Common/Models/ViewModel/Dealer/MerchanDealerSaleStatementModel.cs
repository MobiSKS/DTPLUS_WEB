using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Dealer
{
 
    public class MerchanDealerSaleStatementModel : CommonResponseBase
    {
        public MerchanDealerSaleStatementModel()
        {
            Data = new MerchanDealerSaleStatement();
        }
       public MerchanDealerSaleStatement Data { get; set; }
    }
    public class MerchanDealerSaleStatement
    {
        public MerchanDealerSaleStatement()
        {
            ViewStatementDetails = new List<SaleStatementDetails>();
            TransactionDetails = new List<SaleStatementSummary>();
        }
        public List<SaleStatementSummary> TransactionDetails  { get; set; }
        public List<SaleStatementDetails> ViewStatementDetails { get; set; }
    }
    public class SaleStatementSummary
    {
        public string StatementDate { get; set; }
        public string StatementPeriod { get; set; }
        public string Purchase { get; set; }
        public string Payment { get; set; }
        public string PreviousOutstanding { get; set; }
        public string AmountDue { get; set; }

    }
   
    public class SaleStatementDetails
    {
        
        public string CustomerID { get; set; }
        public string MerchantID { get; set; }
        public string TransactionType { get; set; }
        public string AccountNumber { get; set; }
        public string TransactionDate { get; set; }
        public string TokenNumber { get; set; }
        public string TransactionAmount { get; set; }
        public string Balance { get; set; }
   

    }
}
