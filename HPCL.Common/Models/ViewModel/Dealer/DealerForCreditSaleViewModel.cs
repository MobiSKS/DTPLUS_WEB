using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Dealer
{
    public class DealerForCreditSaleViewModel:BaseEntity
    {
        public DealerForCreditSaleViewModel()
        {
            TypeMapDealerForCreditSale = new List<TypeMapDealerForCreditSale>();
        }
        public string CustomerID { get; set; }
        public string NoofEntries { get; set; }
        public string MerchantID { get; set; }
        public string EffectiveDate { get; set; }
        public string CreditPeriod { get; set; }
        public List<TypeMapDealerForCreditSale> TypeMapDealerForCreditSale { get; set; }

    }
    public class TypeMapDealerForCreditSale
    {
        public string MerchantID { get; set; }
        public string EffectiveDate { get; set; }
        public string CreditPeriod { get; set; }
    }
}
