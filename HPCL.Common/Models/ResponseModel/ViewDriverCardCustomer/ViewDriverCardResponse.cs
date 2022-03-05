using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.DriverCardCustomer
{
   public class ViewDriverCardResponse
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
