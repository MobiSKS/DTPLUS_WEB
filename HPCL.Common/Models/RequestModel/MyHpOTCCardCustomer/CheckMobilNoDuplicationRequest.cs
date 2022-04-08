using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.MyHpOTCCardCustomer
{
    public class CheckMobilNoDuplicationRequest : BaseEntity
    {
        public string Mobileno { get; set; }
    }
}