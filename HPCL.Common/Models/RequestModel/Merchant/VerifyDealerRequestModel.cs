using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Merchant
{
    public class VerifyDealerRequestModel : BaseEntity
    {
        public string DealerCode { get; set; }
    }

}