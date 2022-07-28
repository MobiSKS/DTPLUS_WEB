using System.Collections.Generic;

namespace HPCL.Common.Models.CommonEntity.RequestEnities
{
    public class PostAuth : BaseEntity
    {
        public string CreditPouchType { get; set; }
        public ObjCustomerDetails[] ObjCustomerDetail { get; set; }
    }

    public class ObjCustomerDetails
    {
        public string CustomerID { get; set; }
    }
}
