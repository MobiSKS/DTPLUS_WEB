using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.AshokLeyLand
{
    public class GetCardDetailsRequest: BaseEntity
    {
        public string CustomerId { get; set; }
        public string CardNo { get; set; }
        public string MobileNumber { get; set; }
        public string VehicleNumber { get; set; }
        public string StatusFlag { get; set; }
    }
}