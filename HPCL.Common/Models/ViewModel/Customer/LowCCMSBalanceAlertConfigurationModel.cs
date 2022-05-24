using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Customer
{
    public class LowCCMSBalanceAlertConfigurationModel: BaseEntity
    {
        public string CustomerID { get; set; }
        public string Amount { get; set; }
        public string ActionType { get; set; }
        public string Remarks { get; set; }
        public string IndividualOrgName { get; set; }
        public string NameOnCard { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
