using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models
{
    public class OfficerModel
    {
        
        public OfficerModel()
        {
            OfficerTypeMdl = new List<OfficerTypeModel>();
            OfficerTypeMdl.Add(new OfficerTypeModel
            {
                OfficerTypeID = 0,
                OfficerTypeName = "Select Type",
                OfficerTypeShortName = "Select Type"
            });

            OfficerZoneMdl = new List<OfficerZoneModel>();
            OfficerRegionMdl = new List<OfficerRegionModel>();
            OfficerHqMdl = new List<OfficerHqModel>();
            OfficerStateMdl = new List<OfficerStateModel>();
            OfficerStateMdl.Add(new OfficerStateModel
            {
                CountryID = 0,
                StateID = 0,
                StateName = "Select State"
            });
            OfficerDistrictMdl = new List<OfficerDistrictModel>();
        }

        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Location is Required")]
        public int LocationID { get; set; }
        [Required(ErrorMessage = "Officer Type is Required")]
        public int OfficerTypeID { get; set; }
        [Required(ErrorMessage = "Address1 is Required")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        [Required(ErrorMessage = "State is Required")]
        public int State { get; set; }
        [Required(ErrorMessage = "District is Required")]
        public string District { get; set; }
        public int? Pin { get; set; }
        [Required(ErrorMessage = "Mobile is Required")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Numeric value only")]
        public int? Mobile { get; set; }
        public int? Phone { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Email id is not valid")]
        public string Email { get; set; }
        public string Fax { get; set; }
        public virtual List<OfficerTypeModel> OfficerTypeMdl { get; set; }
        public virtual List<OfficerZoneModel> OfficerZoneMdl { get; set; }
        public virtual List<OfficerRegionModel> OfficerRegionMdl { get; set; }
        public virtual List<OfficerHqModel> OfficerHqMdl { get; set; }
        public virtual List<OfficerStateModel> OfficerStateMdl { get; set; }
        public virtual List<OfficerDistrictModel> OfficerDistrictMdl { get; set; }
    }

    public class OfficerTypeModel
    {
        public int OfficerTypeID { get; set; }
        public string OfficerTypeName { get; set; }
        public string OfficerTypeShortName { get; set; }

    }
    public class OfficerZoneModel
    {
        public int HQID { get; set; }
        public int ZoneID { get; set; }
        public string ZoneName { get; set; }
    }
    public class OfficerRegionModel
    {
        public int RegionID { get; set; }
        public string RegionName { get; set; }
        public string RegionShortName { get; set; }
    }
    public class OfficerHqModel
    {
        public int HQID { get; set; }
        public string HQName { get; set; }
        public string HQShortName { get; set; }
    }
    public class OfficerStateModel
    {
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }
    }
    public class OfficerDistrictModel
    {
        public int stateID { get; set; }
        public int districtID { get; set; }
        public string districtName { get; set; }
    }
}
