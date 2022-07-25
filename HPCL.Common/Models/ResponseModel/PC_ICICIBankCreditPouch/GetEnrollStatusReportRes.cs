using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.PC_ICICIBankCreditPouch
{
    public class GetEnrollStatusReportRes : ResponseMsg
    {
        public List<GetEnrollStatusReportResData> data { get; set; }
    }

    public class GetEnrollStatusReportResData
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Status { get; set; }
        public int RequestNo { get; set; }
        public string PlanName { get; set; }
    }
}
