using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.AshokLayland
{
    public class SearchAlResult : ResponseMsg
    {
        public List<ALList> data { get; set; }
    }

    public class ALList
    {
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public int ZonalOfficeID { get; set; }
        public string ZonalOfficeName{get;set;}
        public int RegionalOfficeID { get; set; }
        public string RegionalOfficeName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string MobileNo { get; set; }
        public string Pin { get; set; }
        public string EmailId { get; set; }
        public string ZOfficeID { get; set; }
        public string ROfficeID { get; set; }
        public string SId { get; set; }
        public string DId { get; set; }
        public string SBUId { get; set; }
    }
}
