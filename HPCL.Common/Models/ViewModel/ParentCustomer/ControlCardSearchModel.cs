using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.ParentCustomer
{
    public  class ControlCardSearchModel:CommonResponseBase
    {
        public ControlCardSearchModel()
        {
            Data = new List<ControlCardDetails>();
        }
        public string CustomerID { get; set; }
        public List<ControlCardDetails> Data { get; set; }
    }
    public class ControlCardDetails
    {
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ControlCardNo { get; set; }

    }
}
