using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.CustomerFeeWaiver
{
    public class BindApprovePendingCustomer : BaseEntity
    {
        public string CustomerReferenceNo { get; set; }
        public string formNumber { get; set; }
    }
}
