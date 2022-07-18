using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.ParentCustomer
{
    public class GetAccountStatementType
    {
        public string TypeName { get; set; }
        public string TypeId { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
