using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.CustomerSearch
{
    public class ControlCardPinResetReq : BaseEntity
    {
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string CustomerID { get; set; }
        [StringLength(10)]
        public string ControlCardNo { get; set; }
    }
}
