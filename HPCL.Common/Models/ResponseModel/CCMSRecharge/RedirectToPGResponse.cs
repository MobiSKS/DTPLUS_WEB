using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.CommonResponse;
using System;

namespace HPCL.Common.Models.ResponseModel.CCMSRecharge
{
    public class RedirectToPGResponse : ResponseMsg
    {
        public RedirectToPGResponseData data { get; set; }
    }

    public class RedirectToPGResponseData
    {
        public string OrderId { get; set; }
        public RedirectToPGResponseDataResponse response { get; set; }
    }

    public class RedirectToPGResponseDataResponse
    {
        public string bankName { get; set; }
        public string transactionId { get; set; }
        public string request { get; set; }
        public string response { get; set; }
        public string apiurl { get; set; }
        public string request_Hash { get; set; }
        public string accessCode { get;set; }
        public string customerId { get; set; }
        public string controlCardNo { get; set; }
        public Decimal amount { get; set; }
    }
}
