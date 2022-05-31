using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Dealer
{
    public class DealerCreditSaleStatement:CommonResponseBase
    {
        public List<DealerCreditStatementDetails> Data { get; set; }
    }
    public class DealerCreditStatementDetails
    {
        public string CustomerID { get; set; }
        public string MerchantID { get; set; }
        public string TransactionType { get; set; }
        public string TransactionDate { get; set; }
        public string AccountNumber { get; set; }
        public string TokenNumber { get; set; }
        public string TransactionAmount { get; set; }
        public string Balance { get; set; }

    }
}
