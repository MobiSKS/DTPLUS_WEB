using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.TMS
{
    public class EnrollVehicleViewModel: BaseEntity
    {
        public EnrollVehicleViewModel()
        {
            StatusList = new List<StatusResponseModal>();
            vehicleDetailsModel = new List<EnrollVehicleDetailsModel>();
        }

        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string VehicleNo { get; set; }
        public string CardNo { get; set; }
        public int EnrollmentStatus { get; set; }
        public string Reason { get; set; }
        public string Message { get; set; }
        public virtual List<StatusResponseModal> StatusList { get; set; }
        public virtual List<EnrollVehicleDetailsModel> vehicleDetailsModel { get; set; }
    }

    public class EnrollVehicleDetailsModel
    {
        public string CustomerID { get; set; }
        public string VehicleNo { get; set; }
        public string CardNo { get; set; }
        public string VehicleType { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
        public string CreatedBy { get; set; }
        public string TMSUserId { get; set; }
    }
}
