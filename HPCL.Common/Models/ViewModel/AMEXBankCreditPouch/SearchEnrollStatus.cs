using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.AMEXBankCreditPouch
{
    public class SearchEnrollStatus : BaseEntity
    {
        public SearchEnrollStatus()
        {
            FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
        }

        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string CustomerId { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }
        public string ZO { get; set; }
        public string RO { get; set; }
    }
}
