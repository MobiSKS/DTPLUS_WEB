using HPCL.Common.Models.CommonEntity;
using System;

namespace HPCL.Common.Models.ViewModel.CustomerFinancial
{
    public class CardToCardAmountTransfer : BaseEntity
    {
        public string CustomerId { get; set; }
        public cardToCardTransfer[] cardToCardTransfer { get; set; }
    }

    public class cardToCardTransfer
    {
        public string FromCardNo { get; set; }
        public string ToCardNo { get; set; }
        public Decimal TransferAmount { get; set; }
        public string FromMobileNo { get; set; }
        public string ToMobileNo { get; set; }
    }
}
