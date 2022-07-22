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
    public class JCBViewCardDetails : BaseEntity
    {
        [Required(ErrorMessage = "Please enter Customer ID")]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        [StringLength(10)]
        public string Customerid { get; set; }
        [RegularExpression(FieldValidation.ValidCardNo, ErrorMessage = FieldValidation.ValidCardNoErrMsg)]
        [StringLength(16)]
        public string CardNo { get; set; }
        [RegularExpression(FieldValidation.ValidMobileNumber, ErrorMessage = FieldValidation.ValidMobileNumberErrMsg)]
        [StringLength(10)]
        public string MobileNo { get; set; }
        public string FastagNo { get; set; }
        public string Cardno { get; set; }
        public string Mobileno { get; set; }
        public string Vehiclenumber { get; set; }
        public string VechileNo { get; set; }
        public bool IsNewMapping { get; set; }
    }
}