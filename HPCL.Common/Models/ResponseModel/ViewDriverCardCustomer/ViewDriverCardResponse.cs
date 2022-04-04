using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.DriverCardCustomer
{
    public class ViewDriverCardResponse : CommonResponseBase
    {
        public ViewDriverCardResponse()
        {
            Data = new List<ViewDriverCardResponseDetails>();
        }
        public List<ViewDriverCardResponseDetails> Data { get; set; }
    }
    public class ViewDriverCardResponseDetails
    {

        public string ZonalOfficeName { get; set; }

        public string RegionalOfficeName { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CardNo { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedDate { get; set; }
        public string AssignStatus { get; set; }
    }
}