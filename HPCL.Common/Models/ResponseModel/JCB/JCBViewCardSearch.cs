using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.JCB
{
    public class JCBViewCardSearch : CommonResponseBase
    {
        public virtual List<JCBViewCardSearchResult> Data { get; set; } = new List<JCBViewCardSearchResult>();
    }
    public class JCBViewCardSearchResult
    {
        public string SrNumber { get; set; }
        public string UserName { get; set; }
        public string CardNumber { get; set; }
        public string VehicleNo { get; set; }
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
        public string VechileNo { get; set; }
        public string FastagNo { get; set; }
        public string IndividualOrgName { get; set; }
        public string RegionalOfficeName { get; set; }
        public string SaleTransLimt { get; set; }
    }

}
