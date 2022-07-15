using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ResponseModel.Aggregator;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.Customer;
using HPCL.Common.Models.ViewModel.Officers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.VolvoEicher
{

    public class VEManageProfile : BaseEntity
    {
        public VEManageProfile()
        {
            CustomerTypeMdl = new List<CustomerTypeModel>();
            CustomerTypeMdl.Add(new CustomerTypeModel
            {
                CustomerTypeID = 0,
                CustomerTypeName = "Select Type"

            });
            CustomerSubTypeMdl = new List<CustomerSubTypeModel>();
            CustomerSubTypeMdl.Add(new CustomerSubTypeModel
            {
                CustomerSubTypeID = 0,
                CustomerSubTypeName = "Select Sub Type"

            });
            CustomerZonalOfficeMdl = new List<CustomerZonalOfficeModel>();
            CustomerZonalOfficeMdl.Add(new CustomerZonalOfficeModel
            {
                ZonalOfficeID = 0,
                ZonalOfficeName = "Select Zonal Office"
            });
            CustomerRegionMdl = new List<CustomerRegionModel>();
            CustomerRegionMdl.Add(new CustomerRegionModel
            {
                RegionalOfficeID = 0,
                RegionalOfficeName = "Select Region"
            });
            CustomerStateMdl = new List<StateResponseModal>();
            CustomerStateMdl.Add(new StateResponseModal
            {
                CountryID = 0,
                StateID = 0,
                StateName = "Select State"
            });
            CustomerDistrictMdl = new List<CustomerDistrictModel>();
            CustomerTbentityMdl = new List<CustomerTbentityModel>();
            CustomerTbentityMdl.Add(new CustomerTbentityModel
            {
                TBEntityID = 0,
                TBEntityName = "Select Type of Business Entity"
            });
            CustomerTypeOfFleetMdl = new List<CustomerTypeOfFleetModel>();
            CustomerTypeOfFleetMdl.Add(new CustomerTypeOfFleetModel
            {
                TypeOfFleetId = 0,
                TypeOfFleetName = "Select Type of Fleet"
            });
            CustomerSecretQueMdl = new List<CustomerSecretQueModel>();
            CustomerSecretQueMdl.Add(new CustomerSecretQueModel
            {
                SecretQuestionId = 0,
                SecretQuestionName = "Select Secret Question"
            });

            CardDetailsMdl = new List<CardDetails>();
            VehicleTypeMdl = new List<VehicleTypeModel>();
            SalesAreaMdl = new List<SalesAreaModel>();

            SalesAreaMdl.Add(new SalesAreaModel
            {
                SalesAreaID = 0,
                SalesAreaName = "Select Sales Area"
            });
            PerOrRegAddressDistrictMdl = new List<OfficerDistrictModel>();
            PerOrRegAddressDistrictMdl.Add(new OfficerDistrictModel
            {
                districtID = 0,
                districtName = "Select District"
            });
            CommunicationDistrictMdl = new List<OfficerDistrictModel>();
            CommunicationDistrictMdl.Add(new OfficerDistrictModel
            {
                districtID = 0,
                districtName = "Select District"
            });

            SBUTypes = new List<SbuTypeResponseModal>();
        }
        public string DealerCode { get; set; }
        public string EmployeeID { get; set; }
        public string NoofCards { get; set; }
        public string CustomerDateOfApplication { get; set; }
        public virtual List<OfficerDistrictModel> CommunicationDistrictMdl { get; set; }
        public int CustomerRegionID { get; set; }
        public string CustomerReferenceNo { get; set; }
        public string IsDuplicatePanNo { get; set; }
        public string AllowPanDuplication { get; set; }
        public string ExternalPANAPIStatus { get; set; }
        public string PanCardRemarks { get; set; }
        public string Remarks { get; set; }
        public virtual List<OfficerDistrictModel> PerOrRegAddressDistrictMdl { get; set; }
        public string CustomertypeId { get; set; }
        public string CustomerSubTypeID { get; set; }

        public string CustomerZonalOfficeID { get; set; }
        public string SalesArea { get; set; }

        public string CustomerIncomeTaxPan { get; set; }

        public string CustomerTbentityID { get; set; }


        public string CustomerNameOnCard { get; set; }

        public string CustomerResidenceStatus { get; set; }
        public string IndividualOrgNameTitle { get; set; }

        public string IndividualOrgName { get; set; }
        public string CommunicationAddress1 { get; set; }
        public string CommunicationAddress2 { get; set; }
        public string CommunicationAddress3 { get; set; }
        public string CommunicationLocation { get; set; }

        public string CommunicationCity { get; set; }
        public string CommunicationPinCode { get; set; }
        public string CommunicationStateID { get; set; }

        public string CommunicationDistrictId { get; set; }
        public bool IsSameAsCommAddress { get; set; }

        public bool InterState { get; set; }
        public bool InterCity { get; set; }
        public bool IntraCity { get; set; }

        public string CommunicationDialCode { get; set; }
        public string CommunicationPhoneNo { get; set; }
        public string CommunicationFaxCode { get; set; }
        public string CommunicationFax { get; set; }

        public string CommunicationMobileNumber { get; set; }

        public string CommunicationEmail { get; set; }


        public string PerOrRegAddress1 { get; set; }
        public string PerOrRegAddress2 { get; set; }
        public string PerOrRegAddress3 { get; set; }

        public string PerOrRegAddressLocation { get; set; }

        public string PerOrRegAddressCity { get; set; }
        public string PerOrRegAddressPinCode { get; set; }
        public string PerOrRegAddressStateID { get; set; }
        public string PerOrRegAddressState { get; set; }
        public string PerOrRegAddressDistrict { get; set; }

        public string PermanentDistrictId { get; set; }

        public string PerOrRegAddressDialCode { get; set; }
        public string PerOrRegAddressPhoneNumber { get; set; }
        public string PermanentFaxCode { get; set; }
        public string PermanentFax { get; set; }





        // Key Office Details

        public string KeyOffTitle { get; set; }

        public string KeyOffFirstName { get; set; }

        public string KeyOffIndividualInitials { get; set; }
        public string KeyOffLastName { get; set; }
        public string KeyOffMiddleName { get; set; }
        public string KeyOffDesignation { get; set; }
        public string KeyOffFaxCode { get; set; }
        public string KeyOffFax { get; set; }

        public string KeyOffDialCode { get; set; }

        public string KeyOffPhoneCode { get; set; }
        public string KeyOffPhoneNumber { get; set; }

        public string KeyOffMobileNumber { get; set; }

        public string KeyOffEmail { get; set; }
        public string KeyOffDateOfAnniversary { get; set; }
        public string KeyOfficialDOB { get; set; }

        public string CustomerTypeOfFleetID { get; set; }
        public string KeyOfficialSecretQuestion { get; set; }
        public string KeyOfficialSecretQuestionName { get; set; }
        public string KeyOfficialSecretAnswer { get; set; }
        public string AreaOfOperation { get; set; }
        public string FleetSizeNoOfVechileOwnedHCV { get; set; }
        public string FleetSizeNoOfVechileOwnedLCV { get; set; }
        public string FleetSizeNoOfVechileOwnedMUVSUV { get; set; }
        public string FleetSizeNoOfVechileOwnedCarJeep { get; set; }
        public string KeyOfficialCardAppliedFor { get; set; }

        public decimal KeyOfficialApproxMonthlySpendsonVechile1 { get; set; }
        public decimal KeyOfficialApproxMonthlySpendsonVechile2 { get; set; }
        public string NoOfCards { get; set; }


        public string TypeOfCustomerID { get; set; }

        public string TierOfCustomerID { get; set; }

        public string CustomerSalesAreaID { get; set; }

        public string FormNumber { get; set; }

        public string OfficerTypeID { get; set; }
        public virtual List<CardDetails> CardDetailsMdl { get; set; }
        public virtual List<CustomerTypeModel> CustomerTypeMdl { get; set; }
        public virtual List<CustomerSubTypeModel> CustomerSubTypeMdl { get; set; }
        public virtual List<CustomerZonalOfficeModel> CustomerZonalOfficeMdl { get; set; }
        public virtual List<CustomerRegionModel> CustomerRegionMdl { get; set; }
        public virtual List<StateResponseModal> CustomerStateMdl { get; set; }
        public virtual List<CustomerDistrictModel> CustomerDistrictMdl { get; set; }
        public virtual List<CustomerTbentityModel> CustomerTbentityMdl { get; set; }
        public virtual List<CustomerTypeOfFleetModel> CustomerTypeOfFleetMdl { get; set; }
        public virtual List<CustomerSecretQueModel> CustomerSecretQueMdl { get; set; }
        public virtual List<VehicleTypeModel> VehicleTypeMdl { get; set; }
        public virtual List<SalesAreaModel> SalesAreaMdl { get; set; }

        public virtual List<VECustomerProfileResponse> CustomerProfileResponses { get; set; }

        public string CustomerSignedOn { get; set; }

        public string CustomerFormNumber { get; set; }
        public string CustomerApplicationDate { get; set; }
        public string CustomerRegionalOfficeID { get; set; }
        [StringLength(10)]
        public string CustomerId { get; set; }
        public string NameOnCard { get; set; }

        public string ExistingCustomerId { get; set; }

        public string CustomerName { get; set; }

        public string IncomeTaxPan { get; set; }

        public string BeneficiaryName { get; set; }

        public string RelationwithBeneficiary { get; set; }

        public string BeneficiaryMobile { get; set; }
        public virtual List<SbuTypeResponseModal> SBUTypes { get; set; }
        public int SBUTypeID { get; set; }
        public string CustomerTypeId { get; set; }
        public string CustomerSubtypeId { get; set; }
        public string CustomerIdSelected { get; set; }
    }


    public class VECustomerProfileResponse
    {
        public string CustomerZonalOfficeID { get; set; }
        public string CustomerRegionID { get; set; }
        public string SalesArea { get; set; }
        public string CustomerIncomeTaxPan { get; set; }
        public string CustomerTbentityID { get; set; }
        public string CustomerDateOfApplication { get; set; }
        public string CustomerNameOnCard { get; set; }
        public string CustomerResidenceStatus { get; set; }
        public string IndividualOrgNameTitle { get; set; }
        public string IndividualOrgName { get; set; }
        public string CommunicationAddress1 { get; set; }
        public string CommunicationAddress2 { get; set; }
        public string CommunicationAddress3 { get; set; }
        public string CommunicationLocation { get; set; }
        public string CommunicationCity { get; set; }
        public string CommunicationState { get; set; }
        public string CommunicationDistrictId { get; set; }
        public bool IsSameAsCommAddress { get; set; }
        public bool stringerState { get; set; }
        public bool stringerCity { get; set; }
        public bool stringraCity { get; set; }
        public string CommunicationDialCode { get; set; }
        public string CommunicationPhoneNo { get; set; }
        public string CommunicationFaxCode { get; set; }
        public string CommunicationFax { get; set; }
        public string CommunicationMobileNumber { get; set; }
        public string CommunicationEmail { get; set; }
        public string PerOrRegAddress1 { get; set; }
        public string PerOrRegAddress2 { get; set; }
        public string PerOrRegAddress3 { get; set; }
        public string PerOrRegAddressLocation { get; set; }
        public string PerOrRegAddressCity { get; set; }
        public string PerOrRegAddressPinCode { get; set; }
        public string PerOrRegAddressStateID { get; set; }
        public string PerOrRegAddressState { get; set; }
        public string PerOrRegAddressDistrict { get; set; }
        public string PermanentDistrictId { get; set; }
        public string PerOrRegAddressDialCode { get; set; }
        public string PerOrRegAddressPhoneNumber { get; set; }
        public string PermanentFaxCode { get; set; }
        public string PermanentFax { get; set; }


        // Key Office Details

        public string KeyOffTitle { get; set; }
        public string KeyOffFirstName { get; set; }
        public string KeyOffIndividualInitials { get; set; }
        public string KeyOffLastName { get; set; }
        public string KeyOffMiddleName { get; set; }
        public string KeyOffDesignation { get; set; }
        public string KeyOffFaxCode { get; set; }
        public string KeyOffFax { get; set; }
        public string KeyOffDialCode { get; set; }
        public string KeyOffPhoneCode { get; set; }
        public string KeyOffPhoneNumber { get; set; }
        public string KeyOffMobileNumber { get; set; }
        public string KeyOffEmail { get; set; }
        public string KeyOffDateOfAnniversary { get; set; }
        public string KeyOfficialDOB { get; set; }
        public string CustomerTypeOfFleetID { get; set; }
        public string KeyOfficialSecretQuestion { get; set; }
        public string KeyOfficialSecretQuestionName { get; set; }
        public string KeyOfficialSecretAnswer { get; set; }
        public string AreaOfOperation { get; set; }
        public string FleetSizeNoOfVechileOwnedHCV { get; set; }
        public string FleetSizeNoOfVechileOwnedLCV { get; set; }
        public string FleetSizeNoOfVechileOwnedMUVSUV { get; set; }
        public string FleetSizeNoOfVechileOwnedCarJeep { get; set; }
        public string KeyOfficialCardAppliedFor { get; set; }
        public decimal KeyOfficialApproxMonthlySpendsonVechile1 { get; set; }
        public decimal KeyOfficialApproxMonthlySpendsonVechile2 { get; set; }
        public string NoOfCards { get; set; }
        public string FeePaymentsCollectFeeWaiver { get; set; }
        public string FeePaymentsChequeNo { get; set; }
        public string FeePaymentsChequeDate { get; set; }
        public string TypeOfCustomerID { get; set; }
        public string TierOfCustomerID { get; set; }
        public string CustomerSalesAreaID { get; set; }
        public string FormNumber { get; set; }
        public string OfficerTypeID { get; set; }
        public string FormNumberForSearch { get; set; }
        public string CustomerReferenceNoForSearch { get; set; }
        public string Remarks { get; set; }
        public string CustomerID { get; set; }
        public string CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; }
        public string CustomerSubtypeId { get; set; }
        public string CustomerSubtypeName { get; set; }
        public string ZonalOfficeID { get; set; }
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeID { get; set; }
        public string RegionalOfficeName { get; set; }
        public string DateOfApplication { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedTime { get; set; }
        public object ModifiedBy { get; set; }
        public string ModifiedTime { get; set; }
        public string NameOnCard { get; set; }
        public string TypeOfBusinessEntityId { get; set; }
        public string TypeOfBusinessEntityName { get; set; }
        public string ResidenceStatus { get; set; }
        public string IncomeTaxPan { get; set; }
        public string CommunicationCityName { get; set; }
        public string CommunicationPincode { get; set; }
        public string CommunicationStateId { get; set; }
        public string CommunicationStateName { get; set; }
        public string CommunicationDistrictName { get; set; }
        public string CommunicationMobileNo { get; set; }
        public string CommunicationEmailid { get; set; }
        public string PermanentAddress1 { get; set; }
        public string PermanentAddress2 { get; set; }
        public string PermanentAddress3 { get; set; }
        public string PermanentLocation { get; set; }
        public string PermanentCityName { get; set; }
        public string PermanentPincode { get; set; }
        public string PermanentStateId { get; set; }
        public string PermanentStateName { get; set; }
        public string PermanentDistrictName { get; set; }
        public string PermanentPhoneNo { get; set; }
        public string KeyOfficialTitle { get; set; }
        public string KeyOfficialIndividualInitials { get; set; }
        public string KeyOfficialNameofIndividual { get; set; }
        public string KeyOfficialFirstName { get; set; }
        public string KeyOfficialMiddleName { get; set; }
        public string KeyOfficialLastName { get; set; }
        public string KeyOfficialFax { get; set; }
        public string KeyOfficialDesignation { get; set; }
        public string KeyOfficialEmail { get; set; }
        public string KeyOfficialPhoneNo { get; set; }
        public string KeyOfficialDOA { get; set; }
        public string KeyOfficialMobile { get; set; }
        public string KeyOfficialSecretQuestionId { get; set; }
        public string KeyOfficialTypeOfFleetId { get; set; }
        public string KeyOfficialTypeOfFleetName { get; set; }
        public string FeePaymentsCollectFeeWaiverId { get; set; }
        public string ReferenceId { get; set; }
        public string ControlCardNo { get; set; }
        public string CustomerReferenceNo { get; set; }
        public string CustomerStatusId { get; set; }
        public string CustomerStatusName { get; set; }
        public string Approvedon { get; set; }
        public object ApprovedBy { get; set; }
        public string CustomerStatusFeewaiverID { get; set; }
        public object CustomerStatusFeewaiverName { get; set; }
        public object FeewaiverComments { get; set; }
        public string FeewaiverApprovedOn { get; set; }
        public object FeewaiverApprovedBy { get; set; }
        public string CustomerSignedOn { get; set; }
        public string CustomerFormNumber { get; set; }
        public string CustomerApplicationDate { get; set; }
        public string CustomerRegionalOfficeID { get; set; }
        public string ExistingCustomerId { get; set; }
        public string BeneficiaryName { get; set; }
        public string RelationwithBeneficiary { get; set; }
        public string BeneficiaryMobile { get; set; }
        public string DrivingLicence { get; set; }
        public string CustomerName { get; set; }
        public string SignedOn { get; set; }
        public int SBUId { get; set; }
        public string strSBU { get; set; }
    }
    public class VESearchGridResponse
    {
        public string CardNumber { get; set; }
        public string VechileNo { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Status { get; set; }
    }
}
