using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.Cards
{
    public class GetCardWiseLimitRes : ResponseMsg
    {
        public List<GetCardWiseLimitResData> Data { get; set; }
    }

    public class GetCardWiseLimitResData
    {
        public string CustomerName { get; set; }
        public string CardNumber { get; set; }
        public string NameOnCard { get; set; }
        public string VehicleNo { get; set; }
        public string LimitType { get; set; }
        public string OldCCMSLimitOption { get; set; }
        public string NewCCMSLimitOption { get; set; }
        public string OldCCMSLimitValue { get; set; }
        public string NewCCMSLimitValue { get; set; }
        public string OldOneTimeLimit { get; set; }
        public string NewOneTimeLimit { get; set; }
        public string OldDailyLimit { get; set; }
        public string NewDailyLimit { get; set; }
        public string OldMonthlyLimit { get; set; }
        public string NewMonthlyLimit { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
