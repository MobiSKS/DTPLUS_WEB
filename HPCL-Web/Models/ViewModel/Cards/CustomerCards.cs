using HPCL.Common.Resources;
using HPCL_Web.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCL_Web.Models.ViewModel.Cards
{
    public class CustomerCards : BaseEntity
    {
        public CustomerCards()
        {
            StatusModals = new List<StatusModal>();
            LimitTypeModals = new List<LimitTypeModal>();
        }

        [Required(ErrorMessage = FieldValidation.CustomerNotEmpty)]
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
        public virtual List<StatusModal> StatusModals { get; set; }
        public virtual List<LimitTypeModal> LimitTypeModals { get; set; }
    }
}