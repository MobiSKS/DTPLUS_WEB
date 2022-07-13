using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Dealer
{
    public class DealerCreditSaleViewModel:CommonResponseBase
    {
        public DealerCreditSaleViewModel()
        {
            Data = new List<GetDealerCreditSaleView>();
            FromDate = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
        public string CustomerID { get; set; }
        public string MerchantID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<GetDealerCreditSaleView> Data { get; set; }
    }
    public class GetDealerCreditSaleView
    {
        public string MerchantID { get;set;}
        public string OutletNameAndLocation { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string TransactionType { get; set; }
        public string AccountNumber { get; set; }
        public string TransactionAmount { get; set; }
        public string TransactionDate { get; set; }
        public string Balance { get; set; }
        public string RSP { get; set; }
        public string TokenNumber { get; set; }
 
    }
    public class StatementDateModel
    {
        public string StatementDate { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
