using System.ComponentModel.DataAnnotations;

namespace HPCL_Web.Models.Cards.ManageCards
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
        [StringLength(10)]
        [Required(ErrorMessage ="Mobile Number is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage ="Enter a valid mobile number")]
        public string MobileNumber { get; set; }
        public int LimitType { get; set; }
        public string Amount { get; set; }
    }
}
