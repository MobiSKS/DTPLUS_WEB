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
        public string CustomerId { get; set; }
        public string MobileNo { get; set; }
        public string FormNumber { get; set; }
        public string NameonCard { get; set; }
        public string CustomerName { get; set; }
        public string CommunicationStateId { get; set; }
        public string CommunicationCityName { get; set; }

        public string CardType { get; set; }
        public string VehicleNo { get; set; }
        public string CardNumber { get; set; }
        public string IssueDate { get; set; }
        public string SearchType { get; set; }
        public List<ConfigureSMSAlert> TypePCConfigureSMSAlerts { get; set; }
    }
    public class ConfigureSMSAlert
    {
        public string CustomerID { get; set; }
        public  string TransactionID { get; set; }
        public string statusId { get; set; }
    }
}
