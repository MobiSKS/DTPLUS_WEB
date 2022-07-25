using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class GetCardWiseLimit : BaseEntity
    {
        public GetCardWiseLimit()
        {
            fromDate = DateTime.Now.ToString("dd-MM-yyyy");
            toDate = DateTime.Now.ToString("dd-MM-yyyy");
            LimitTypeLst = new List<GetLimitTypeResponse>();
            LimitTypeLst.Add(new GetLimitTypeResponse
            {
                Id = "0",
                LimitType = "Select"
            });
        }
        [StringLength(10)]
        [Required(ErrorMessage = FieldValidation.CustomerNotEmpty)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string customerId { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        [StringLength(16)]
        [RegularExpression(FieldValidation.ValidCardNo, ErrorMessage = FieldValidation.ValidCardNoErrMsg)]
        public string cardNumber { get; set; }
        public string limitType { get; set; }
        public virtual List<GetLimitTypeResponse> LimitTypeLst { get; set; }
    }
}
