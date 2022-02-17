using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPCL_Web.Models.Officer;

namespace HPCL_Web.Models
{
    public class CustomerModel : BaseEntity
    {
        public CustomerModel()
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
            CustomerStateMdl = new List<CustomerStateModel>();
            CustomerStateMdl.Add(new CustomerStateModel
            {
                CountryID = 0,
                StateID = 0,
                StateName = "Select State"
            });
            //CustomerDistrictMdl = new List<CustomerDistrictModel>();
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

            OfficerTypeMdl = new List<OfficerTypeModel>();
            OfficerTypeMdl.Add(new OfficerTypeModel
            {
                OfficerTypeID = 0,
                OfficerTypeName = "Select Created By"
            });

            CommunicationDistrictMdl = new List<OfficerDistrictModel>();
            CommunicationDistrictMdl.Add(new OfficerDistrictModel
            {
                districtID = 0,
                districtName = "Select Communication District"
            });

            PerOrRegAddressDistrictMdl = new List<OfficerDistrictModel>();
            PerOrRegAddressDistrictMdl.Add(new OfficerDistrictModel
            {
                districtID = 0,
                districtName = "Select Permanent District"
            });

        }

        public virtual List<CardDetails> CardDetailsMdl { get; set; }
        public virtual List<CustomerTypeModel> CustomerTypeMdl { get; set; }
        public virtual List<CustomerSubTypeModel> CustomerSubTypeMdl { get; set; }
        public virtual List<CustomerZonalOfficeModel> CustomerZonalOfficeMdl { get; set; }
        public virtual List<CustomerRegionModel> CustomerRegionMdl { get; set; }
        public virtual List<CustomerStateModel> CustomerStateMdl { get; set; }
        //public virtual List<CustomerDistrictModel> CustomerDistrictMdl { get; set; }
        public virtual List<CustomerTbentityModel> CustomerTbentityMdl { get; set; }
        public virtual List<CustomerTypeOfFleetModel> CustomerTypeOfFleetMdl { get; set; }
        public virtual List<CustomerSecretQueModel> CustomerSecretQueMdl { get; set; }
        public virtual List<VehicleTypeModel> VehicleTypeMdl { get; set; }
        public virtual List<SalesAreaModel> SalesAreaMdl { get; set; }

        public virtual List<OfficerDistrictModel> CommunicationDistrictMdl { get; set; }
        public virtual List<OfficerDistrictModel> PerOrRegAddressDistrictMdl { get; set; }

        [Required(ErrorMessage = "Customer Type is Required")]
        public int CustomerTypeID { get; set; }
        [Required(ErrorMessage = "Zonal Office is Required")]
        public int CustomerZonalOfficeID { get; set; }
        [Required(ErrorMessage = "Region  is Required")]
        public int CustomerRegionID { get; set; }
        [Required(ErrorMessage = "Sales Area  is Required")]
        public string SalesArea { get; set; }
        [Required(ErrorMessage = "Customer Sub Type is Required")]
        public int CustomerSubTypeID { get; set; }

        [Required(ErrorMessage = "Income Tax Pan is Required")]
        public string CustomerIncomeTaxPan { get; set; }

        [Required(ErrorMessage = "Type of Business Entity is Required")]
        public int CustomerTbentityID { get; set; }

        //[Required(ErrorMessage = "Date of application is Required")]
        public string CustomerDateOfApplication { get; set; }

        [Required(ErrorMessage = "Customer Name on Card is Required")]
        public string CustomerNameOnCard { get; set; }

        [Required(ErrorMessage = "Customer Residence Status is Required")]
        public string CustomerResidenceStatus { get; set; }
        [Required(ErrorMessage = "Individual Organisation Title is Required")]
        public string IndividualOrgNameTitle { get; set; }
        [Required(ErrorMessage = "Individual Organisation Name is Required")]

        public string IndividualOrgName { get; set; }
        [Required(ErrorMessage = "Communication Address 1 is Required")]
        public string CommunicationAddress1 { get; set; }
        public string CommunicationAddress2 { get; set; }
        public string CommunicationAddress3 { get; set; }
        public string CommunicationLocation { get; set; }

        [Required(ErrorMessage = "City is Required under communication Address")]
        public string CommunicationCity { get; set; }
        [Required(ErrorMessage = "Pin code is Required under communication Address")]
        public string CommunicationPinCode { get; set; }
        [Required(ErrorMessage = "State is Required under communication Address")]
        public int CommunicationStateID { get; set; }

        [Required(ErrorMessage = "State is Required under communication Address")]
        public string CommunicationState { get; set; }
        [Required(ErrorMessage = "District is Required under communication Address")]
        public string CommunicationDistrictId { get; set; }
        public bool IsSameAsCommAddress { get; set; }

        public bool InterState { get; set; }
        public bool InterCity { get; set; }
        public bool IntraCity { get; set; }

        [Required(ErrorMessage = "Dial code is Required under communication Address")]
        public string CommunicationDialCode { get; set; }
        [Required(ErrorMessage = "Phone Number is Required under communication Address")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value only")]
        public string CommunicationPhoneNo { get; set; }
        public string CommunicationFaxCode { get; set; }
        public string CommunicationFax { get; set; }

        [Required(ErrorMessage = "Mobile Phone number is Required under communication Address")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value only")]
        public string CommunicationMobileNumber { get; set; }


        [Required(ErrorMessage = "E-mail is Required under communication Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string CommunicationEmail { get; set; }


        [Required(ErrorMessage = "Address 1 is Required in Permanent/Registered  Address")]
        public string PerOrRegAddress1 { get; set; }
        [Required(ErrorMessage = "Address 2 is Required in Permanent/Registered  Address")]
        public string PerOrRegAddress2 { get; set; }
        [Required(ErrorMessage = "Address 3 is Required in Permanent/Registered  Address")]
        public string PerOrRegAddress3 { get; set; }

        public string PerOrRegAddressLocation { get; set; }

        [Required(ErrorMessage = "City is Required under Permanent/Registered Address")]
        public string PerOrRegAddressCity { get; set; }
        [Required(ErrorMessage = "Pin code is Required under Permanent/Registered Address")]
        public string PerOrRegAddressPinCode { get; set; }
        [Required(ErrorMessage = "State is Required under communication Address")]
        public int PerOrRegAddressStateID { get; set; }
        [Required(ErrorMessage = "State is Required under Permanent/Registered Address")]

        public string PerOrRegAddressState { get; set; }
        [Required(ErrorMessage = "District is Required under Permanent/Registered Address")]
        public string PerOrRegAddressDistrict { get; set; }

        public int PermanentDistrictId { get; set; }

        [Required(ErrorMessage = "Dial code is Required under Permanent/Registered Address")]
        public string PerOrRegAddressDialCode { get; set; }
        //[Required(ErrorMessage = "Phone Number is Required under Permanent/Registered Address")]
        //[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value only")]
        public string PerOrRegAddressPhoneNumber { get; set; }
        public string PermanentFaxCode { get; set; }
        public string PermanentFax { get; set; }





        // Key Office Details

        [Required(ErrorMessage = "Title is Required")]
        public string KeyOffTitle { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string KeyOffFirstName { get; set; }

        public string KeyOffIndividualInitials { get; set; }
        public string KeyOffLastName { get; set; }
        public string KeyOffMiddleName { get; set; }
        [Required(ErrorMessage = "Designation is Required")]
        public string KeyOffDesignation { get; set; }
        public string KeyOffFaxCode { get; set; }
        public string KeyOffFax { get; set; }

        public string KeyOffDialCode { get; set; }

        public string KeyOffPhoneCode { get; set; }

        //[Required(ErrorMessage = "Phone Number is Required under communication Address")]
        //[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value only")]
        public string KeyOffPhoneNumber { get; set; }

        [Required(ErrorMessage = "Mobile Phone number is Required under communication Address")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value only")]
        public string KeyOffMobileNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string KeyOffEmail { get; set; }
        public string KeyOffDateOfAnniversary { get; set; }
        public string KeyOfficialDOB { get; set; }

        //Other details in Key Official details Tab
        public string CustomerTypeOfFleetID { get; set; }
        public string KeyOfficialSecretQuestion { get; set; }
        public string KeyOfficialSecretQuestionName { get; set; }
        public string KeyOfficialSecretAnswer { get; set; }
        public string AreaOfOperation { get; set; }
        public int FleetSizeNoOfVechileOwnedHCV { get; set; }
        public int FleetSizeNoOfVechileOwnedLCV { get; set; }
        public int FleetSizeNoOfVechileOwnedMUVSUV { get; set; }
        public int FleetSizeNoOfVechileOwnedCarJeep { get; set; }
        public string KeyOfficialCardAppliedFor { get; set; }

        public decimal KeyOfficialApproxMonthlySpendsonVechile1 { get; set; }
        public decimal KeyOfficialApproxMonthlySpendsonVechile2 { get; set; }
        public int NoOfCards { get; set; }

        public int FeePaymentsCollectFeeWaiver { get; set; }
        public int FeePaymentsChequeNo { get; set; }
        public string FeePaymentsChequeDate { get; set; }

        public int TypeOfCustomerID { get; set; }

        public int TierOfCustomerID { get; set; }

        [Required(ErrorMessage = "Sales Area is Required")]
        public int CustomerSalesAreaID { get; set; }

        public string FormNumber { get; set; }

        public int OfficerTypeID { get; set; }
        public virtual List<OfficerTypeModel> OfficerTypeMdl { get; set; }

        public string FormNumberForSearch { get; set; }
        public string CustomerReferenceNoForSearch { get; set; }
        public string Remarks { get; set; }

        public string IdProofType { get; set; }
        public string IdProofDocumentNo { get; set; }
        public string IdProofFront { get; set; }
        public string IdProofBack { get; set; }
        public string AddressProofType { get; set; }
        public string AddressProofDocumentNo { get; set; }
        public string AddressProofFront { get; set; }
        public string AddressProofBack { get; set; }

    }


    public class CustomerTypeModel
    {
        public int CustomerTypeID { get; set; }
        public string CustomerTypeName { get; set; }

    }
    public class CustomerSubTypeModel
    {
        public int CustomerSubTypeID { get; set; }
        public string CustomerSubTypeName { get; set; }

    }
    public class CustomerZonalOfficeModel
    {
        //  public int HQID { get; set; }
        public int ZonalOfficeID { get; set; }
        public string ZonalOfficeName { get; set; }
    }
    public class CustomerRegionModel
    {
        public int RegionalOfficeID { get; set; }
        public string RegionalOfficeName { get; set; }

    }
    public class CustomerStateModel
    {
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }
    }
    public class CustomerDistrictModel
    {
        public int stateID { get; set; }
        public int districtID { get; set; }
        public string districtName { get; set; }
    }

    public class CustomerTbentityModel
    {
        public int TBEntityID { get; set; }
        public string TBEntityName { get; set; }

    }
    public class CustomerTypeOfFleetModel
    {
        public int TypeOfFleetId { get; set; }
        public string TypeOfFleetName { get; set; }

    }
    public class CustomerSecretQueModel
    {
        public int SecretQuestionId { get; set; }
        public string SecretQuestionName { get; set; }
        public string SecretAnswer { get; set; }

    }

    public class CardDetails
    {
        public string CardIdentifier { get; set; }
        public string VechileNo { get; set; }
        public int VehicleType { get; set; }
        public string VehicleMake { get; set; }
        public int YearOfRegistration { get; set; }
    }
    public class VehicleTypeModel
    {

        public int VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; }

    }

    public class CustomerResponse
    {
        public int Status_Code { get; set; }
        public int Internel_Status_Code { get; set; }
        public string Message { get; set; }

        public Boolean Success { get; set; }

        public List<CustomerResponseData> Data { get; set; }
    }

    public class CustomerResponseData
    {
        public string ReferenceId { get; set; }
        public string FormNumber { get; set; }
        public string CustomerReferenceNo { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }

    }

    public class CustomerCardInfo
    {
        public CustomerCardInfo()
        {
            VehicleTypeMdl = new List<VehicleTypeModel>();
            ObjCardDetail = new List<CardDetails>();
        }

        public String CustomerReferenceNo { get; set; }
        public String CustomerName { get; set; }
        public String FormNumber { get; set; }
        public string NoOfCards { get; set; }
        public string RBEId { get; set; }
        public int FeePaymentsCollectFeeWaiver { get; set; }
        public string FeePaymentNo { get; set; }
        public string FeePaymentDate { get; set; }
        public string CardPreference { get; set; }

        public string RBEName { get; set; }
        public int Status { get; set; }
        public int StatusCode { get; set; }
        public virtual List<VehicleTypeModel> VehicleTypeMdl { get; set; }
        public List<CardDetails> ObjCardDetail { get; set; }
    }

    public class CustomerResponseByReferenceNo
    {
        public int Status_Code { get; set; }
        public int Internel_Status_Code { get; set; }
        public string Message { get; set; }

        public Boolean Success { get; set; }

        public List<CustomerResponseDataByReferenceNo> Data { get; set; }
    }

    public class CustomerResponseDataByReferenceNo
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FormNumber { get; set; }

    }

    public class CustomerRBE
    {
        public int Status_Code { get; set; }
        public int Internel_Status_Code { get; set; }
        public string Message { get; set; }

        public Boolean Success { get; set; }

        public List<CustomerRBEData> Data { get; set; }
    }

    public class CustomerRBEData
    {
        public string RBEId { get; set; }
        public string RBEName { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }

    public class CustomerInserCardResponse
    {
        public int Status_Code { get; set; }
        public int Internel_Status_Code { get; set; }
        public string Message { get; set; }

        public Boolean Success { get; set; }

        public List<CustomerInserCardResponseData> Data { get; set; }
    }

    public class CustomerInserCardResponseData
    {
        public int Status { get; set; }
        public string Reason { get; set; }

    }

    public class CustomerCardInsertInfo
    {
        public String CustomerReferenceNo { get; set; }
        public String CustomerName { get; set; }
        public String FormNumber { get; set; }
        public string NoOfCards { get; set; }
        public string RBEId { get; set; }
        public int FeePaymentsCollectFeeWaiver { get; set; }
        public string FeePaymentNo { get; set; }
        public string FeePaymentDate { get; set; }
        public string CardPreference { get; set; }

        public string RBEName { get; set; }

        public string Useragent { get; set; }
        public string Userip { get; set; }
        public string UserId { get; set; }

        public string Createdby { get; set; }

        public List<CardDetails> ObjCardDetail { get; set; }
    }
    public class SalesAreaModel
    {
        public int SalesAreaID { get; set; }
        public string SalesAreaName { get; set; }
    }
    //public class OfficerTypeModel
    //{
    //    public int OfficerTypeID { get; set; }
    //    public string OfficerTypeName { get; set; }
    //    public string OfficerTypeShortName { get; set; }

    //}
    //public class OfficerStateModel
    //{
    //    public int CountryID { get; set; }
    //    public int StateID { get; set; }
    //    public string StateName { get; set; }
    //}
    //public class OfficerDistrictModel
    //{
    //    public int stateID { get; set; }
    //    public int districtID { get; set; }
    //    public string districtName { get; set; }
    //}

    public class SearchCustomerResponseGrid
    {
        public string FormNumber { get; set; }
        public string CustomerReferenceNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public int TotalCards { get; set; }
        public int CreatedRoleId { get; set; }
        public string CreatedRoleName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string KYCStatus { get; set; }

    }

    public class CustomerFullDetails
    {
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
        public string SalesareaID { get; set; }
        public string SalesArea { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedTime { get; set; }
        public string IndividualOrgNameTitle { get; set; }
        public string IndividualOrgName { get; set; }
        public string NameOnCard { get; set; }
        public string TypeOfBusinessEntityId { get; set; }
        public string TypeOfBusinessEntityName { get; set; }
        public string ResidenceStatus { get; set; }
        public string IncomeTaxPan { get; set; }
        public string CommunicationAddress1 { get; set; }
        public string CommunicationAddress2 { get; set; }
        public string CommunicationAddress3 { get; set; }
        public string CommunicationLocation { get; set; }
        public string CommunicationCityName { get; set; }
        public string CommunicationPincode { get; set; }
        public string CommunicationStateId { get; set; }
        public string CommunicationStateName { get; set; }
        public string CommunicationDistrictId { get; set; }
        public string CommunicationDistrictName { get; set; }

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
        public string PermanentStateName { get; set; }
        public string PermanentDistrictId { get; set; }
        public string PermanentDistrictName { get; set; }
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
        public string KeyOfficialSecretQuestionId { get; set; }
        public string KeyOfficialSecretQuestionName { get; set; }
        public string KeyOfficialSecretAnswer { get; set; }
        public string KeyOfficialTypeOfFleetId { get; set; }
        public string KeyOfficialTypeOfFleetName { get; set; }
        public string KeyOfficialCardAppliedFor { get; set; }
        public string KeyOfficialApproxMonthlySpendsonVechile1 { get; set; }
        public string KeyOfficialApproxMonthlySpendsonVechile2 { get; set; }
        public string AreaOfOperation { get; set; }
        public string FleetSizeNoOfVechileOwnedHCV { get; set; }
        public string FleetSizeNoOfVechileOwnedLCV { get; set; }
        public string FleetSizeNoOfVechileOwnedMUVSUV { get; set; }
        public string FleetSizeNoOfVechileOwnedCarJeep { get; set; }
        public string NoOfCards { get; set; }
        public string FeePaymentsCollectFeeWaiverId { get; set; }
        public string FeePaymentsCollectFeeWaiver { get; set; }
        public string FeePaymentNo { get; set; }
        public string FeePaymentDate { get; set; }
        public string ReferenceId { get; set; }
        public string ControlCardNo { get; set; }
        public string FormNumber { get; set; }
        public string CustomerReferenceNo { get; set; }
        public string CustomerStatusId { get; set; }
        public string CustomerStatusName { get; set; }
        public string Remarks { get; set; }
        public string Approvedon { get; set; }
        public string ApprovedBy { get; set; }
        public string CustomerStatusFeewaiverID { get; set; }
        public string CustomerStatusFeewaiverName { get; set; }
        public string FeewaiverComments { get; set; }
        public string FeewaiverApprovedOn { get; set; }
        public string FeewaiverApprovedBy { get; set; }

        public string TierOfCustomerId { get; set; }
        public string TypeOfCustomerName { get; set; }
        public string TypeOfCustomerId { get; set; }
        public string TierOfCustomerName { get; set; }

    }

    public class UploadDocResponseBody
    {
        public string FormNumber { get; set; }
        public string CustomerReferenceNo { get; set; }
        public string IdProofTypeId { get; set; }
        public string IdProofTypeName { get; set; }
        public string IdProofDocumentNo { get; set; }
        public string IdProofFront { get; set; }
        public string IdProofBack { get; set; }
        public string AddressProofTypeId { get; set; }
        public string AddressProofTypeName { get; set; }
        public string AddressProofDocumentNo { get; set; }
        public string AddressProofFront { get; set; }
        public string AddressProofBack { get; set; }
    }
}