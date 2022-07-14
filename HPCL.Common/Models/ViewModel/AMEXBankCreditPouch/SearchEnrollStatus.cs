using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.AMEXBankCreditPouch
{
    public class SearchEnrollStatus : BaseEntity
    {
        public SearchEnrollStatus()
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
        public virtual List<SbuTypeResponseModal> SBUTypes { get; set; }
        public virtual List<RegionalOfficeResponseModal> RegionMdl { get; set; }
        public virtual List<ZonalOfficeResponseModal> ZoneMdl { get; set; }
    }
}
