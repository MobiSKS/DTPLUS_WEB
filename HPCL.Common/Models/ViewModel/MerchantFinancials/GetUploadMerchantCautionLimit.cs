using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.MerchantFinancials
{
    public class GetUploadMerchantCautionLimit : BaseEntity
    {
        //[Required(ErrorMessage = FieldValidation.MerchantNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidMerchantId, ErrorMessage = FieldValidation.ValidMerchantIdErrMsg)]
        public string MerchantId { get; set; }
        public string MerchantStatus { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string RegionalOffice { get; set; }
        public string SalesArea { get; set; }
        public string ZonalOffice { get; set; }
    }
}
