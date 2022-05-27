using HPCL.Common.Models.CommonEntity;
using System;

namespace HPCL.Common.Models.ViewModel.CustomerFinancial
{
    public class CCMSToCardsAmountTransfer : BaseEntity
    {
        public string customerId { get; set; }
        public ccmsToCardTransfer[] ccmsToCardTransfer { get; set; }
    }

    public class ccmsToCardTransfer
    {
        public  string cardNo { get; set; }
        public string MobileNo { get; set; }
        public Decimal transferAmount { get; set; }
    }
}
