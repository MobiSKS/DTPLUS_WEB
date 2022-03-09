using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class SetIndividualLimit : BaseEntity
    {
        [Required(ErrorMessage = FieldValidation.CustomerNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string CustomerId { get; set; }
        [StringLength(14)]
        public string CardNo { get; set; }
        [Required(ErrorMessage = FieldValidation.MobNoNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidMobileNumber, ErrorMessage = FieldValidation.ValidMobileNumberErrMsg)]
        public string MobileNo { get; set; }
        public string VehicleNo { get; set; }
    }
}
