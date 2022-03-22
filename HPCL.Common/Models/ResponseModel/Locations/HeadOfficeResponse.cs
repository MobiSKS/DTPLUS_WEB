using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.Locations
{
    public class HeadOfficeDetailsResponse : ResponseMsg
    {
        public HeadOfficeDetailsResponse()
        {
            data = new List<HODResData>();
        }

       public virtual List<HODResData> data { get; set; }
    }

    public class HODResData
    {
        public int HQID { get; set; }
        public string HQCode { get; set; }
        public string HQName { get; set; }
        public string HQShortName { get; set; }
    }
}
