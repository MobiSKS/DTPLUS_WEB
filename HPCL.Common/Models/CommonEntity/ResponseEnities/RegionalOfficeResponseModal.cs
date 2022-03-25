using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.CommonEntity.ResponseEnities
{
    public class RegionalOfficeResponseModal
    {
        public int ZonalID { get; set; }
        public int RegionalOfficeID { get; set; }
        public string RegionalOfficeName { get; set; }
        public string RegionalOfficeCode { get; set; }
        public string RegionalOfficeShortName { get; set; }
        public string RegionalOfficeERPCode { get; set; }
        public string ZonalOfficeName { get; set; }
    }
}
