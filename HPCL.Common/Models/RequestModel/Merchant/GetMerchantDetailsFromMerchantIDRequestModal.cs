using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Merchant
{
    public class GetMerchantDetailsFromMerchantIDRequestModal : BaseEntity
    {
        public string MerchantId { get; set; }
    }
}
