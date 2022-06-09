using HPCL.Common.Models.ResponseModel.CommonResponse;
using System;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.Cards
{

    public class SearchDetailsByCardId : ResponseMsg
    {
        public CardResponse Data { get; set; }
    }

    public class CardResponse
    {
        public List<SearchCardResult> GetCardsDetailsModelOutput { get; set; }
        public List<LimitResponseData> GetCardLimtModel { get; set; }
        public List<ServicesResponse> CardServices { get; set; }
    }

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
        public string RBEId { get; set; }
        public string RbeName { get; set; }
        public string FeePaymentNo { get; set; }
        public string FeePaymentDate { get; set; }
        public int FeePaymentsCollectFeeWaiverId { get; set; }
        public string FeePaymentsCollectFeeWaiver { get; set; }
    }

    public class LimitResponseData
    {
        public Decimal CardBalance { get; set; }
        public string CardStatus { get; set; }
        public Decimal SaleTranscationLimit { get; set; }
        public Decimal DailySaleLimit { get; set; }
        public Decimal DailyCreditLimit { get; set; }
        public Decimal CashPurseLimit { get; set; }
        public Decimal MonthlySaleLimit { get; set; }
        public Decimal MonthlySaleBalance { get; set; }
        public Decimal CCMSReloadSale { get; set; }
        public string CCMSReloadSaleLimit { get; set; }
        public Decimal CCMSReloadSaleLimitValue { get; set; }
        public string ExpiryDate { get; set; }
        public string AllowCreditTranscation { get; set; }
        public int LimitTypeId { get; set; }
    }

    public class ServicesResponse
    {
        public string ServiceID { get; set; }
        public string ServiceName { get; set; }
        public int SelectedServices { get; set; }
    }
}
