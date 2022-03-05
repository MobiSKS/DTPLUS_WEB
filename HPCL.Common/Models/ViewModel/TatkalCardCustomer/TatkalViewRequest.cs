using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel.TatkalCardCustomer
{
   public  class TatkalViewRequest : BaseEntity
    {

        public string RegionalOfficeId { get; set; }
        public int StatusFlag { get; set; }
    }
}
