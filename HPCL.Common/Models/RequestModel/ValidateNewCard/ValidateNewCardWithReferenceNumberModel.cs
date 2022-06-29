using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.ValidateNewCard
{
    public class ValidateNewCardWithReferenceNumberModel : BaseEntity
    {
        public string CustomerReferenceNo { get; set; }
        public string FormNumber { get; set; }
    }
}
