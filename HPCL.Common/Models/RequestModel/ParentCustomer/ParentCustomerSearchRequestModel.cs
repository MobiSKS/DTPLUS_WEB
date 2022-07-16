using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.ParentCustomer
{
    public class ParentCustomerSearchRequestModel:BaseEntity
    {
        public string ControlCardNo { get; set; }   
        public string CustomerID { get; set; }
    }
}
