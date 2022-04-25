using HPCL.Common.Models.CommonEntity;
using System.Collections.Generic;

namespace HPCL.Common.Models.ViewModel.ApplicationFormDataEntry
{
    public class AddAddOnCard : BaseEntity
    {
        public string CustomerId { get; set; }
        public string FormNumber { get; set; }
        public string NoOfCards { get; set; }
        public string RBEId { get; set; }
        public string RBEName { get; set; }
        public int FeePaymentsCollectFeeWaiver { get; set; }
        public string FeePaymentNo { get; set; }
        public string FeePaymentDate { get; set; }
        public List<ObjCardDetail> ObjCardDetail { get; set; }
        public string CardPreference { get; set; }
        public string NoofVechileforAllCards { get; set; }
        public string CustomerTypeId { get; set; }
        public int Status { get; set; }
        public string CustomerName { get; set; }
        public string ReceivedAmount { get; set; }
        public string NoOfGridRows { get; set; }
        public bool VehicleVerifiedManually { get; set; }
        public string VehicleNoVerifiedManually { get; set; }
        public string Reason { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string TableStringyfiedData { get; set; }
        public AddAddOnCard()
        {
            ObjCardDetail = new List<ObjCardDetail>();
            VehicleVerifiedManually = false;
        }
    }

    public class ObjCardDetail
    {
        public string CustomerTypeId { get; set; }
        public string Message { get; set; }
        public string CardPreference { get; set; }
        public string FeePaymentNo { get; set; }
        public string FeePaymentDate { get; set; }
        public int FeePaymentsCollectFeeWaiver { get; set; }
        public string NoOfCards { get; set; }
        public string NoofVehicles { get; set; }
        public string CardIdentifier { get; set; }
        public string VechileNo { get; set; }
        public string VehicleType { get; set; }
        public string VehicleMake { get; set; }
        public string YearOfRegistration { get; set; }
        public string MobileNo { get; set; }
        public bool VehicleVerifiedManually { get; set; }
        public string DuplicateVehicleNo { get; set; }
        public string DuplicateMobileNo { get; set; }
        public ObjCardDetail()
        {
            VehicleVerifiedManually = false;
        }
    }
}
