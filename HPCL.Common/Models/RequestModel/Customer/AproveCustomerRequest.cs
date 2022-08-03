using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class AproveCustomerRequest : BaseEntity
    {
        public String CustomerReferenceNo { get; set; }
        public String Comments { get; set; }
        public String Approvalstatus { get; set; }
        public String ApprovedBy { get; set; }
        public string FormNumber { get; set; }
    }

    public class AproveAggregatorCustomerRequest : BaseEntity
    {
        public String CustomerReferenceNo { get; set; }
        public String Comments { get; set; }
        public String Approvalstatus { get; set; }
        public String ApprovedBy { get; set; }
        public BigInteger FormNumber { get; set; }
    }
}
