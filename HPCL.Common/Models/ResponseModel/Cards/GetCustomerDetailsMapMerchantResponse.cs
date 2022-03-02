namespace HPCL.Common.Models.ResponseModel.Cards
{
    public class GetCustomerDetails
    {
        public string CustomerID { get; set; }
        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }
        public string CustomerName { get; set; }
    }

    public class GetCustomerCardDetails
    {
        public string CardNo { get; set; }
        public string CardIdentifier { get; set; }
        public string LastTransactionDate { get; set; }
    }

    public class MerchantMapResponse
    {
        public string MerchantId { get; set; }
        public string MerchantName { get; set; }
        public string RetailOutletName { get; set; }
        public string Address { get; set; }
    }

}
