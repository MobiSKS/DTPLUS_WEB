using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.ICICIBankCreditPouch
{
    public class GetRequestAuthorizationReq : BaseEntity
    {
        public GetRequestAuthorizationReq()
        {
            FromDate = DateTime.Now.ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
        //[Required(ErrorMessage = FieldValidation.CustomerNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string CustomerId { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }
    }
}
