using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models
{
    public class OfficerListModel
    {
        public OfficerListModel()
        {
            OfficerTypeMdl = new List<OfficerTypeModel>();
            OfficerZoneMdl = new List<OfficerZoneModel>();
            OfficerRegionMdl = new List<OfficerRegionModel>();
            OfficerHqMdl = new List<OfficerHqModel>();
            OfficerTypeMdl.Add(new OfficerTypeModel
            {
                OfficerTypeID = 0,
                OfficerTypeName = "Select Type",
                OfficerTypeShortName = "Select Type"
            });
        }
        public int SerialNum { get; set; }
        public string OfficerTypeID { get; set; }
        public string OfficerTypeName { get; set; }
        public string OfficerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Pin { get; set; }
        public string EmailId { get; set; }
        public string Fax { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string ReferenceId { get; set; }
        public string LocationId { get; set; }
        public string Createdby { get; set; }
        public string StateId { get; set; }
        public string CityId { get; set; }
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string LocationName { get; set; }
        public virtual List<OfficerTypeModel> OfficerTypeMdl { get; set; }
        public virtual List<OfficerZoneModel> OfficerZoneMdl { get; set; }
        public virtual List<OfficerRegionModel> OfficerRegionMdl { get; set; }
        public virtual List<OfficerHqModel> OfficerHqMdl { get; set; }

    }
}
