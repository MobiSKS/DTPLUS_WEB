using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Dealer
{
    public class DealerCreditClosePaymentModel : CommonResponseBase
    {
        public DealerCreditClosePaymentModel()
        {
            Data = new List<DealerCreditClosePaymenDetails>();
        }
        public List<DealerCreditClosePaymenDetails> Data { get; set; }

    }
    public class DealerCreditClosePaymenDetails
    {
        public string MerchantID { get; set; }
        public string RetailOutletName { get; set; }
        public string CustomerId { get; set; }
        public string IndividualOrgName { get; set; }
        public string Outstanding { get; set; }
        public string Limit { get; set; }
        public string LimitBalance { get; set; }
        public string Amount { get; set; }
        public string SourceofPayment { get; set; }
        public string OTP { get; set; }
        public string CustomerID { get; set; }
    }
}