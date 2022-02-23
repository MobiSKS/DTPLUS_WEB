using System.Collections.Generic;

namespace HPCL_Web.Models.ResponseModel.Cards
{
    public class CardDetailsResponse
    {
        public List<GetCardLimtModel> getCardLimtModel { get; set; }
        public List<CardServices> cardServices { get; set; }
    }

    public class GetCardLimtModel
    {
        public string SaleTransactionLimit { get; set; }
        public string DailySaleLimit { get; set; }
        public string DailyCreditLimit { get; set; }
        public string CashPurseLimit { get; set; }
        public string MonthlySaleLimit { get; set; }
        public string MonthlySaleBalance { get; set; }
        public string ReloadSaleLimit { get; set; }
        public string EnableCreditTransaction { get; set; }
    }

    public class CardServices
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public int SelectedServices { get; set; }
    }
}
