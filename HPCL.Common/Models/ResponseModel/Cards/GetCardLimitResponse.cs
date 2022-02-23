namespace HPCL.Common.Models.ResponseModel.Cards
{
    public class GetCardLimitResponse
    {
        public int SrNumber { get; set; }
        public string CardNumber { get; set; }
        public string VechileNo { get; set; }
        public string VehicleType { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Status { get; set; }
        public int YearOfRegistration { get; set; }
        public string MobileNumber { get; set; }
        public string VehicleMake { get; set; }
        public int CashPurseLimit { get; set; }
        public int SaleTxnLimit { get; set; }
        public int DailySaleLimit { get; set; }
        public int MonthlySaleLimit { get; set; }
    }
}
