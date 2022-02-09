using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models
{
    public class BaseEntity
    {
        public string UserId { get; set; }
        public string UserAgent { get; set; }
        public string UserIp { get; set; }
    }
}
