using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.MyHpOTCCardCustomer
{
    public class EmailDuplicationCheckRequestModel: BaseEntity
    {
        public string CommunicationEmailid { get; set; }
    }
}
