using HPCL.Common.Models.ResponseModel.CommonResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.CustomerDashboard
{
    public class KeyEventsResponseModel : ResponseMsg
    {
        public string LastLogin { get; set; }
        public string LastTransaction { get; set; }
        public string LastRedemption { get; set; }
        public string LastPasswordChange { get; set; }
        public string LastContactDetailsUpdate { get; set; }
    }
}
