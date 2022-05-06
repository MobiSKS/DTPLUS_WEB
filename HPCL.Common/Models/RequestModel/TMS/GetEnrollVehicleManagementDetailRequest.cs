using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.TMS
{
    public class GetEnrollVehicleManagementDetailRequest: BaseEntity
    {
        public string CustomerID { get; set; }
        public string VehicleNo { get; set; }
        public string CardNo { get; set; }
        public int EnrollmentStatus { get; set; }
    }
}
