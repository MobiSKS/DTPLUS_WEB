using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.ParentCustomer
{
    public class ParentChildCustomerMappingRequest:BaseEntity
    {
        public ParentChildCustomerMappingRequest()
        {
            ObjChildDtl = new List<ParentChildCustomerDetails>();
            ObjParentCustomerDtl= new List<ParentChildCustomerDetails>();
        }
        public string ParentCustomerId { get; set; }
        public List<ParentChildCustomerDetails> ObjParentCustomerDtl { get; set; }
        public List<ParentChildCustomerDetails> ObjChildDtl { get; set; }
        


        public string CustomerId { get; set; }
    }
    public class ParentChildCustomerDetails
    {
        public string ChildCustomerId { get; set; }
    }
}
