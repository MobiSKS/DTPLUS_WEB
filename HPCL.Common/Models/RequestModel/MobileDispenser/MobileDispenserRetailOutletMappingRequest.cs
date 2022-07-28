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
        //public MobileDispenserRetailOutletMappingRequest()
        //{
        //    TerminalStatusResponseModals = new List<TerminalStatusResponseModal>();
        //    TerminalStatusResponseModals.Add(new TerminalStatusResponseModal
        //    {
        //        StatusId = 0,
        //        StatusName = "-ALL-"
        //    });
        //}
        public string MobileDispenserId { get; set; }

      
        public int Status { get; set; }

        //public virtual List<TerminalStatusResponseModal> TerminalStatusResponseModals { get; set; }
    }
}
