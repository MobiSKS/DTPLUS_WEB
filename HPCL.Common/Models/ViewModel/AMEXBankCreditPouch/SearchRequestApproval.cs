using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.AMEXBankCreditPouch
{
    public class SearchRequestApproval : BaseEntity
    {
        public string CustomerId { get; set; }
        public int ZO { get; set; }
        public int RO { get; set; }
        public int Status { get; set; }
    }

    public class SearchRequestApprovalClone
    {
        //[Required(ErrorMessage = FieldValidation.CustomerNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string CustomerId { get; set; }
        public string ZO { get; set; }
        public string RO { get; set; }
        public int Status { get; set; }
    }
}
