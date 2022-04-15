using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.DtpSupport
{
    public class GetBlockUnblockCustomerCcmsAccountByCustomeridResponse : ResponseMsg
    {
        public List<DataResponseArray> data { get; set; }
    }

    public class DataResponseArray
    {
        public string CustomerName { get; set; }
        public int CCMSBalanceStatusId { get; set; }
        public string CCMSBalanceStatusName { get; set; }
        public string CCMSBalanceRemarks { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
