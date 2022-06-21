using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.COMCOManager
{
    public class GetCOMCOMapCustomerDetailsRequest: BaseEntity
    {
        public string MerchantID { get; set; }
        public string CustomerID { get; set; }
    }
}
