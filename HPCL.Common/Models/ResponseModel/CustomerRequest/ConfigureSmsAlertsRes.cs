using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.CustomerRequest
{
    public class ConfigureSmsAlertsRes : ResponseMsg
    {
        public List<ConfigureSmsAlertsResData> data { get; set; }
    }

    public class ConfigureSmsAlertsResData
    {
        public string TransactionType { get;set; }
        public int TransactionID { get; set; }
        public int SMSStatus { get; set; }
        public string SMSCondition { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
