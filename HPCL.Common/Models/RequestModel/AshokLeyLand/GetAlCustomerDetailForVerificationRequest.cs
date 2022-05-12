using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.AshokLeyLand
{
    public class GetAlCustomerDetailForVerificationRequest : BaseEntity
    {
        public int StateID { get; set; }
        public string FormNumber { get; set; }
        public string CustomerName { get; set; }
        public int Status { get; set; }
    }
}