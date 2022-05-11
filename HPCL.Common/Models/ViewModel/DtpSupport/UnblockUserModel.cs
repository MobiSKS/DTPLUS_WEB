using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.DtpSupport
{
    public class UnblockUserModel : BaseEntity
    {
        //[Required(ErrorMessage = FieldValidation.CustomerNotEmpty)]
        //[StringLength(10)]
        //[RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string CustomerId { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter Remarks")]
        [RegularExpression(FieldValidation.ValidRemarks, ErrorMessage = FieldValidation.ValidRemarksErrMsg)]
        public string Remark { get; set; }
        public string CreatedTime { get; set; }
        public string Name { get; set; }
        public string Success { get; set; }
        public string UpdateStatus { get; set; }
        public int StatusCode { get; set; }
        public int Status { get; set; }
        public string UserName1 { get; set; }

    }
}