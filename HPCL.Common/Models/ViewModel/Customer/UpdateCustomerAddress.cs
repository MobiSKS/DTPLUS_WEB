using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ViewModel.Officers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Customer
{
    public class UpdateCustomerAddress : BaseEntity
    {
        public UpdateCustomerAddress()
        {
            CustomerStateMdl = new List<StateResponseModal>();
            CustomerStateMdl.Add(new StateResponseModal
            {
                CountryID = 0,
                StateID = 0,
                StateName = "Select State"
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
        public string CustomerId { get; set; }
        public string IndividualOrgName { get; set; }

        public virtual List<StateResponseModal> CustomerStateMdl { get; set; }
        public virtual List<OfficerDistrictModel> CommunicationDistrictMdl { get; set; }
        public virtual List<OfficerDistrictModel> PerOrRegAddressDistrictMdl { get; set; }


        [Required(ErrorMessage = "Income Tax Pan is Required")]
        public string IncomeTaxPan { get; set; }
               
        public string CommunicationAddress1 { get; set; }

        [Required(ErrorMessage = "Address field cannot be left blank")]
        public string CommunicationAddress2 { get; set; }
        public string CommunicationAddress3 { get; set; }
        public string CommunicationLocation { get; set; }

        [Required(ErrorMessage = "City is Required under communication Address")]
        public string CommunicationCityName { get; set; }
        [Required(ErrorMessage = "Pin code is Required under communication Address")]
        public string CommunicationPinCode { get; set; }
        [Required(ErrorMessage = "State is Required under communication Address")]
        public int CommunicationStateId { get; set; }

        [Required(ErrorMessage = "State is Required under communication Address")]
        public string CommunicationState { get; set; }
        [Required(ErrorMessage = "District is Required under communication Address")]
        public string CommunicationDistrictId { get; set; }

        public string CommunicationDialCode { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value only")]
        public string CommunicationPhonePart2 { get; set; }
        public string CommunicationPhoneNo { get; set; }
        public string CommunicationFaxCode { get; set; }
        public string CommunicationFaxPart2 { get; set; }
        public string CommunicationFax { get; set; }

        [Required(ErrorMessage = "Mobile Phone number is Required under communication Address")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value only")]
        public string CommunicationMobileNo { get; set; }


        [Required(ErrorMessage = "E-mail is Required under communication Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string CommunicationEmailid { get; set; }


        [Required(ErrorMessage = "Address field cannot be left blank")]
        public string PermanentAddress1 { get; set; }
        [Required(ErrorMessage = "Address field cannot be left blank")]
        public string PermanentAddress2 { get; set; }

        public string PermanentAddress3 { get; set; }

        public string PermanentLocation { get; set; }

        [Required(ErrorMessage = "City is Required under Permanent/Registered Address")]
        public string PermanentCityName { get; set; }
        [Required(ErrorMessage = "Pin code is Required under Permanent/Registered Address")]
        public string PermanentPincode { get; set; }
        [Required(ErrorMessage = "State is Required")]
        public int PermanentStateId { get; set; }
        [Required(ErrorMessage = "State is Required")]

        public string PerOrRegAddressState { get; set; }
        [Required(ErrorMessage = "District is Required")]
        public string PerOrRegAddressDistrict { get; set; }

        public int PermanentDistrictId { get; set; }
        public string PermanentPhoneNo { get; set; }
        public string PerOrRegAddressDialCode { get; set; }

        public string PerOrRegAddressPhoneNumber { get; set; }
        public string PermanentFaxCode { get; set; }
        public string PermanentFaxPhoneNumber { get; set; }
        public string PermanentFax { get; set; }
        public string Remarks { get; set; }
        public int Internel_Status_Code { get; set; }
        public string CustomerReferenceNo { get; set; }
        public string IsDuplicatePanNo { get; set; }
        public string AllowPanDuplication { get; set; }
        public string PanCardRemarks { get; set; }
        public string ExternalPANAPIStatus { get; set; }
        public string Message { get; set; }
        public string CASID { get; set; }
        public string PASID { get; set; }
        public string CADID { get; set; }
        public string PADID { get; set; }
        public int TBEntityId { get; set; }
        public string TBEntityName { get; set; }
        public string FormNumber { get; set; }
    }
}