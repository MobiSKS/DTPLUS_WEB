using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.ICICIBankCreditPouch
{
    public class CustomerDetailsReq : BaseEntity
    {
        [Required(ErrorMessage = FieldValidation.CustomerNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string CustomerId { get; set; }
        public string RequestedBy { get; set; }
    }
}
