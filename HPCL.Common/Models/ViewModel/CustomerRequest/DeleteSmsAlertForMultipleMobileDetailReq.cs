using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.CustomerRequest
{
    public class DeleteSmsAlertForMultipleMobileDetailReq : BaseEntity
    {
        public string CustomerID { get; set; }
        public string MobileNo { get; set; }
    }
}
