using HPCL.Common.Models.ResponseModel.CommonResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.CustomerDashboard
{
    public class ReminderResponseModel : ResponseMsg
    {
        public string ExpiringDrivestars { get; set; }
        public string ExpiringCards { get; set; }
    }
}
