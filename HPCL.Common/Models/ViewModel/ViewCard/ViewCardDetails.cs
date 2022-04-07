using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.ViewCard
{
    public class ViewCardDetails : BaseEntity
    {
        [Required(ErrorMessage = "Invalid CustomerId")]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        [StringLength(10)]
        public string Customerid { get; set; }
        [RegularExpression(FieldValidation.ValidCardNo, ErrorMessage = FieldValidation.ValidCardNoErrMsg)]
        [StringLength(16)]
        public string CardNo { get; set; }
        [RegularExpression(FieldValidation.ValidMobileNumber, ErrorMessage = FieldValidation.ValidMobileNumberErrMsg)]
        [StringLength(10)]
        public string MobileNo { get; set; }
        public string VechileNo { get; set; }
        public string FastagNo { get; set; }
    }
}