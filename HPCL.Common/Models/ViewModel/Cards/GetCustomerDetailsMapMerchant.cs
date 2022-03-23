using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class GetCustomerDetailsMapMerchant : BaseEntity
    {
        [Required(ErrorMessage = FieldValidation.CustomerNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string CustomerID { get; set; }
        public string MerchantID { get; set; }
        [Required(ErrorMessage = "Please Select State")]
        public string StateID { get; set; }
        public string DistrictID { get; set; }
        public string City { get; set; }
        public string HighwayName { get; set; }
        public string HighwayNo1 { get; set; }
        public string HighwayNo2 { get; set; }
    }
}
