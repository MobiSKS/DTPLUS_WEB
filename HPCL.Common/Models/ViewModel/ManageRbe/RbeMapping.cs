using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.ManageRbe
{
    public class RbeMapping : BaseEntity
    {
        [RegularExpression(FieldValidation.ValidFirstName, ErrorMessage = FieldValidation.ValidFirstNameErrMsg)]
        public string FirstName { get; set; }

        [RegularExpression(FieldValidation.ValidMobileNumber, ErrorMessage = FieldValidation.ValidUserNameErrMsg)]
        [StringLength(10)]
        public string UserName { get; set; }
    }
}
