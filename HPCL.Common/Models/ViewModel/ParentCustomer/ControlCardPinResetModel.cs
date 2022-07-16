using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.ParentCustomer
{
    public class ControlCardPinResetModel:CommonResponseBase
    {
        public string CustomerID { get; set; }
        public string CardNo { get; set; }
    }
}
