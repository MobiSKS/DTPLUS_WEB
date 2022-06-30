using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Security
{
    public class GetUserRolesAndRegions:CommonResponseBase
    {
        public UserRolesAndRegionsData Data { get; set; }
    }
    public class UserRolesAndRegionsData
    {
        public List<UserLocation> Locations { get; set; }
        public List<UserRoles> UserRoles { get; set; }
    }
    public class UserLocation
    {
        public string ZonalOfficeID { get; set; }
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeID { get; set; }
        public string RegionalOfficeName { get; set; }
    }
    public class UserRoles
    {
        public string ID { get; set; }
        public string UserRole { get; set; }
    }
}
