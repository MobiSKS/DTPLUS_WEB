using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel.Terminal
{
    public class ManageTerminalModel : BaseEntity
    {

        public ManageTerminalModel(){

            StatusModals = new List<StatusModal>();
            StatusModals.Add(new StatusModal
            {
                StatusId = 0,
                StatusName = "--All--"
            });
        }


        public string MerchantId { get; set; }
        public string TerminalId { get; set; }
        public string DeploymentStatus { get; set; }


        public virtual List<StatusModal> StatusModals { get; set; }
    }


    public class ManageTerminalRequest :BaseEntity
    {
        public string MerchantId { get; set; }
        public string TerminalId { get; set; }

        public string DeploymentStatus { get; set; }

    }
}
