using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.DICV
{
    public class SearchDealerRequest : BaseEntity
    {
        public string DealerCode { get; set; }
        public string DTPDealerCode { get; set; }
        public string OfficerType { get; set; }
    }
}