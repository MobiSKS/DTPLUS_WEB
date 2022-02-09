using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models
{
    public class CustomerModel
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
        }

        public virtual List<CardDetails> CardDetailsMdl { get; set; }
        public virtual List<CustomerTypeModel> CustomerTypeMdl { get; set; }
        public virtual List<CustomerSubTypeModel> CustomerSubTypeMdl { get; set; }
        public virtual List<CustomerZonalOfficeModel> CustomerZonalOfficeMdl { get; set; }
        public virtual List<CustomerRegionModel> CustomerRegionMdl { get; set; }
        public virtual List<CustomerStateModel> CustomerStateMdl { get; set; }
        public virtual List<CustomerDistrictModel> CustomerDistrictMdl { get; set; }
        public virtual List<CustomerTbentityModel> CustomerTbentityMdl { get; set; }
        public virtual List<CustomerTypeOfFleetModel> CustomerTypeOfFleetMdl { get; set; }
        public virtual List<CustomerSecretQueModel> CustomerSecretQueMdl { get; set; }
        public virtual List<VehicleTypeModel> VehicleTypeMdl { get; set; }
        

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

        [Required(ErrorMessage = "Date of application is Required")]
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
        [Required(ErrorMessage = "Communication Address 2 is Required")]
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
        //[Required(ErrorMessage = "Phone Number is Required under communication Address")]
        //[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value only")]
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
        //[Required(ErrorMessage = "Address 3 is Required in Permanent/Registered  Address")]
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
        [Required(ErrorMessage = "Phone Number is Required under Permanent/Registered Address")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value only")]
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

    public class CardDetailsInput
    {
        public string CustomerName { get; set; }
        public string FormNumber { get; set; }
        public string CustomerReferenceNo { get; set; }
        public int NoOfCards { get; set; }
        public int RBEId { get; set; }
        public int FeePaymentsCollectFeeWaiver { get; set; }
        public int FeePaymentNo { get; set; }
        public string FeePaymentDate { get; set; }
        public string CardPreference { get; set; }
        public List<CardDetails> CardDetails { get; set; }
    }

}