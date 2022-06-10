using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class ApproveCustomerContactPersonRequest : BaseEntity
    {
        public String ActionType { get; set; }
        public List<AddressRequestDetails> TypeApproveCustomerContactPersonDetails { get; set; }
        public ApproveCustomerContactPersonRequest()
        {
            TypeApproveCustomerContactPersonDetails = new List<AddressRequestDetails>();
        }
    }

}