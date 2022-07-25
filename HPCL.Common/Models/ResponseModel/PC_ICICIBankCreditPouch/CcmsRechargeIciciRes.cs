using HPCL.Common.Models.ResponseModel.CommonResponse;

namespace HPCL.Common.Models.ResponseModel.PC_ICICIBankCreditPouch
{
    public class CcmsRechargeAmexRes : ResponseMsg
    {
        public CcmsRechargeHdfcResData data { get; set; }
    }

    public class CcmsRechargeHdfcResData
    {
        public string OrderId { get; set; }
        public CcmsRechargeHdfcResDataResponse response { get; set; }
    }

    public class CcmsRechargeHdfcResDataResponse
    {
        public string bankName { get; set; }
        public string transactionId { get; set; }
        public string request { get; set; }
        public string response { get; set; }
        public string apiurl { get; set; }
        public string userId { get; set; }
        public string request_Hash { get; set; }
        public string accessCode { get; set; }
    }
}
