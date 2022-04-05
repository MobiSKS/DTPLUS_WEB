using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel.Customer
{
    public class CustomerCardInsertInfo
    {
        public String CustomerReferenceNo { get; set; }
        public String CustomerName { get; set; }
        public String FormNumber { get; set; }
        public string NoOfCards { get; set; }
        public string RBEId { get; set; }
        public int FeePaymentsCollectFeeWaiver { get; set; }
        public string FeePaymentNo { get; set; }
        public string FeePaymentDate { get; set; }
        public string CardPreference { get; set; }

        public string RBEName { get; set; }

        public string Useragent { get; set; }
        public string Userip { get; set; }
        public string UserId { get; set; }

        public string Createdby { get; set; }
        public string VehicleNoVerifiedManually { get; set; }

        public List<CardDetails> ObjCardDetail { get; set; }
    }
}
