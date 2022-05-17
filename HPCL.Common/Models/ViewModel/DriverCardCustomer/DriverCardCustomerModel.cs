using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using Microsoft.AspNetCore.Http;

namespace HPCL.Common.Models.ViewModel.DriverCardCustomer
{
    public class DriverCardCustomerModel : BaseEntity
    {

        public DriverCardCustomerModel()
        {

            CustomerStateMdl = new List<StateResponseModal>();
            CustomerStateMdl.Add(new StateResponseModal
            {
                CountryID = 0,
                StateID = 0,
                StateName = "Select State"
            });

            DocumentTypeMdl = new List<ProofType>();
            DocumentTypeMdl.Add(new ProofType
            {
                ProofTypeId = 0,
                ProofTypeName = "Select Document Type"
            });
        }

        [Required(ErrorMessage = "Driver Title is Required")]
        public string IndividualOrgNameTitle { get; set; }

        [Required(ErrorMessage = "Driver Name is Required")]
        public string IndividualOrgName { get; set; }

        //[Required(ErrorMessage = "Income Tax Pan is Required")]
        public string IncomeTaxPan { get; set; }

        [Required(ErrorMessage = "Communication Address is Required")]
        public string CommunicationAddress1 { get; set; }

        [Required(ErrorMessage = "Communication City is Required")]
        public string CommunicationCityName { get; set; }

        [Required(ErrorMessage = "Communication PIN is Required")]
        public string CommunicationPincode { get; set; }

        [Required(ErrorMessage = "Communication State is Required")]
        public int CommunicationStateId { get; set; }

        [Required(ErrorMessage = "Address Proof is required")]
        public IFormFile AddressProof { get; set; }
        public string CommunicationDialCode { get; set; }
        public string CommunicationPhonePart2 { get; set; }
        public string CommunicationPhoneNo { get; set; }

        [Required(ErrorMessage = "Communication Mobile No is Required")]
        public string CommunicationMobileNo { get; set; }

        [Required(ErrorMessage = "Communication Email is Required")]
        public string CommunicationEmailid { get; set; }

        [Required(ErrorMessage = "Communication Form Number is Required")]
        public string FormNumber { get; set; }
        public string MerchantId { get; set; }

        public string IfDTPCustomer { get; set; }

        [Required(ErrorMessage = "Card Number is Required")]
        public string CardNo { get; set; }

        [Required(ErrorMessage = "Driving Licence is Required")]
        public string DrivingLicence { get; set; }

        [Required(ErrorMessage = "File Name is Required")]
        public string AddressProofDocumentNo { get; set; }

        public virtual List<StateResponseModal> CustomerStateMdl { get; set; }
        public virtual List<ProofType> DocumentTypeMdl { get; set; }

        public int RegionalOfficeId { get; set; }
        public int Internel_Status_Code { get; set; }
        public string Remarks { get; set; }

        [Required(ErrorMessage = "Document Type is Required")]
        public string AddressProofType { get; set; }

        public string ExistingCustomerId { get; set; }
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Beneficiary Name is Required")]
        public string BeneficiaryName { get; set; }

        [Required(ErrorMessage = "Relation With Beneficiary is Required")]
        public string RelationWithBeneficiary { get; set; }

        [Required(ErrorMessage = "Beneficiary Mobile is Required")]
        public string BeneficiaryMobile { get; set; }
        public string LoggedInAs { get; set; }
        public string OutletName { get; set; }
        public string Zone { get; set; }
        public string RegionalOffice { get; set; }
        public string ExternalPANAPIStatus { get; set; }

    }


}