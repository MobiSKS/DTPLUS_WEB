using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Aggregator
{
    public class ManageAggregatorRequestModel:BaseEntity
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
