using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.Cards
{
    public class SetSaleLimitUpdateRes : ResponseMsg
    {
        public List<SetSaleLimitUpdateResData> data { get; set; }
    }

    public class SetSaleLimitUpdateResData
    {
        public string Cardno { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
