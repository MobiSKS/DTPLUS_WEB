using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ViewModel.Officers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Officers
{
    public class OfficerDetailsModel
    {
        public OfficerDetailsModel()
        {
            OfficerZones = new List<ZonalOfficeResponseModal>();
            OfficerZones.Add(new ZonalOfficeResponseModal
            { 
                ZonalOfficeID = 0,
                ZonalOfficeName = "--ALL--"
            });

            OfficerStates = new List<StateResponseModal>();
            OfficerStates.Add(new StateResponseModal
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
        public virtual List<ZonalOfficeResponseModal> OfficerZones { get; set; }
        public virtual List<RegionalOfficeResponseModal> OfficerRegions { get; set; }
        public virtual List<StateResponseModal> OfficerStates { get; set; }
        public virtual List<DistrictResponseModal> OfficerDistricts { get; set; }
    }
}
