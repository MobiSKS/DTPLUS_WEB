using HPCL.Common.Models.ResponseModel.MobileDispenser;
using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.MobileDispenser
{
    public  class MobileDispenserViewModel 
    {

        public int SerialNum { get; set; }
        public string MobileDispenserId { get; set; }


        public string MappedMerchantId { get; set; }


        public string CreatedBy { get; set; }


        public string CreatedTime { get; set; }


        public int Status { get; set; }


        public string ModifiedBy { get; set; }


        public string ModifiedTime { get; set; }


        public string Remarks { get; set; }

    }
}
