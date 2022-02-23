namespace HPCL.Common.Models.ResponseModel.CustomerFeeWaiver
{
    public class PendingCustResponse
    {
        public string FormNumber { get; set; }
        public string CustomerReferenceNo { get; set; }
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public string CreatedRoleName { get; set; }
    }

    public class GetApproveFeeWaiverBasicDetail
    {
        public string CustomerReferenceNo { get; set; }
        public string CardPreference { get; set; }
        public int FeePaymentsCollectFeeWaiverId { get; set; }
        public string FeePaymentsCollectorFeeWaiver { get; set; }
        public string FeePaymentNo { get; set; }
        public string FeePaymentDate { get; set; }
    }

    public class GetApproveFeeWaiverCardDetail
    {
        public string CardIdentifier { get; set; }
        public string VehicleType { get; set; }
        public string VehicleMake { get; set; }
        public int YearOfRegistration { get; set; }
        public string VechileOwnerName { get; set; }
    }

}
