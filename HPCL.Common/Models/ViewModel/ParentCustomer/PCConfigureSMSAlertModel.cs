using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.ParentCustomer
{
    public class PCConfigureSMSAlertModel:CommonResponseBase
    {
        public PCConfigureSMSAlertModel()
        {
            Data = new List<AvailableAlerts>();
        }
        public string CustomerID { get; set; }
        public List<AvailableAlerts> Data { get; set; }
        public string DND { get; set; }
    }
    public class AvailableAlerts
    {
        public string TransactionID { get; set; }
        public string SMSStatus { get; set; }
        public string TransactionType { get; set; }
        public string SMSCondition { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }

    }
}
