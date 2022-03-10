using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.AshokLeyLand
{
    public class AlEnrollment : BaseEntity
    {
        [Required(ErrorMessage = "Dealer Code should not be empty")]
        [RegularExpression(FieldValidation.ValidDealerCode, ErrorMessage = FieldValidation.ValidDealerCodeErrMsg)]
        public string DealerCode { get; set; }
        public string DTPDealerCode { get; set; }
        [Required(ErrorMessage = "Dealer Name should not be empty")]
        public string DealerName { get; set; }
        [Required(ErrorMessage = "Zonal Office should not be empty")]
        public int ZonalOfficeId { get; set; }
        [Required(ErrorMessage = "Regional Office should not be empty")]
        public int RegionalOfficeId { get; set; }
        [Required(ErrorMessage = "Address1 should not be empty")]
        public string Address1 { get; set; }
        [Required(ErrorMessage = "Address2 should not be empty")]
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        [Required(ErrorMessage = "State should not be empty")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "City Name should not be empty")]
        public string CityName { get; set; }
        [Required(ErrorMessage = "District should not be empty")]
        public int DistrictId { get; set; }
        [Required(ErrorMessage = "Pin should not be empty")]
        [RegularExpression(FieldValidation.ValidPinCode, ErrorMessage = FieldValidation.ValidPinCodeErrMsg)]
        public string Pin { get; set; }
        [Required(ErrorMessage = FieldValidation.MobNoNotEmpty)]
        [RegularExpression(FieldValidation.ValidMobileNumber, ErrorMessage = FieldValidation.ValidMobileNumberErrMsg)]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Email ID should not be empty")]
        [RegularExpression(FieldValidation.ValidEmail, ErrorMessage = FieldValidation.ValidEmailErrMsg)]
        public string EmailId { get; set; }
    }
}
