using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models
{
    public class OfficerLocationModel
    {
        public int ZoneOfcID { get; set; }
        public int RegionalOfcID { get; set; }
        public int UserNameID { get; set; }
        public bool IsNewUser { get; set; }
        public virtual IEnumerable<ZoneOffice> ZoneOffices { get; set; }
        public virtual IEnumerable<RegionalOffice> RegionalOffices { get; set; }
        public virtual IEnumerable<UserName> UserNames { get; set; }
    }
    public class ZoneOffice
    {
        public int ZoneOfficeID { get; set; }
        public string ZomeOfficeName { get; set; }
    }
    public class RegionalOffice
    {
        public int RegionalOfficeID { get; set; }
        public string RegionalOfficeName { get; set; }
    }
    public class UserName
    {
        public int UserID { get; set; }
        public string UserNameValue { get; set; }
    }
}
