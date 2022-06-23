using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class CustomerCreationRequestModel : BaseEntity
    {
        public string RequestId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerType { get; set; }
        public string CustomerSubtype { get; set; }
        public string ZonalOffice { get; set; }
        public string RegionalOffice { get; set; }
        public string DateOfApplication { get; set; }
        public string SalesArea { get; set; }
        public string IndividualOrgNameTitle { get; set; }
        public string IndividualOrgName { get; set; }
        public string NameOnCard { get; set; }
        public string TypeOfBusinessEntity { get; set; }
        public string ResidenceStatus { get; set; }
        public string IncomeTaxPan { get; set; }
        public string CommunicationAddress1 { get; set; }
        public string CommunicationAddress2 { get; set; }
        public string CommunicationAddress3 { get; set; }
        public string CommunicationLocation { get; set; }
        public string CommunicationCityName { get; set; }
        public string CommunicationPincode { get; set; }
        public string CommunicationStateId { get; set; }
        public string CommunicationDistrictId { get; set; }
        public string CommunicationPhoneNo { get; set; }
        public string CommunicationFax { get; set; }
        public string CommunicationMobileNo { get; set; }
        public string CommunicationEmailid { get; set; }
        public string PermanentAddress1 { get; set; }
        public string PermanentAddress2 { get; set; }
        public string PermanentAddress3 { get; set; }
        public string PermanentLocation { get; set; }
        public string PermanentCityName { get; set; }
        public string PermanentPincode { get; set; }
        public string PermanentStateId { get; set; }
        public string PermanentDistrictId { get; set; }
        public string PermanentPhoneNo { get; set; }
        public string PermanentFax { get; set; }
        public string KeyOfficialTitle { get; set; }
        public string KeyOfficialIndividualInitials { get; set; }
        public string KeyOfficialFirstName { get; set; }
        public string KeyOfficialMiddleName { get; set; }
        public string KeyOfficialLastName { get; set; }
        public string KeyOfficialFax { get; set; }
        public string KeyOfficialDesignation { get; set; }
        public string KeyOfficialEmail { get; set; }
        public string KeyOfficialPhoneNo { get; set; }
        public string KeyOfficialDOA { get; set; }
        public string KeyOfficialMobile { get; set; }
        public string KeyOfficialDOB { get; set; }
        public string KeyOfficialSecretQuestion { get; set; }
        public string KeyOfficialSecretAnswer { get; set; }
        public string KeyOfficialTypeOfFleet { get; set; }
        public string KeyOfficialCardAppliedFor { get; set; }
        public string KeyOfficialApproxMonthlySpendsonVechile1 { get; set; }
        public string KeyOfficialApproxMonthlySpendsonVechile2 { get; set; }
        public string AreaOfOperation { get; set; }
        public string FleetSizeNoOfVechileOwnedHCV { get; set; }
        public string FleetSizeNoOfVechileOwnedLCV { get; set; }
        public string FleetSizeNoOfVechileOwnedMUVSUV { get; set; }
        public string FleetSizeNoOfVechileOwnedCarJeep { get; set; }
        public string NoOfCards { get; set; }
        public string FeePaymentsCollectFeeWaiver { get; set; }
        public string Createdby { get; set; }
        public string TierOfCustomer { get; set; }
        public string TypeOfCustomer { get; set; }
        public string FormNumber { get; set; }
        public string PanCardRemarks { get; set; }
        public string RBEId { get; set; }

    }
}
