using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.CustomerManage
{
    public class AddOnCustomerModel : BaseEntity
    {
        public string CustomerId { get; set; }
        public string EmailId { get; set; }
        public string Remarks { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public int Status { get; set; }
        public string Success { get; set; }
    }
}