using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.VolvoEicher
{
    public class GetSalesExecutiveEmployeeIDResponse : BaseEntity
    {
        public string SalesExecutiveEmployeeID { get; set; }
        public int Status { get; set; }
        public int StatusCode { get; set; }
        public string Reason { get; set; }
    }
}