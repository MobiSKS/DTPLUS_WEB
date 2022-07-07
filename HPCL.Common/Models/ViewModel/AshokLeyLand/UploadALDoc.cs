using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.AshokLeyLand
{
    public class UploadALDoc : BaseEntity
    {
        public string CustomerID { get; set; }

        [Display(Name = "IdProofType")]
        [Required(ErrorMessage = "ID Proof Type is required")]
        public int IdProofType { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "ID Proof Document Number is required")]
        [RegularExpression(FieldValidation.ValidDocumentNumber, ErrorMessage = FieldValidation.ValidDocumentNumberErrMsg)]
        public string IdProofDocumentNo { get; set; }

        [Required(ErrorMessage = "ID Proof Front Photo is required")]
        public IFormFile IdProofFront { get; set; }

        [Required(ErrorMessage = "ID Proof Back Photo is required")]
        public IFormFile IdProofBack { get; set; }

        [Display(Name = "Select Address Proof Type")]
        [Required(ErrorMessage = "Address Proof Type is required")]
        public int AddressProofType { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Address Proof Document Number is required")]
        [RegularExpression(FieldValidation.ValidDocumentNumber, ErrorMessage = FieldValidation.ValidDocumentNumberErrMsg)]
        public string AddressProofDocumentNo { get; set; }

        [Required(ErrorMessage = "Address Proof Front Photo is required")]
        public IFormFile AddressProofFront { get; set; }

        [Required(ErrorMessage = "Address Proof Back Photo is required")]
        public IFormFile AddressProofBack { get; set; }

        [Required(ErrorMessage = "Please enter 10 digits Form Number")]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidFormNo, ErrorMessage = FieldValidation.ValidFormNoErrMsg)]
        public string FormNumber { get; set; }
        public string CustomerName { get; set; }
        public string IdProofFrontSRC { get; set; }
        public string CustomerTypeId { get; set; }
        public string Type { get; set; }

        [Display(Name = "Select PAN Card Type")]
        [Required(ErrorMessage = "PAN Card Type is required")]
        public int PanCardType { get; set; }


        [Required(ErrorMessage = "PAN Card is required")]
        public IFormFile PanCard { get; set; }
        [MaxLength(20)]
        [Required(ErrorMessage = "Vehicle Details Type is required")]
        [RegularExpression(FieldValidation.ValidDocumentNumber, ErrorMessage = FieldValidation.ValidDocumentNumberErrMsg)]
        public string VehicleDetailsType { get; set; }


        [Required(ErrorMessage = "Vehicle Details is required")]
        public IFormFile VehicleDetails { get; set; }
        [MaxLength(20)]
        [Required(ErrorMessage = "Customer Form Type is required")]
        [RegularExpression(FieldValidation.ValidDocumentNumber, ErrorMessage = FieldValidation.ValidDocumentNumberErrMsg)]
        public string CustomerFormType { get; set; }


        [Required(ErrorMessage = "Customer Form is required")]
        public IFormFile CustomerForm { get; set; }
        public string IdProofFrontimg { get; set; }
        public string IdProofBackimg { get; set; }
        public string AddProofFrontimg { get; set; }
        public string AddProofBackimg { get; set; }
        public string PanFrontimg { get; set; }
        public string VehFrontimg { get; set; }
        public string CustFrontimg { get; set; }
    }
}