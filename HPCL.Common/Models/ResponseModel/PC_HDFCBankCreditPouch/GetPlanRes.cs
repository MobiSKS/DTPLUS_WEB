using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.PC_HDFCBankCreditPouch
{
    public class GetPlanRes : ResponseMsg
    {
        public List<GetPlanResData> data { get; set; }
    }

    public class GetPlanResData
    {
        public string PlanName { get; set; }
        public int PlanId { get; set; }
    }
}
