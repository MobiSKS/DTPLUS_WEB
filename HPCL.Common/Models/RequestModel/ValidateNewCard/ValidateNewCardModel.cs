using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.ValidateNewCard
{
    public class ValidateNewCardModel : BaseEntity
    {
        public string StateId { get; set; }
        public string FormNumber { get; set; }
        public string CustomerName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
