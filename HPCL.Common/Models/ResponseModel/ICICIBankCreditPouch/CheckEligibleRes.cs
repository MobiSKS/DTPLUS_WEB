using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.ICICIBankCreditPouch
{
    public class CheckEligibleRes : ResponseMsg
    {
        public List<CheckEligibleResData> data { get; set; }
    }

    public class CheckEligibleResData
    {
        public string PlanName { get; set; }
        public int PlanTypeId { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
