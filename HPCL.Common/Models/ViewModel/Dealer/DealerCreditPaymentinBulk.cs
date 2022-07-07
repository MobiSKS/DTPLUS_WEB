using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Dealer
{
    public class DealerCreditPaymentinBulk
    {
        public DealerCreditPaymentinBulk()
        {
            Data = new List<GetDealerCreditPaymentInBulk>();
        }
        public List<GetDealerCreditPaymentInBulk> Data { get; set; }
    }
    public class GetDealerCreditPaymentInBulk
    {
        public string MerchantID { get; set; }
        public string CustomerID { get; set; }
        public string OutletNameAndLocation { get; set; }
        public string Outstanding { get; set; }
        public string Amount { get; set; }
        public string RetailOutletName { get; set; }
    }
}
