using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class ConfigureEmailAlertRequest:BaseEntity
    {
        public string CustomerId { get; set; }
        public ConfigureEmailAlertRequest()
        {
            objConfigureEmailAlert = new List<ObjConfigureEmailAlert>();
        }
        public List<ObjConfigureEmailAlert> objConfigureEmailAlert { get; set; }
    }
    public class ObjConfigureEmailAlert
    {
        public int ServiceId { get; set; }
        public int AllowedStatus { get; set; }
    }
}
