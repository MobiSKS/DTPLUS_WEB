using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Customer
{
    public class CustomerResetPasswordViewModel:CommonResponseBase
    {
        public string CustomerId { get; set; }
        public string AlternateEmailId { get; set; }
        public List<GetCustomerResetPassword> Data { get; set; }
    }
    public class GetCustomerResetPassword
    {
        public string CommunicationEmailid { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
