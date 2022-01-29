using HPCL_Web.Models.Customer;
using System;
using System.Collections.Generic;

namespace HPCL_Web.Models
{
    public class CustomerModel : BaseEntity
    {
        public CustomerModel()
        {
            CustomerList = new List<CustomerTypeResponse>();  
        }

        public int CustomerType { get; set; }
        
        //public 
        public virtual List<CustomerTypeResponse> CustomerList { get; set; }

    }
}
