using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.TatkalCardCustomer
{
   public  class TatkalCardCustomerViewResponse
    {

        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }

        public string Status { get; set; }
        public string Remark { get; set; }

        public string CardNo { get; set; }
        public string CreatedBy { get; set; }
        public string RequestedBy { get; set; }
        public string CardProcessDate { get; set; }
        public string Mapped { get; set; }

    }
}
