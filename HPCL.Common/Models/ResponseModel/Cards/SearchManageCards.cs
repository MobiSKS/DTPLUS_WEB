using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.Cards
{
    public class SearchManageCards : ResponseMsg
    {
        public List<SearchGridResponse> data { get; set; }
    }

    public class SearchGridResponse
    {
        public int SrNumber { get; set; }
        public string CardNumber { get; set; }
        public string CustomerID { get; set; }
        public string UserID { get; set; }
        public string RequestDate { get; set; }
        public string OwnedorAttachedId { get; set; }
        public string OwnedorAttached { get; set; }
        public string VechileNo { get; set; }
        public string VehicleType { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Status { get; set; }
        public string YearOfRegistration { get; set; }
        public string Manufacturer { get; set; }
        public string MobileNumber { get; set; }
        public string VINNumber { get; set; }
        public string VehicleMake { get; set; }
        public string OwnershipType { get; set; }
        public string FormNumber { get; set; }
        public string CardCategory { get; set; }
        public string CardIssueType { get; set; }
        public string CardIdentifier { get; set; }
        public string LimitTypeName { get; set; }
        public string CCMSReloadSaleLimitValue { get; set; }
        public string CardPreference { get; set; }
        public string IndividualOrgName { get; set; }
        public string RegionalOfficeName { get; set; }
    }
}
