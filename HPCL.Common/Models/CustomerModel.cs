using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPCL.Common.Models.ViewModel.Officers;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ViewModel.Customer;
using HPCL.Common.Models.CommonEntity.ResponseEnities;

namespace HPCL.Common.Models
{
    public class CustomerModel : BaseEntity
    {
        public CustomerModel()
        {
            CustomerTypeMdl = new List<CustomerTypeModel>();
            CustomerTypeMdl.Add(new CustomerTypeModel
            {
                CustomerTypeID = 0,
                CustomerTypeName = "Select Customer Type"

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
                districtName = "Select District"
            });

            PerOrRegAddressDistrictMdl = new List<OfficerDistrictModel>();
            PerOrRegAddressDistrictMdl.Add(new OfficerDistrictModel
            {
                districtID = 0,
                districtName = "Select District"
            });

        }

        public virtual List<CardDetails> CardDetailsMdl { get; set; }
        public virtual List<CustomerTypeModel> CustomerTypeMdl { get; set; }
        public virtual List<CustomerSubTypeModel> CustomerSubTypeMdl { get; set; }
        public virtual List<CustomerZonalOfficeModel> CustomerZonalOfficeMdl { get; set; }
        public virtual List<CustomerRegionModel> CustomerRegionMdl { get; set; }
        public virtual List<StateResponseModal> CustomerStateMdl { get; set; }
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
        public int Internel_Status_Code { get; set; }
        public string CustomerReferenceNo { get; set; }
        public string IsDuplicatePanNo { get; set; }
        public string AllowPanDuplication { get; set; }
        public string PanCardRemarks { get; set; }
        
    }

    

}