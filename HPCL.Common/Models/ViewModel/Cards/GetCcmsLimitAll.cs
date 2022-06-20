using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System;
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
        public int TypeOfLimit { get; set; }
        public Decimal CcmsLimitRs { get; set; }
    }


    public class UpdateCcmsLimitAll : BaseEntity
    {
        public string CustomerId { get; set; }
        public int LimitType { get; set; }
        [Required(ErrorMessage = FieldValidation.AmountNotEmpty)]
        [MaxLength(10)]
        [RegularExpression(FieldValidation.ValidAmount, ErrorMessage = FieldValidation.ValidAmountErrMsg)]
        public int Amount { get; set; }
    }
}
