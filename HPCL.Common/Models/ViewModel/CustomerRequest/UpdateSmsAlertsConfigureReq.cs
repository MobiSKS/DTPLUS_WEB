using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.CustomerRequest
{
    public class UpdateSmsAlertsConfigureReq : BaseEntity
    {
        public string CustomerId { get; set; }
        public TypeConfigureSMSAlerts[] TypeConfigureSMSAlerts { get; set; }
    }

    public class TypeConfigureSMSAlerts
    {
        public string CustomerID { get; set; }
        public int TransactionID { get; set; }
        public int statusId { get; set; }
    }
}