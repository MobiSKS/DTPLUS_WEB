using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class SearchCards : BaseEntity
    {
        //[Required(ErrorMessage = FieldValidation.CustomerNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string CustomerId { get; set; }
        [StringLength(16)]
        [RegularExpression(FieldValidation.ValidCardNo, ErrorMessage = FieldValidation.ValidCardNoErrMsg)]
        public string CardNo { get; set; }
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidMobileNumber, ErrorMessage = FieldValidation.ValidMobileNumberErrMsg)]
        public string MobileNo { get; set; }
    }

    public class UpdateStatus : BaseEntity
    {
        public string CardNo { get; set; }
        public int Statusflag { get; set; }
    }
}
