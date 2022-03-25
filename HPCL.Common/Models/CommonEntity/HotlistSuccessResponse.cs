using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.CommonEntity
{
    public class HotlistSuccessResponse
    {
        public int Status { get; set; }
        public string Reason { get; set; }
        public string ActionName { get; set; }
        public string EntityTypeValue { get; set; }
    }
}
