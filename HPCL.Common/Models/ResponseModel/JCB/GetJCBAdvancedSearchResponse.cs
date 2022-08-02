using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.JCB
{
    public class GetJCBAdvancedSearchResponse : CommonResponseBase
    {
        public GetJCBAdvancedSearchResponse()
        {
            Data = new List<GetJCBAdvancedSearchData>();
        }
        public List<GetJCBAdvancedSearchData> Data { get; set; }
    }
    public class GetJCBAdvancedSearchData
    {
        public string IndividualOrgName { get; set; }
        public string FormNumber { get; set; }
        public string CustomerID { get; set; }
        public string CustomerTypeName { get; set; }
        public string RegionalOfficeName { get; set; }
        public string ZonalOfficeName { get; set; }
        public string CreatedBy { get; set; }
        public string StatusName { get; set; }
        public string NameOnCard { get; set; }
        public string ResidenceType { get; set; }
        public string Constitution { get; set; }
        public string ITPermanentAccount { get; set; }
        public string BankName { get; set; }
        public string BankBranchName { get; set; }
        public string BankAccountNumber { get; set; }
        public string ControlCardNo { get; set; }
        public string CommunicationCityName { get; set; }
        public string StateName { get; set; }
        public string CommunicationPhoneNo { get; set; }
        public string CommunicationPincode { get; set; }
        public string CommunicationMobileNo { get; set; }
        public string CommunicationEmailid { get; set; }
        public string District { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}