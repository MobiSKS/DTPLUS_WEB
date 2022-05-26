using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class UpdateCCMSBalAlertConfigurationRequest: BaseEntity
    {
        public string CustomerID { get; set; }
        public string Amount { get; set; }
        public string ActionType { get; set; }
    }
}
