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
    public class DICVCustomerTransactionViewModel : BaseEntity
    {
        public DICVCustomerTransactionViewModel()
        {
            FromDate = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
        [Required(ErrorMessage = FieldValidation.CustomerNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string CustomerID { get; set; }

        public string CardNo { get; set; }
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidMobileNumber, ErrorMessage = FieldValidation.ValidMobileNumberErrMsg)]
        public string MobileNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}