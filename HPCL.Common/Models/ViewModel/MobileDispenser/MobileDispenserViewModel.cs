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
        public MobileDispenserViewModel()
        {
            GetStatus = new List<GetStatusMobileDispenserResponse>();
            GetStatus.Add(new GetStatusMobileDispenserResponse
            {   StatusId="0",
                StatusName = "--ALL--"
            });
        }

        public int StatusIdValue { get; set; }
        public virtual List<MobileDispenserRetailOutletMappingResponse> GetAllDataMobileDispenser { get; set; }

        public virtual List<GetStatusMobileDispenserResponse> GetStatus { get; set; }










        //public int SerialNum { get; set; }
        //public string MobileDispenserId { get; set; }


        //public string MappedMerchantId { get; set; }


        //public string CreatedBy { get; set; }


        //public string CreatedTime { get; set; }


        //public int Status { get; set; }


        //public string ModifiedBy { get; set; }


        //public string ModifiedTime { get; set; }


        //public string Remarks { get; set; }

    }
}
