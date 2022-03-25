using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel.MerchantFinancials
{
    public class MerchantReceivablePayableModel
    {
        public string MerchantId { get; set; }
        public string TerminalOrMerchant { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<MerchantReceivablePayableDetails> ReceivablePayableDetails { get; set; }
        public string Message { get; set; }
        public MerchantReceivablePayableModel()
        {
            ReceivablePayableDetails = new List<MerchantReceivablePayableDetails>();
        }
    }

    public class MerchantReceivablePayableDetails
    {
        public string SrNumber { get; set; }
        public string TerminalId { get; set; }
        public string BatchId { get; set; }
        public string SettlementDate { get; set; }
        public string Receivable { get; set; }
        public string Payable { get; set; }
    }
}