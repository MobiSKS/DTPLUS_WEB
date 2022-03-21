using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.ViewCard
{
    public class ViewCardDetails : BaseEntity
    {

        [Required(ErrorMessage = "CustomerID Required")]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        [StringLength(10)]
        public string Customerid { get; set; }

        public string CardNo { get; set; }
        public string MobileNo { get; set; }
        public string VechileNo { get; set; }
        public string FastagNo { get; set; }

        public string ModifiedBy { get; set; }





    }
}