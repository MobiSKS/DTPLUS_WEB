using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class GetLimitUpdateForSingleRechargeCards : BaseEntity
    {
        [Required(ErrorMessage = FieldValidation.CustomerNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string CustomerID { get; set; }
        [StringLength(16)]
        [RegularExpression(FieldValidation.ValidCardNo, ErrorMessage = FieldValidation.ValidCardNoErrMsg)]
        public string Cardno { get; set; }
    }
}
