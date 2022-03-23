using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.MerchantFinancials
{
    public class MerchantERPReloadSaleEarningRequest : BaseEntity
    {
        public string MerchantId { get; set; }
        public string TerminalId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
