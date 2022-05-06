using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.TMS
{
    public class GetTMSUrlResponse : CommonResponseBase
    {
        public GetTMSUrlResponse()
        {
            Data = new List<GetTMSUrlResponseData>();
        }
        public List<GetTMSUrlResponseData> Data { get; set; }
    }

    public class GetTMSUrlResponseData
    {
        public string access_token { get; set; }
        public string message { get; set; }
        public string refresh_token { get; set; }
        public string url { get; set; }
        public string tmsUserId { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}