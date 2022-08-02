using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HPCL.Common.Models.ViewModel.Merchant
{
    public class SearchMerchantModel : BaseEntity
    {
        public SearchMerchantModel()
        {
            TerminalStatusResponseModals = new List<TerminalStatusResponseModal>();
            TerminalStatusResponseModals.Add(new TerminalStatusResponseModal
            {
                StatusId = 0,
                StatusName = "-ALL-"
            });
        }
        [Required(ErrorMessage = FieldValidation.MerchantNotEmpty)]
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidMerchantId, ErrorMessage = FieldValidation.ValidMerchantIdErrMsg)]
        public string MerchantId { get; set; }
        public string ErpCode { get; set; }
        public string RetailOutletName { get; set; }
        public string RetailOutletCity { get; set; }
        public string HighwayNo { get; set; }
        public string MerchantStatus { get; set; }
        public virtual List<TerminalStatusResponseModal> TerminalStatusResponseModals { get; set; }
    }
}
