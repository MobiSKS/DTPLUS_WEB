using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models.Common
{
    public class ApiResponseModel
    {
        public string ReferenceId { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
