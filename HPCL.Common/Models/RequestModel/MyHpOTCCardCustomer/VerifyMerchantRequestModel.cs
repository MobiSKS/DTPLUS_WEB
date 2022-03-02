using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.MyHpOTCCardCustomer
{
    public class VerifyMerchantRequestModel : BaseEntity
    {
        public string MerchantId { get; set; }
        public string RegionalOfficeId { get; set; }
    }

}
