using HPCL.Common.Models.CommonEntity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.COMCOManager
{
    public class RequestToSetCreditLimitModel : BaseEntity
    {
        public RequestToSetCreditLimitModel()
        {
            COMCOLimitModeMdl = new List<StatusModal>();
            COMCOLimitModeMdl.Add(new StatusModal
            {
                StatusId = 0,
                StatusName = "Select"
            });
            COMCOLimitIntervalMdl = new List<StatusModal>();
            COMCOLimitIntervalMdl.Add(new StatusModal
            {
                StatusId = 0,
                StatusName = "--Select--"
            });
            lstChequeDetails = new List<ChequeDetails>();
        }

        public virtual List<StatusModal> COMCOLimitModeMdl { get; set; }
        public virtual List<StatusModal> COMCOLimitIntervalMdl { get; set; }

        [Required(ErrorMessage = "Customer ID is required")]
        public string CustomerID { get; set; }

        [Required(ErrorMessage = "Select Mode is Required")]
        public int LimitSetMode { get; set; }
        public int InvoiceIntervalId { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        public string Amount { get; set; }

        //[Required(ErrorMessage = "Caution Amount is required")]
        public string CautionAmountWOSD { get; set; }
        public string CautionAmountWSD { get; set; }

        [Required(ErrorMessage = "COMOCO Package ID is required")]
        public string COMOCOPackageID { get; set; }

        //[Required(ErrorMessage = "Finance Charges is required")]
        public string FinanceCharges { get; set; }
        public string Remarks { get; set; }
        public string NoOfCheques { get; set; }
        public IFormFile ScannedReferenceDocumentWOSD { get; set; }
        public IFormFile ScannedReferenceDocumentWSD { get; set; }
        public string ServicesCharges { get; set; }
        public int Internel_Status_Code { get; set; }
        public List<ChequeDetails> lstChequeDetails { get; set; }
    }
    public class ChequeDetails
    {
        public string ChequeBDSCRNumber { get; set; }
        public string ChequeBDSCRDate { get; set; }
    }
}