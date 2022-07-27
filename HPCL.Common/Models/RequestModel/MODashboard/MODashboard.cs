using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.MODashboard
{
    public class MODashboard : BaseEntity
    {
        public string UserName { get; set; }

        public string UserType { get; set; }
    }
}

