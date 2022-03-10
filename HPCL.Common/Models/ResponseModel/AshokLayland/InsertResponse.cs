using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.AshokLayland
{
    public class InsertResponse : ResponseMsg
    {
        public List<ResponseData> data { get; set; }
    }

    public class ResponseData
    {
        public string ReferenceId { get; set; }
        public string Password { get; set; }
        public string DealerId { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
