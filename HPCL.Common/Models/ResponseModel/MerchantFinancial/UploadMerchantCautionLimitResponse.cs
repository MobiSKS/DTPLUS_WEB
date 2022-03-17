using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.MerchantFinancial
{
    public class UploadMerchantCautionLimitResponse : ResponseMsg
    {
        public List<UploadMerchantCautionLimitResponseData> data { get; set; }
    }

    public class UploadMerchantCautionLimitResponseData
    {
        public int SrNumber { get; set; }
        public string MerchantId { get; set; }
        public string RetailOutletName { get; set; }
        public string RegionalOfficeName { get; set; }
        public string SalesArea { get; set; }
        public decimal AvgHsdSale { get; set; }
        public decimal HsdRspValue { get; set; }
        public decimal DtplusCautionLimit { get; set; }
        public decimal AvgMsSale { get; set; }
        public decimal MsRspValue { get; set; }
        public decimal HpPayCautionLimit { get; set; }
        public string LastUpdatedOn { get; set; }
        public string StatusName { get; set; }
    }
}
