using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.CustomerFinancial
{
    public class CardToCardAmountTransferRes : ResponseMsg
    {
        public List<CardToCardAmountTransferResData> data { get; set; }
    }

    public class CardToCardAmountTransferResData
    {
        public string FromCardNo { get; set; }
        public string ToCardNo { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
