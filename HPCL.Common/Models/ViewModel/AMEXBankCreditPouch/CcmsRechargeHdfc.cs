using HPCL.Common.Models.CommonEntity;
using System;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.AMEXBankCreditPouch
{
    public class CcmsRechargeHdfc : BaseEntity
    {
        [Required(ErrorMessage = FieldValidation.CustomerNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string CustomerId { get; set; }
        [MaxLength(10)]
        [Required(ErrorMessage = "Amount is Required")]
        public Decimal Amount { get; set; }
    }
}
