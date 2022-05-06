using HPCL.Common.Models.CommonEntity;
using System;

namespace HPCL.Common.Models.ViewModel.CustomerFinancial
{
    public class CardToCardAmountTransfer : BaseEntity
    {
        public string customerId { get; set; }
        public cardToCardTransfer[] cardToCardTransfer { get; set; }
    }

    public class cardToCardTransfer
    {
        public string fromCardNo { get; set; }
        public string toCardNo { get; set; }
        public Decimal transferAmount { get; set; }
    }
}
