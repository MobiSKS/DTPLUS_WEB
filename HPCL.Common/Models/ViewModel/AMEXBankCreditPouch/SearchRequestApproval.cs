using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.AMEXBankCreditPouch
{
    public class SearchRequestApproval : BaseEntity
    {
        public SearchRequestApproval()
        {
            ZoneMdl = new List<ZonalOfficeResponseModal>();
            RegionMdl = new List<RegionalOfficeResponseModal>();
            ZoneMdl.Add(new ZonalOfficeResponseModal
            {
                ZonalOfficeID = 0,
                ZonalOfficeName = "--All--"
            });
            RegionMdl.Add(new RegionalOfficeResponseModal
            {
                RegionalOfficeID = 0,
                RegionalOfficeName = "--All--"
            });
            SBUTypes = new List<SbuTypeResponseModal>();
        }

        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        public string CustomerId { get; set; }
        public string ZO { get; set; }
        public string RO { get; set; }
        public int Status { get; set; }
        public string sbuTypeId { get; set; }
        public virtual List<SbuTypeResponseModal> SBUTypes { get; set; }
        public virtual List<RegionalOfficeResponseModal> RegionMdl { get; set; }
        public virtual List<ZonalOfficeResponseModal> ZoneMdl { get; set; }
    }
}
