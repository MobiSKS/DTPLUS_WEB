using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.TatkalCardCustomer
{
    public class ViewTatkalCardsResponseModel:CommonResponseBase
    {
        public ViewTatkalCardsResponseModel()
        {
            Data = new List<ViewTatkalCardDetails>();
        }
        public List<ViewTatkalCardDetails> Data { get; set; }   
    }
    public class ViewTatkalCardDetails
    {
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }

        public string CardNo { get; set; }
        public string RequestedBy { get; set; }

        public string CardProcessDate { get; set; }
        public string MappingStatus { get; set; }
 
    }
}
