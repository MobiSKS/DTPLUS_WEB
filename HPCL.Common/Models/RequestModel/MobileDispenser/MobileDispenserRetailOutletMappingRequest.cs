using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.MobileDispenser
{
    public  class MobileDispenserRetailOutletMappingRequest:BaseEntity
    {
       
        public string MobileDispenserId { get; set; }

      
        public int Status { get; set; }
    }
}
