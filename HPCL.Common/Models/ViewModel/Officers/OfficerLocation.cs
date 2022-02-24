using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ViewModel.Officers
{
    public class OfficerLocationModel
    {
        public OfficerLocationModel()
        {
            ZoneOffices = new List<ZonalOfficeResponseModal>();
            RegionalOffices = new List<RegionalOfficeResponseModal>();
            LocationMappings = new List<LocationMappingResponseModal>();
            ZoneOffices.Add(new ZonalOfficeResponseModal
            {
                ZonalOfficeID = 0,
                ZonalOfficeName = "-- Select --"
            });
        }
        [Required(ErrorMessage = "Zonal Office is Required")]
        public int ZoneOfcID { get; set; }
        public int RegionalOfcID { get; set; }
        [Required(ErrorMessage = "User Name is Required")]
        public string UserName { get; set; }
        public bool IsNewUser { get; set; }
        public int OfficerID { get; set; }
        public virtual List<ZonalOfficeResponseModal> ZoneOffices { get; set; }
        public virtual List<RegionalOfficeResponseModal> RegionalOffices { get; set; }
        public virtual List<UserName> UserNames { get; set; }
        public List<LocationMappingResponseModal> LocationMappings { get; set; }
    }
    //public class ZoneOffice
    //{
    //    public int ZonalOfficeID { get; set; }
    //    public string ZonalOfficeName { get; set; }
    //}
    //public class RegionalOffice
    //{
    //    public int RegionalOfficeID { get; set; }
    //    public string RegionalOfficeName { get; set; }
    //}
    public class UserName
    {
        public int UserID { get; set; }
        public string UserNameValue { get; set; }
    }

    //public class LocationMapping
    //{
    //    public string OfficerId { get; set; }
    //    public string ZOId { get; set; }
    //    public string ZOName { get; set; }
    //    public string ROId { get; set; }
    //    public string ROName { get; set; }
    //}
}
