using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.CustomerFinancial
{
    public class GetViewAccountStatementResponse : ResponseMsg
    {
        public GetViewAccountStatementResponseData data { get; set; }
    }

    public class GetViewAccountStatementResponseData
    {
        public List<GetCcmsAccountSummary> getCcmsAccountSummary { get; set; }
        public List<GetCardTransactionDetails> getCardTransactionDetails { get; set; }
    }

    public class GetCcmsAccountSummary
    {
        public decimal Credits { get; set; }
        public decimal Debits { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
    }

    public class GetCardTransactionDetails
    {
        public string CardNo { get; set; }
        public string VechileNo { get; set; }
        public string TransactionType { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public decimal Amount { get; set; }
        public string RewardType { get; set; }
        public string Date { get; set; }
    }
}
