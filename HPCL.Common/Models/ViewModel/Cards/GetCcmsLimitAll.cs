using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class GetCcmsLimitAll : BaseEntity
    {
        [Required(ErrorMessage = FieldValidation.CustomerNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string CustomerId { get; set; }
        public int StatusFlag { get; set; }

        //[Required(ErrorMessage = "Required Type of Limit")]
        public int TypeOfLimit { get; set; }
        public int CcmsLimitRs { get; set; }
    }


    public class UpdateCcmsLimitAll : BaseEntity
    {
        public string CustomerId { get; set; }
        public int LimitType { get; set; }
        [Required(ErrorMessage = FieldValidation.AmountNotEmpty)]
        [RegularExpression(FieldValidation.ValidAmount, ErrorMessage = FieldValidation.ValidAmountErrMsg)]
        public int Amount { get; set; }
    }
}
