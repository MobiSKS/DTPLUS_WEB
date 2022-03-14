using System;
using System.Collections.Generic;
using System.Text;
using HPCL.Common.Models.CommonEntity;

namespace HPCL.Common.Models.ViewModel.Customer
{
    public class CustomerCardInfo
    {
        public CustomerCardInfo()
        {
            VehicleTypeMdl = new List<VehicleTypeModel>();
            ObjCardDetail = new List<CardDetails>();
        }

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
        public int Status { get; set; }
        public int StatusCode { get; set; }
        public virtual List<VehicleTypeModel> VehicleTypeMdl { get; set; }
        public List<CardDetails> ObjCardDetail { get; set; }
        public string NoOfVehiclesAllCards { get; set; }
        public string CustomerTypeName { get; set; }
        public string CustomerTypeId { get; set; }

        public string PaymentType { get; set; }
        public string PaymentReceivedDate { get; set; }
        public string ReceivedAmount { get; set; }
        public string Message { get; set; }
        public string Remarks { get; set; }
    }

    public class CardDetails
    {
        public string CardIdentifier { get; set; }
        public string VechileNo { get; set; }
        public string VehicleType { get; set; }
        public string VehicleMake { get; set; }
        public int YearOfRegistration { get; set; }
        public string MobileNo { get; set; }
    }

}
