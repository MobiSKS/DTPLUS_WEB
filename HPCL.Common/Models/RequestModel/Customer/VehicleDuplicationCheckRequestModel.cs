using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class VehicleDuplicationCheckRequestModel: BaseEntity
    {
        public string VechileNo { get; set; }
        public string VinNumber { get; set; }
    }
}
