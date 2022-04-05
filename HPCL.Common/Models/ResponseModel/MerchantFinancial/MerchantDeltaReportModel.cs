using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.MerchantFinancial
{
    public class MerchantDeltaReportModel:CommonResponseBase
    {
        public MerchantDeltaReportModel()
        {
            MerchantDeltaReportDetails = new List<MerchantDeltaReportDetails>();
            FromDate = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
        }
        public virtual List<MerchantDeltaReportDetails> MerchantDeltaReportDetails { get; set; }
        public string MerchantId { get; set; }
        public string TerminalId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class MerchantDeltaReportDetails
    {
        public string TerminalId { get; set; }
        public string BatchId { get; set; }
        public string SettlementDate { get; set; }
        public string SaleDelta { get; set; }
        public string ReloadDelta { get; set; }

    }
}
