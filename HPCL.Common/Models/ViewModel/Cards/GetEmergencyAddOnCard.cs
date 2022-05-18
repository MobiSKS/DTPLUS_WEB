using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class GetEmergencyAddOnCard : BaseEntity
    {
        [Required(ErrorMessage = FieldValidation.CustomerNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string CustomerId { get; set; }
        [MaxLength(2)]
        [Required(ErrorMessage = FieldValidation.NoOfTatkalCardsNotEmpty)]
        [RegularExpression(FieldValidation.NoOfTatkalCardsNum, ErrorMessage = FieldValidation.NoOfTatkalCardsNumErrMsg)]
        public string NoOfTatkalCards { get; set; }
    }
}
