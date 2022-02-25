using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Merchant
{
    public class GetVerifyMerchantListRequestModal : BaseEntity
    {
        public string Category { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
