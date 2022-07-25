using System;
using HPCL.Common.Models.CommonEntity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Models.ResponseModel.CommonResponse;

namespace HPCL.Common.Models.ResponseModel.MerchantDashboard
{

    public class KeyInformationResponseModel : ResponseMsg
    {
        public string TerminalId { get; set; }
        public string DateOfInstallation { get; set; }

        public string Status { get; set; }
        public string Reason { get; set; }

    }

}
