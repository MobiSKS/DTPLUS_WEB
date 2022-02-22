using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HPCL_Web.Models.ViewCard
{
    public class ViewCardDetails : BaseEntity
    {
        [Required(ErrorMessage = "CustomerId is Required")]
        public string Customerid { get; set; }

        public string CardNo { get; set; }
        public string MobileNo { get; set; }
        public string VechileNo { get; set; }
        public string FastagNo { get; set; }
        public int Statusflag { get; set; }

        public string ModifiedBy { get; set; }



    }
    public class ViewCardSearchResult
    {

        public string SrNumber { get; set; }
        public string UserName { get; set; }
        public string CardNumber { get; set; }
        public string VechileNo { get; set; }

        [StringLength(10)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
        public string MobileNumber { get; set; }
        public string DailySaleLimit { get; set; }
        public string DailySaleBal { get; set; }

        public string MonthlySaleLimit { get; set; }
        public string MonthlySaleBal { get; set; }
        public string CCMSLimit { get; set; }
        public string LimitType { get; set; }
        public string AvailableCCMSLimit { get; set; }
        public string Reason { get; set; }
        public string MappingDate { get; set; }

        public string FastagNumber { get; set; }

    }

}
