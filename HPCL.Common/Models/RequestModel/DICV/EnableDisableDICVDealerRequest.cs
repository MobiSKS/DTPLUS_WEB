using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.DICV
{
    public class EnableDisableDICVDealerRequest : BaseEntity
    {
        public string DealerCode { get; set; }
        public string OfficerType { get; set; }
        public bool IsDisable { get; set; }
    }
}