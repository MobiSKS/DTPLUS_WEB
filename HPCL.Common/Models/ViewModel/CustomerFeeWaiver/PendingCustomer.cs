
using HPCL.Common.Models.CommonEntity;
using System.Numerics;

namespace HPCL.Common.Models.ViewModel.CustomerFeeWaiver
{
    public class PendingCustomer : BaseEntity
    {
        public string StateId { get; set; }
        public string FormNumber { get; set; }
        public string CustomerName { get; set; }
        public string Createdon { get; set; }
        public string Createdby { get; set; }
        public string CardPreference { get; set; }
        public int FeePaymentsCollectFeeWaiverId { get; set; }
        public string FeePaymentsCollectorFeeWaiver { get; set; }
        public string FeePaymentNo { get; set; }
        public string FeePaymentDate { get; set; }
        public string CardIdentifier { get; set; }
        public string VehicleType { get; set; }
        public string VehicleMake { get; set; }
        public int YearOfRegistration { get; set; }
        public string VechileOwnerName { get; set; }

        public string Comments { get; set; }
    }

    public class ApproveRejectWaiver : BaseEntity
    {
        public string CustomerReferenceNo { get; set; }
        public string Comments { get; set; }
        public int Approvalstatus { get; set; }
        public string ApprovedBy { get; set; }
    }

    public class BindPendingCustomer : BaseEntity
    {
        public BigInteger FormNumber { get; set; }
    }
}
