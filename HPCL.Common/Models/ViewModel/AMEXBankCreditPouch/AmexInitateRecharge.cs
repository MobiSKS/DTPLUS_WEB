using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ViewModel.AMEXBankCreditPouch
{
    public class AmexInitateRecharge : ResponseMsg
    {
        public List<HdfcInitateRechargeData> data { get; set; }
    }

    public class HdfcInitateRechargeData
    {
        public string OrderId { get; set; }
        public string Response { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
