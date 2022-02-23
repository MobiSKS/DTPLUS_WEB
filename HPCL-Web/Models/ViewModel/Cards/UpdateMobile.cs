using HPCL.Common.Resources;
using HPCL_Web.Models.CommonEntity;
using System.ComponentModel.DataAnnotations;

namespace HPCL_Web.Models.ViewModel.Cards
{
    public class UpdateMobile : BaseEntity
    {
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class UpdateMobileModal
    {
        public string CardNumber { get; set; }
        [Required(ErrorMessage = FieldValidation.MobNoNotEmpty)]
        [RegularExpression(FieldValidation.ValidMobileNumber, ErrorMessage = FieldValidation.ValidMobileNumberErrMsg)]
        public string MobileNumber { get; set; }
        public string LimitTypeName { get; set; }
        public string CCMSReloadSaleLimitValue { get; set; }
    }
}
