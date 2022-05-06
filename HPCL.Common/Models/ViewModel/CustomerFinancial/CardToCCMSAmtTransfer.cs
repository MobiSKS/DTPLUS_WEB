using HPCL.Common.Models.CommonEntity;
using System;

namespace HPCL.Common.Models.ViewModel.CustomerFinancial
{
    public class CardToCCMSAmtTransfer : BaseEntity
    {
        public string CustomerId { get; set; }
        public cardToCCMSTransfer[] cardToCCMSTransfer { get; set; }
    }

    public class cardToCCMSTransfer
    {
        public string cardNo { get; set; }
        public Decimal transferAmount { get; set; }
    }
}
