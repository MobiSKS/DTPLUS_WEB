using HPCL.Common.Models.ResponseModel.MobileDispenser;
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
    }
}
