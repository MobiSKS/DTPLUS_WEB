using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.MobileDispenser
{
    public  class GetStatusMobileDispenserRetailOutletMappingResponse
    {
        public int SerialNum { get; set; }
        public string MobileDispenserId { get; set; }


        public string MappedMerchantId { get; set; }


        public string CreatedBy { get; set; }


        public string CreatedTime { get; set; }


        public string Status { get; set; }


        public string ModifiedBy { get; set; }


        public string ModifiedTime { get; set; }


        public string Remarks { get; set; }

    }
}
