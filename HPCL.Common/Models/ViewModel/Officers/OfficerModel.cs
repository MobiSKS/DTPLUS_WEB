using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Officers
{
    public class OfficerModel
    {

        public OfficerModel()
        {
            OfficerTypeMdl = new List<OfficerTypeResponseModal>();
            OfficerTypeMdl.Add(new OfficerTypeResponseModal
            {
                OfficerTypeID = 0,
                OfficerTypeName = "Select Type",
                OfficerTypeShortName = "Select Type"
            });

            OfficerZoneMdl = new List<ZonalOfficeResponseModal>();
            OfficerRegionMdl = new List<RegionalOfficeResponseModal>();
            OfficerHqMdl = new List<HqResponseModal>();
            OfficerStateMdl = new List<StateResponseModal>();
            OfficerStateMdl.Add(new StateResponseModal
            {
                CountryID = 0,
                StateID = 0,
                StateName = "Select State"
            });
            OfficerDistrictMdl = new List<DistrictResponseModal>();
        }

        //[Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //[Required]
        public string UserName { get; set; }
        //[Required(ErrorMessage = "Location is Required")]
        public string LocationID { get; set; }
        //[Required(ErrorMessage = "District is Required")]
        public string DistrictID { get; set; }
        //[Required(ErrorMessage = "Officer Type is Required")]
        public string OfficerTypeID { get; set; }
        public string OfficerTypeName { get; set; }
        //[Required(ErrorMessage = "Address1 is Required")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        //[Required(ErrorMessage = "State is Required")]
        public string State { get; set; }
        public string StateId { get; set; }
        //[RegularExpression(@"^[1-9][0-9]{5}$", ErrorMessage = "Should be 6 Digits Only")]
        public string Pin { get; set; }
        //[Required(ErrorMessage = "Mobile is Required")]
        //[RegularExpression(@"^(?!(0))[0-9]{10}$", ErrorMessage = "Should not start with 0 and should be 10 Digits Only")]
        public string Mobile { get; set; }
        public string MobileNo { get; set; }
        public string Phone { get; set; }
        //[Required(ErrorMessage = "Email is Required")]
        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string EmailId { get; set; }
        public string Fax { get; set; }
        public int OfficerID { get; set; }
        public virtual List<OfficerTypeResponseModal> OfficerTypeMdl { get; set; }
        public virtual List<ZonalOfficeResponseModal> OfficerZoneMdl { get; set; }
        public virtual List<RegionalOfficeResponseModal> OfficerRegionMdl { get; set; }
        public virtual List<HqResponseModal> OfficerHqMdl { get; set; }
        public virtual List<StateResponseModal> OfficerStateMdl { get; set; }
        public virtual List<DistrictResponseModal> OfficerDistrictMdl { get; set; }
    }

    public class OfficerTypeModel
    {
        public int OfficerTypeID { get; set; }
        public string OfficerTypeName { get; set; }
        public string OfficerTypeShortName { get; set; }

    }
    public class OfficerZoneModel
    {
        public int ZonalOfficeID { get; set; }
        public string ZonalOfficeName { get; set; }
    }
    public class OfficerRegionModel
    {
        public int RegionalOfficeID { get; set; }
        public string RegionalOfficeName { get; set; }
    }
    public class OfficerHqModel
    {
        public int HQID { get; set; }
        public string HQCode { get; set; }
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
