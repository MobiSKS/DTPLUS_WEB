using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models.ViewCard
{
    public class ViewCardDetails
    {
        [Required(ErrorMessage = "CustomerId is Required")]
        public string CustomerId { get; set; }
        public string UserId { get; internal set; }
        public string UserAgent { get; set; }
        public string UserIp { get; set; }
    }
    public class ViewCardSearchResult
    {

        public string Sr { get; set; }


        public string UserName { get; set; }
        public string CardNumber { get; set; }
        public string VehicleNo { get; set; }
        public string MobileNumber { get; set; }
        public string DailySaleBalance { get; set; }
        public string MonthlySaleLimits { get; set; }
        public string MontlySaleBalance { get; set; }
        public string CCMSLimts { get; set; }
        public string TypeOfLimits { get; set; }
        public string AvailabeCCMSLimits { get; set; }

    }

}
