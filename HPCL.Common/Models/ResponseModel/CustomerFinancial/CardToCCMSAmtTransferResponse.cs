using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.CustomerFinancial
{
    public class CardToCCMSAmtTransferResponse : ResponseMsg
    {
        public List<CardToCCMSAmtTransferResponseData> data { get; set; }
    }

    public class CardToCCMSAmtTransferResponseData
    {
        public string CardNo { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
