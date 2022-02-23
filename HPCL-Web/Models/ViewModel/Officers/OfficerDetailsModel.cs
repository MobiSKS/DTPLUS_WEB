using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models.Officer
{
    public class OfficerDetailsModel
    {
        public OfficerDetailsModel()
        {
            OfficerZones = new List<OfficerZoneModel>();
            OfficerZones.Add(new OfficerZoneModel { 
                ZonalOfficeID = 0,
                ZonalOfficeName = "--ALL--"
            });

            OfficerStates = new List<OfficerStateModel>();
            OfficerStates.Add(new OfficerStateModel
            {
                CountryID = 0,
                StateID = 0,
                StateName = "--ALL--"
            });
        }
        public string ZonalOfcID { get; set; }
        public string RegionalOfcID { get; set; }
        public string StateID { get; set; }
        public string DistrictID { get; set; }
        public string ZonalOfcIdVal { get; set; }
        public string RegionalOfcIdVal { get; set; }
        public string StateIdVal { get; set; }
        public string DistrictIdVal { get; set; }
        public virtual List<OfficerZoneModel> OfficerZones { get; set; }
        public virtual List<OfficerRegionModel> OfficerRegions { get; set; }
        public virtual List<OfficerStateModel> OfficerStates { get; set; }
        public virtual List<OfficerDistrictModel> OfficerDistricts { get; set; }
    }
}
