using HPCL.Common.Models.CommonEntity;
using System;

namespace HPCL.Common.Models.ViewModel.CustomerFinancial
{
    public class AmountTransferExcel : BaseEntity
    {
        public string CustomerId { get; set; }
        public Decimal AvailableCcmsBalance { get; set; }
    }
}
