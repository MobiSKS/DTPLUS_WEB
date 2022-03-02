using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.TerminalManagementResponse
{
    public class TerminalInstallationRequestResponse
    {
        public ObjMerchantDetail[] ObjMerchantDetail { get;set;}
        public ObjTerminalDetail[] ObjTerminalDetail { get; set; }
    }

    public class ObjMerchantDetail
    {

            public string RetailOutletName { get; set; }
             public int ZonalOfficeId { get; set; }
            public string ZonalOfficeName { get; set; }
            public int RegionalOfficeId { get; set; }
            public string RegionalOfficeName { get; set; }
            public int RetailOutletStateId { get; set; }
            public string SalesAreaName { get; set; }
            public string RetailOutletAddress { get; set; }
            public string RetailOutletCity { get; set; }
           
            public string StateName { get; set; }
            public int RetailOutletDistrictId { get; set; }
            public string DistrictName { get; set; }
            public string TerminalIssuanceType { get; set; }
            public int SalesAreaId { get; set; }




    }
    public class ObjTerminalDetail
    {
        public  string SrNumber { get; set; }

        public string TerminalId { get; set; }

        public double LastMonthSpends { get; set; }

        public string Status { get; set; }

    }
}
