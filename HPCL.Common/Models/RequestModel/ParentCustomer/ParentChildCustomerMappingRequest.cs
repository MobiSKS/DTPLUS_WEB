﻿using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.ParentCustomer
{
    public class ParentChildCustomerMappingRequest:BaseEntity
    {
        public string ParentCustomerId { get; set; }
        public List<ParentChildCustomerDetails> ObjParentCustomerDtl { get; set; }

    }
    public class ParentChildCustomerDetails
    {
        public string Id { get; set; }
        public string ChildCustomerId { get; set; }
    }
}
