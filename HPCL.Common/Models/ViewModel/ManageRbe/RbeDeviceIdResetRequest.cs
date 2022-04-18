using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.ManageRbe
{
    public class RbeDeviceIdResetRequest : BaseEntity
    {
        [RegularExpression(FieldValidation.ValidFirstName, ErrorMessage = FieldValidation.ValidFirstNameErrMsg)]
        public string FirstName { get; set; }
        [RegularExpression(FieldValidation.ValidUserName, ErrorMessage = FieldValidation.ValidUserNameErrMsg)]
        public string MobileNo { get; set; }    
    }
}
