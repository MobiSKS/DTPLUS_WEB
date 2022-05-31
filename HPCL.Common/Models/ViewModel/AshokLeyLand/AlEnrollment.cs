using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.AshokLeyLand
{
    public class AlEnrollment : BaseEntity
    {
        [Required(ErrorMessage = "Dealer Code field cannot be left blank")]
        [RegularExpression(FieldValidation.ValidDealerCode, ErrorMessage = FieldValidation.ValidDealerCodeErrMsg)]
        public string DealerCode { get; set; }
        public string DTPDealerCode { get; set; }
        [Required(ErrorMessage = "Dealer Name field cannot be left blank")]
        public string DealerName { get; set; }
        [Required(ErrorMessage = "Zonal Office field cannot be left blank")]
        public int ZonalOfficeId { get; set; }
        [Required(ErrorMessage = "Regional Office field cannot be left blank")]
        public int RegionalOfficeId { get; set; }
        [Required(ErrorMessage = "Address1 field cannot be left blank")]
        //[RegularExpression(FieldValidation.ValidAddress, ErrorMessage = FieldValidation.ValidAddressErrMsg)]
        public string Address1 { get; set; }
        [Required(ErrorMessage = "Address2 field cannot be left blank")]
        //[RegularExpression(FieldValidation.ValidAddress, ErrorMessage = FieldValidation.ValidAddressErrMsg)]
        public string Address2 { get; set; }

        //[RegularExpression(FieldValidation.ValidAddress, ErrorMessage = FieldValidation.ValidAddressErrMsg)]
        public string Address3 { get; set; }
        [Required(ErrorMessage = "State field cannot be left blank")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "City Name field cannot be left blank")]
        //[RegularExpression(FieldValidation.ValidCity, ErrorMessage = FieldValidation.ValidCityErrMsg)]
        public string CityName { get; set; }
        [Required(ErrorMessage = "District field cannot be left blank")]
        public int DistrictId { get; set; }
        [StringLength(6)]
        [Required(ErrorMessage = "PIN Code field cannot be left blank")]
        //[RegularExpression(FieldValidation.ValidPinCode, ErrorMessage = FieldValidation.ValidPinCodeErrMsg)]
        public string Pin { get; set; }
        [StringLength(10)]
        [Required(ErrorMessage = "Mobile Number field cannot be left blank")]
        //[RegularExpression(FieldValidation.ValidMobileNumber, ErrorMessage = FieldValidation.ValidMobileNumberErrMsg)]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Email ID field cannot be left blank")]
        [RegularExpression(FieldValidation.ValidEmail, ErrorMessage = FieldValidation.ValidEmailErrMsg)]
        public string EmailId { get; set; }
    }
}
