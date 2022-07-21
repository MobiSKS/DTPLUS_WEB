using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.JCB
{
    public class JCBUpdateMobile : BaseEntity
    {
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
    }

    public class JCBUpdateMobileModal
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