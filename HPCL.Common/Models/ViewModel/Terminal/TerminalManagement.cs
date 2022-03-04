using HPCL.Common.Models.CommonEntity;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.Terminal
{
    public class TerminalManagement : BaseEntity
    {
        [Required(ErrorMessage = "Merchant Id is Required")]
        [StringLength(10)]
        [RegularExpression(@"^(?=(3))[0-9]{10}$", ErrorMessage = "start with 3 and contains 10 digits")]
        public string MerchantId { get; set; }
        public string ZonalOfficeId { get; set; }
        public string RegionalOfficeId { get; set; }
    }
}
 