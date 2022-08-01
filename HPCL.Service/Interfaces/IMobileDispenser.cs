using HPCL.Common.Models.ResponseModel.MobileDispenser;
using HPCL.Common.Models.ViewModel.MobileDispenser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public  interface IMobileDispenser
    {
       public  Task<List<MobileDispenserRetailOutletMappingResponse>>MobileDispenserRetailOutletMapping(string MobileDispenserId, int Status);

       public Task<List<MobileDispenserRetailOutletMappingResponse>> SearchMobileDispenserRetailOutletMapping(string MobileDispenserId, int Status);


        public Task<List<GetStatusMobileDispenserRetailOutletMappingResponse>> GetStatusMobileDispenserRetailOutletMapping(string Status);

        public Task<List<GetStatusMobileDispenserResponse>> GetStatusMobileDispenser();
        // public Task<List<MobileDispenserViewModel>> SearchMobileDispenserRetailOutletMapping(string MobileDispenserId, int Status);




    }
}
