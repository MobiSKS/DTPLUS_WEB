using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models.Officer
{
    public class OfficerLocationModel
    {
        public OfficerLocationModel()
        {
            ZoneOffices = new List<ZoneOffice>();
            RegionalOffices = new List<RegionalOffice>();
            LocationMappings = new List<LocationMapping>();
            ZoneOffices.Add(new ZoneOffice
            {
                ZonalOfficeID = 0,
                ZonalOfficeName = "-- Select --"
            });
        }
        [Required(ErrorMessage = "Zonal Office is Required")]
        public string ZoneOfcID { get; set; }
        public string RegionalOfcID { get; set; }
        [Required(ErrorMessage = "User Name is Required")]
        public string UserName { get; set; }
        public bool IsNewUser { get; set; }
        public string OfficerID { get; set; }
        public virtual List<ZoneOffice> ZoneOffices { get; set; }
        public virtual List<RegionalOffice> RegionalOffices { get; set; }
        public virtual List<UserName> UserNames { get; set; }
        public List<LocationMapping> LocationMappings { get; set; }
    }
    public class ZoneOffice
    {
        public int ZonalOfficeID { get; set; }
        public string ZonalOfficeName { get; set; }
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

    public class LocationMapping
    {
        public string OfficerId { get; set; }
        public string ZOId { get; set; }
        public string ZOName { get; set; }
        public string ROId { get; set; }
        public string ROName { get; set; }
    }
}
