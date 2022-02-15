using System;
using System.ComponentModel.DataAnnotations;

namespace HPCL_Web.Models.CustomerFeeWaiver
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
    }

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
        //[DisplayFormat(DataFormatString = "{yyyy-MM-ddHH:mm:ss}")]
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

    public class ApproveRejectWaiver : BaseEntity
    {
        public string CustomerReferenceNo { get; set; }
        public string Comments { get; set; }
        public int Approvalstatus { get; set; }
        public string ApprovedBy { get; set; }
    }

    public class ApproveRejectResponse
    {
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
