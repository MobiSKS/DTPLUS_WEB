using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.ParentCustomer
{
    public class ParentCustomerSearchRequestModel:BaseEntity
    {
        public string ControlCardNo { get; set; }   
        public string CustomerID { get; set; }
        public List<ConfigureSMSAlert> TypePCConfigureSMSAlerts { get; set; }
    }
    public class ConfigureSMSAlert
    {
        public string CustomerID { get; set; }
        public  string TransactionID { get; set; }
        public string statusId { get; set; }
    }
}
