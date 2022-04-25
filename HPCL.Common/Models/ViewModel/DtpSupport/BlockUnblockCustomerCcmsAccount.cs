using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.DtpSupport
{
    public class BlockUnblockCustomerCcmsAccount : BaseEntity
    {
        [Required(ErrorMessage = FieldValidation.CustomerNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string CustomerID { get; set; }
        public int CCMSBalanceStatus { get; set; }
        [Required(ErrorMessage = "Remark should not be blank")]
        [RegularExpression(FieldValidation.ValidRemarks, ErrorMessage = FieldValidation.ValidRemarksErrMsg)]
        public string CCMSBalanceRemarks { get; set; }
    }
}
