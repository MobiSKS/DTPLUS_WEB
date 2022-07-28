using HPCL.Common.Models.ResponseModel.CommonResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.CustomerDashboard
{
    public class VerifyYourDetailsResponseModel : ResponseMsg
    {
        public string RegisteredEmailAddress { get; set; }
        public string RegisteredMobileNumber { get; set; }
    }
}
