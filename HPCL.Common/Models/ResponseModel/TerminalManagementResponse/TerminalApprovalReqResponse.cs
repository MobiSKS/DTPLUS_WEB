namespace HPCL.Common.Models.ResponseModel.TerminalManagementResponse
{
    public class TerminalApprovalReqResponse
    {
        public string MerchantId { get; set; }
        public string TerminalId { get; set; }
        public string TerminalType { get; set; }
        public string MerchantStatus { get; set; }
        public double LastMonthSpend { get; set; }
        public int NoOfTransactionsOfLastMonth { get; set; }
        public string RequestType { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedDate { get; set; }
        public string Justification { get; set; }
    }
}
