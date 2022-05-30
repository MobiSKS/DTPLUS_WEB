using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.CustomerFinancial
{
    public class CCMSToCardAmtTransferResponse : ResponseMsg
    {
        public List<CCMSToCardAmtTransferResponseData> data { get; set; }
    }

    public class CCMSToCardAmtTransferResponseData
    {
        public string CardNo { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
