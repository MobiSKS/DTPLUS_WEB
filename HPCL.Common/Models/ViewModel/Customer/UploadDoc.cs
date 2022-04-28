using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.CommonResponse;
using HPCL.Common.Resources;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.Customer
{
    public class UploadDoc : BaseEntity
    {
        [Required(ErrorMessage = "Please enter 10 digits Customer Reference No.")]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerReferenceNo, ErrorMessage = FieldValidation.ValidCustomerReferenceNoErrMsg)]
        public string CustomerReferenceNo { get; set; }

        [Display(Name = "IdProofType")]
        [Required(ErrorMessage = "{0} is required")]
        public int IdProofType { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Id Proof Document Number is required")]
        [RegularExpression(FieldValidation.ValidDocumentNumber, ErrorMessage = FieldValidation.ValidDocumentNumberErrMsg)]
        public string IdProofDocumentNo { get; set; }

        [Required(ErrorMessage = "Id Proof Front Photo is required")]
        public IFormFile IdProofFront { get; set; }

        [Required(ErrorMessage = "Id Proof Back Photo is required")]
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

        public string FormNumber { get; set; }
        public string CustomerName { get; set; }
        public string IdProofFrontSRC { get; set; }

        //public ValidationResult Validate(ValidationContext validationContext)
        //{
        //    var numAttachments = AddressProofFront;
        //    if (numAttachments == 0)
        //    {
        //        yield return new ValidationResult(
        //            "You must attached at least one file.",
        //            new string[] { nameof(Attachments) });
        //    }
        //}
    }

    public class UploadDocResponse: ResponseMsg
    {
        public List<UploadDocData> data { get; set; }
    }

    public class UploadDocData
    {
        public string CustomerName { get; set; }
        public string FormNumber { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }

    public class UpdateKycResponse
    {
        public int Status { get; set; }
        public string Reason { get; set; }
    }

    //public class CustomImageValidate : ValidationAttribute
    //{
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        var model = (Customer.UploadDoc)validationContext.ObjectInstance;
    //        if (value.FileName == model.IdProofBack.FileName)
    //        {
    //            return new ValidationResult("Please use diiferent Image");
    //        }
    //        else
    //        {
    //            return ValidationResult.Success;
    //        }
    //    }
    //}
}
