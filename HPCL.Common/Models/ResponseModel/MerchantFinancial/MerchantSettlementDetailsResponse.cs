using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.MerchantFinancial
{
    public class MerchantSettlementDetailsResponse : ResponseMsg
    {
        public List<MerchantSettlementDetailsResponseData> data { get; set; }
    }

    public class MerchantSettlementDetailsResponseData
    {
        public int SrNumber { get; set; }
        public int BatchId { get; set; }
        public string TerminalId { get; set; }
        public string Status { get; set; }
        public string NoofTransactions { get; set; }
        public string SettlementDate { get; set; }
    }

}
