using HPCL.Common.Models.ResponseModel.CommonResponse;
using System;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.Cards
{
    public class SearchCcmsLimitAllResponse : ResponseMsg
    {
        public List<AllLimitCardResponse> data { get; set; }
    }

    public class AllLimitCardResponse
    {
        public Decimal ActualCCMSBalance { get; set; }
        public Decimal UnallocatedCCMSBalance { get; set; }
        public int CCMSUnlimitedStatus { get; set; }
    }
}
