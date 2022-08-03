using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.CustomerRequest
{
    public class SearchHotlistAndReissueRes : ResponseMsg
    {
        public List<SearchHotlistAndReissueResData> data { get; set; }
    }

    public class SearchHotlistAndReissueResData
    {
        public string CardNo { get; set; }
        public string UserName { get; set; }
        public int OwnedORAttachedId { get; set; }
        public string OwnedORAttachedName { get; set; }
        public string VechileNo { get; set; }
        public string VechileType { get; set; }
        public string YearOfRegistration { get; set; }
        public string Manufacturer { get; set; }
        public string CardCategory { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
