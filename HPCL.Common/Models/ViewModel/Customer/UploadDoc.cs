using HPCL.Common.Models.CommonEntity;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.Customer
{
    public class UploadDoc : BaseEntity
    {
        public UploadDoc()
        {
            ProofTypesModal = new List<ProofType>();
        }

        [Required(ErrorMessage = "Customer Reference No is required")]
        public string CustomerReferenceNo { get; set; }

        [Display(Name = "IdProofType")]
        [Required(ErrorMessage = "{0} is required")]
        public int IdProofType { get; set; }

        [Required(ErrorMessage = "Id Proof Document Number is required")]
        public string IdProofDocumentNo { get; set; }

        [Required(ErrorMessage = "Id Proof Front Photo is required")]
        public IFormFile IdProofFront { get; set; }

        [Required(ErrorMessage = "Id Proof Back Photo is required")]
        public IFormFile IdProofBack { get; set; }

        [Display(Name = "Select Address Proof Type")]
        [Required(ErrorMessage = "Address Proof Type is required")]
        public int AddressProofType { get; set; }

        [Required(ErrorMessage = "Address Proof Document Number is required")]
        public string AddressProofDocumentNo { get; set; }

        [Required(ErrorMessage = "Address Proof Front Photo is required")]
        public IFormFile AddressProofFront { get; set; }

        [Required(ErrorMessage = "Address Proof Back Photo is required")]
        public IFormFile AddressProofBack { get; set; }

        public virtual List<ProofType> ProofTypesModal { get; set; }
    }

    public class UploadDocResponse
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FormNumber { get; set; }
    }
    
    public class UpdateKycResponse
    {
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
