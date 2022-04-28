using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.TerminalManagementResponse
{
    public class TerminalInstallationRequestResponse : ResponseMsg
    {
       public Data Data { get; set; }
    }

    public class Data
    {
        public List<ObjMerchantDetail> ObjMerchantDetail { get; set; }
        public List<ObjTerminalDetail> ObjTerminalDetail { get; set; }
        public List<ObjStatusDetail> ObjStatusDetail { get; set; }
    }

    public class ObjMerchantDetail
    {
        public string RetailOutletName { get; set; }
        public int ZonalOfficeId { get; set; }
        public string ZonalOfficeName { get; set; }
        public int RegionalOfficeId { get; set; }
        public string RegionalOfficeName { get; set; }
        public int SalesAreaId { get; set; }
        public string SalesAreaName { get; set; }
        public string RetailOutletAddress { get; set; }
        public string RetailOutletCity { get; set; }
        public string RetailOutletStateId { get; set; }
        public string StateName { get; set; }
        public string RetailOutletDistrictId { get; set; }
        public string DistrictName { get; set; }
        public string TerminalIssuanceType { get; set; }
    }

    public class ObjTerminalDetail
    {
        public string SrNumber { get; set; }
        public string TerminalId { get; set; }
        public double LastMonthSpends { get; set; }
        public string Status { get; set; }
    }
    public class ObjStatusDetail
    {
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
