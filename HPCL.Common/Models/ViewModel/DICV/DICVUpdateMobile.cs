using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.DICV
{
    public class DICVUpdateMobile : BaseEntity
    {
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
    }
    public class DICVUpdateMobileModal
    {
        public string CardNumber { get; set; }
        [StringLength(10)]
        [Required(ErrorMessage = FieldValidation.MobNoNotEmpty)]
        [RegularExpression(FieldValidation.ValidMobileNumber, ErrorMessage = FieldValidation.ValidMobileNumberErrMsg)]
        public string MobileNumber { get; set; }
        public string LimitTypeName { get; set; }
        public string CCMSReloadSaleLimitValue { get; set; }
    }
}
