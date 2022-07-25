using HPCL.Common.Models.ResponseModel.CommonResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.MerchantDashboard
{
    public class LastSaleReloadEarningDetailsResponseModel : ResponseMsg
    {
        public string Date { get; set; }

        public string Reload_Rs { get; set; }

        public string Sale_Rs { get; set; }

        public string Earning_Rs { get; set; }

        public string Status { get; set; }

        public string Reason { get; set; }



    }
}
