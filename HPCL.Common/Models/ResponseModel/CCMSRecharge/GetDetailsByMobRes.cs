using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.CCMSRecharge
{
    public class GetDetailsByMobRes : ResponseMsg
    {
        public List<GetDetailsByMobResData> Data { get; set; }
    }

    public class GetDetailsByMobResData
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerType { get; set; }
        public string ControlCardNo { get; set; }
    }
}
