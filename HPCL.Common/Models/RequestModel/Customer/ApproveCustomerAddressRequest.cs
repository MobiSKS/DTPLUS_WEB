using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class ApproveCustomerAddressRequest : BaseEntity
    {
        public String ActionType { get; set; }
        public List<AddressRequestDetails> TypeApprovalApproveCustomerAddressRequests { get; set; }
        public ApproveCustomerAddressRequest()
        {
            TypeApprovalApproveCustomerAddressRequests = new List<AddressRequestDetails>();
        }
    }
    public class AddressRequestDetails
    {
        public String CustomerID { get; set; }
        public String Comments { get; set; }
    }
}