using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.MobileDispenser
{
    public  class GetStatusMobileDispenserRetailOutletMappingRequest: BaseEntity
    {
        public string Status { get; set; }
    }
}
