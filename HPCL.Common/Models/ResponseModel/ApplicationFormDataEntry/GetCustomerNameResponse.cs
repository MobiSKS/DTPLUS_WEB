using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.ApplicationFormDataEntry
{
    public class GetCustomerNameResponse : ResponseMsg
    {
        public GetCustomerNameResponsedata data { get; set; }
    }

    public class GetCustomerNameResponsedata
    {
        public List<GetNameandFormNumberbyCustomerId> GetNameandFormNumberbyCustomerId { get; set; }
        public List<GetStatus> GetStatus { get; set; }
    }

    public class GetStatus
    {
        public string CustomerID { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }

    public class GetNameandFormNumberbyCustomerId
    {
        public string CustomerId { get; set; }
        public string CustomerReferenceNo { get; set; }
        public string CustomerName { get; set; }
    }
}
