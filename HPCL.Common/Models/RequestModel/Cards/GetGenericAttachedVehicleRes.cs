using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.RequestModel.Cards
{
    public class GetGenericAttachedVehicleRes : ResponseMsg
    {
        public List<GetGenericAttachedVehicleResData> data { get; set; }
    }

    public class GetGenericAttachedVehicleResData
    {
        public string CardNumber { get; set; }  
        public string VechileNo { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleType { get; set; }
        public int YearOfRegistration { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
