using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.JCB
{
    public class JCBCustomerCards : BaseEntity
    {
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidCustomerId, ErrorMessage = FieldValidation.ValidCustomerIdErrMsg)]
        //[Required(ErrorMessage = FieldValidation.CustomerNotEmpty)]
        public string CustomerId { get; set; }
        [StringLength(16)]
        [RegularExpression(FieldValidation.ValidCardNo, ErrorMessage = FieldValidation.ValidCardNoErrMsg)]
        public string CardNo { get; set; }
        [StringLength(10)]
        [RegularExpression(FieldValidation.ValidMobileNumber, ErrorMessage = FieldValidation.ValidMobileNumberErrMsg)]
        public string MobileNo { get; set; }
        [RegularExpression(FieldValidation.ValidVachileNumber, ErrorMessage = FieldValidation.ValidVachileNumberErrMsg)]
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
    }
}