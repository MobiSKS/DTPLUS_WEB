using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class CheckPancardbyDistrictIdResponse
    {
        public int Status { get; set; }
        public string Reason { get; set; }
        public int Internel_Status_Code { get; set; }
        public string CustomerName { get; set; }
        public string CustomerID { get; set; }
    }
}