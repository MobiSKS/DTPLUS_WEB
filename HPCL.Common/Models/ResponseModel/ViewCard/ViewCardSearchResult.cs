using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.ViewCard
{
    
        public class ViewCardSearchResult
        {

            public string SrNumber { get; set; }
            public string UserName { get; set; }
            public string CardNumber { get; set; }
            public string VechileNo { get; set; }
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

    }

    }
