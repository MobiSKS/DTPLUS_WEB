using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.CustomerRequest
{
    public  class ConfigureEmailAlertViewModel:CommonResponseBase
    {
        public string CustomerID { get; set; }
        public ConfigureEmailAlertViewModel()
        {
            Data = new List<ConfigureEmailAlertDetails>();
        }
        public List<ConfigureEmailAlertDetails> Data { get; set; }
        public List<ObjConfigureEmailAlert> objConfigureEmailAlert { get; set; }
    }
    public class ConfigureEmailAlertDetails
    {
        public int ServieId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceStatus { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }

}
