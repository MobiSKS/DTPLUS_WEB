using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel.Approvals
{
    public class TerminalDeInstallationRequestAuthorizationRequestModal
    {
        public TerminalDeInstallationRequestAuthorizationRequestModal()
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
            FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
            SBUTypes = new List<SbuTypeResponseModal>();
        }
        public virtual List<SbuTypeResponseModal> SBUTypes { get; set; }
        public string SBUTypeId { get; set; }
        public string ZonalOfficeId { get; set; }
        public string RegionalOfficeId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string MerchantID { get; set; }
        public string TerminalID { get; set; }
        public virtual List<RegionalOfficeResponseModal> RegionMdl { get; set; }
        public virtual List<ZonalOfficeResponseModal> ZoneMdl { get; set; }
    }
}
