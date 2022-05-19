using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.CustomerRequest
{
    public class UpdateSmsAlertForMultipleMobileDetailReq : BaseEntity
    {
        public CustomerDetailForSmsAlert[] CustomerDetailForSmsAlert { get; set; }
    }

    public class CustomerDetailForSmsAlert
    {
        public string CustomerID { get; set; }
        public string MobileNo { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
    }
}
