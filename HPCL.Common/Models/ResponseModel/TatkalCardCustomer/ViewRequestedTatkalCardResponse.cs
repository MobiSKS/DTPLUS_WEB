using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.TatkalCardCustomer
{
    public class ViewRequestedTatkalCardResponse : CommonResponseBase
    {
        public ViewRequestedTatkalCardResponse()
        {
            Data = new List<ViewRequestedTatkalCardResponseData>();
        }
        public List<ViewRequestedTatkalCardResponseData> Data { get; set; }
    }
    public class ViewRequestedTatkalCardResponseData
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
