using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer
{
    public class ViewOTCCardResponse : CommonResponseBase
    {
        public ViewOTCCardResponse()
        {
            Data = new List<ViewOTCCardResponseData>();
        }
        public List<ViewOTCCardResponseData> Data { get; set; }
    }
    public class ViewOTCCardResponseData
    {
        public string ZonalOfficeName { get; set; }

        public string RegionalOfficeName { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CardNo { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedDate { get; set; }
    }

    
}