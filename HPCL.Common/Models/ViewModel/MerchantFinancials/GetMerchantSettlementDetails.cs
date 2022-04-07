using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.MerchantFinancials
{
    public class GetMerchantSettlementDetails : BaseEntity
    {
        public GetMerchantSettlementDetails()
        {
            FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
        [Required(ErrorMessage = FieldValidation.MerchantNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidMerchantId, ErrorMessage = FieldValidation.ValidMerchantIdErrMsg)]
        public string MerchantId { get; set; }

        [Required(ErrorMessage = FieldValidation.TerminalNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidTerminalId, ErrorMessage = FieldValidation.ValidTerminalIdErrMsg)]
        public string TerminalId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
