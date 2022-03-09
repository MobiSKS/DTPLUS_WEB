using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.Cards
{
    public class SearchCcmsLimitAllResponse : ResponseMsg
    {
        public List<AllLimitCardResponse> data { get; set; }
    }

    public class AllLimitCardResponse
    {
        public int ActualCCMSBalance { get; set; }
        public int UnallocatedCCMSBalance { get; set; }
    }
}
