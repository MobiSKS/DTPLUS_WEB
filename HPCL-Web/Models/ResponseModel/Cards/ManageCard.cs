namespace HPCL_Web.Models.ResponseModel.Cards
{

    public class SearchCardResult
    {
        public int SrNumber { get; set; }
        public string CardNumber { get; set; }
        public string CustomerID { get; set; }
        public string UserID { get; set; }
        public string RequestDate { get; set; }
        public int OwnedorAttachedId { get; set; }
        public string OwnedorAttached { get; set; }
        public string VechileNo { get; set; }
        public string VehicleType { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Status { get; set; }
        public int YearOfRegistration { get; set; }
        public string Manufacturer { get; set; }
        public string MobileNumber { get; set; }
        public string VINNumber { get; set; }
        public string VehicleMake { get; set; }
        public string OwnershipType { get; set; }
        public string FormNumber { get; set; }
        public string CardCategory { get; set; }
        public string CardIssueType { get; set; }
        public string CardIdentifier { get; set; }
    }

    public class LimitResponse
    {
        public int CardBalance { get; set; }
        public string CardStatus { get; set; }
        public int SaleTranscationLimit { get; set; }
        public int DailySaleLimit { get; set; }
        public int DailyCreditLimit { get; set; }
        public int CashPurseLimit { get; set; }
        public int MonthlySaleLimit { get; set; }
        public int MonthlySaleBalance { get; set; }
        public int CCMSReloadSale { get; set; }
        public string CCMSReloadSaleLimit { get; set; }
        public int CCMSReloadSaleLimitValue { get; set; }
        public string ExpiryDate { get; set; }
        public string AllowCreditTranscation { get; set; }
        public int LimitTypeId { get; set; }
    }

    public class ServicesResponse
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public int SelectedServices { get; set; }
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
    }
}
