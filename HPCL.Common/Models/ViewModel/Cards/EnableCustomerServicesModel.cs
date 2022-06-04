using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Cards
{
    public class EnableCustomerServicesModel : BaseEntity
    {
        public string CustomerId { get; set; }
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
        public string Remarks { get; set; }
    }
}