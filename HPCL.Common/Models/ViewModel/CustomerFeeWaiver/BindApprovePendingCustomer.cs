using HPCL.Common.Models.CommonEntity;
using System.Numerics;

namespace HPCL.Common.Models.ViewModel.CustomerFeeWaiver
{
    public class BindApprovePendingCustomer : BaseEntity
    {
        public BigInteger CustomerReferenceNo { get; set; }
        public string formNumber { get; set; }
    }
}
