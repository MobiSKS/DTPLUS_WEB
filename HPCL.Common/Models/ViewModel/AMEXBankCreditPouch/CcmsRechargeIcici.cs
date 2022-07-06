using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.AMEXBankCreditPouch
{
    public class CcmsRechargeAmex : BaseEntity
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
