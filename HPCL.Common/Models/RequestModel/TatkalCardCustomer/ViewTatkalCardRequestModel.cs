using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.TatkalCardCustomer
{
    public class ViewTatkalCardRequestModel:BaseEntity
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string StatusId { get; set; }
        public string ZonalOfficeID { get; set; }
        public string RegionalOfficeID { get; set; }
    }
}
