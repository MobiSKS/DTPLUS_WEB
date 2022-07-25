using System;
using System.Collections.Generic;
using HPCL.Common.Models.CommonEntity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using HPCL.Common.Resources;

namespace HPCL.Common.Models.RequestModel.MerchantDashboard
{
    public class MerchantDashboardRequestModel:BaseEntity
    {
      
        public string MerchantId { get; set; }
      
    }
}
 