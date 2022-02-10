using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCL_Web.Models.Cards.ManageCards
{
    public class CustomerCards : BaseEntity
    {
        public CustomerCards()
        {
            StatusModals = new List<StatusModal>();
            LimitTypeModals = new List<LimitTypeModal>();
        }

        [Required(ErrorMessage = "CustomerId is Required")]
        public string CustomerId { get; set; }
        public string CardNo { get; set; }
        public string MobileNumber { get; set; }
        public string VehicleNumber { get; set; }
        public int StatusFlag { get; set; }

        public string CardCategory { get; set; }
        public string CardIssueType { get; set; }
        public DateTime RequestDate { get; set; }
        public string FormNumber { get; set; }
        public string RbeId { get; set; }
        public string RbeName { get; set; }
        public string vehicleType { get; set; }
        public string VehicleMake { get; set; }
        public string YearOfReg { get; set; }
        public string OwnerType { get; set; }
        public string VinNumber { get; set; }


        //public string CardBalance { get; set; }
        //public string CardStatus { get; set; }
        //public int OneTimeTransactionLimit { get; set; }
        //public int OneTimeTransactionRemaining { get; set; }
        //public int DailyTransactionLimit { get; set; }
        //public int MonthlyTransactionLimit { get; set; }
        //public int YearlyTransactionLimit { get; set; }
        //public int OneTimeCCMSTransactionLimit { get; set; }
        //public int OneTimeCCMSTransactionRemaining { get; set; }
        //public int DailyCCMSTransactionLimit { get; set; }
        //public int MonthlyCCMSTransactionLimit { get; set; }
        //public int YearlyCCMSTransactionLimit { get; set; }
        //public int DailyTransactionRemaining { get; set; }
        //public int MonthlyTransactionRemaining { get; set; }
        //public int YearlyTransactionRemaining { get; set; }
        //public int DailyCCMSTransactionRemaining { get; set; }
        //public int MonthlyCCMSTransactionRemaining { get; set; }
        //public int YearlyCCMSTransactionRemaining { get; set; }

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
        //public int LimitTypeId { get; set; }


        public virtual List<StatusModal> StatusModals { get; set; }
        public virtual List<LimitTypeModal> LimitTypeModals { get; set; }
    }

    public class StatusModal
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }

    public class LimitTypeModal
    {
        public int LimitId { get; set; }
        public string Description { get; set; }
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

    //public class LimitSearchResponse
    //{
    //    public string CustomerID { get; set; }
    //    public string CardBalance { get; set; }
    //    public string CardStatus { get; set; }
    //    public int OneTimeTransactionLimit { get; set; }
    //    public int DailyTransactionLimit { get; set; }
    //    public int MonthlyTransactionLimit { get; set; }
    //    public int YearlyTransactionLimit { get; set; }
    //    public int OneTimeCCMSTransactionLimit { get; set; }
    //    public int DailyCCMSTransactionLimit { get; set; }
    //    public int MonthlyCCMSTransactionLimit { get; set; }
    //    public int YearlyCCMSTransactionLimit { get; set; }
    //}

    //public class CardReminingLimt
    //{
    //    public int RemCardDaily { get; set; }
    //    public int RemCardMonthly { get; set; }
    //    public int RemCardYearly { get; set; }
    //}

    //public class CardReminingCCMSLimt
    //{
    //    public int RemCCMSDaily { get; set; }
    //    public int RemCCMSMonthly { get; set; }
    //    public int RemCCMSYearly { get; set; }
    //}

    public class ServicesResponse
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public int SelectedServices { get; set; }
    }

    public class UpdateMobileResponse
    {
        public int Status { get; set; }
        public string Reason { get; set; }
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