﻿using HPCL.Common.Models.CommonEntity;
using System.Collections.Generic;

namespace HPCL.Common.Models.ViewModel.ApplicationFormDataEntry
{
    public class AddAddOnCard : BaseEntity
    {
        public string CustomerId { get; set; }
        public string FormNumber { get; set; }
        public string NoOfCards { get; set; }
        public string RBEId { get; set; }
        public string RBEName { get; set; }
        public int FeePaymentsCollectFeeWaiver { get; set; }
        public string FeePaymentNo { get; set; }
        public string FeePaymentDate { get; set; }
        public List<ObjCardDetail> ObjCardDetail { get; set; }
        public string CardPreference { get; set; }
        public string NoofVechileforAllCards { get; set; }
        public string CustomerTypeId { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public string NoOfVehiclesAllCards { get; set; }
    }

    public class ObjCardDetail
    {
        public string CardIdentifier { get; set; }
        public string VechileNo { get; set; }
        public string VehicleType { get; set; }
        public string VehicleMake { get; set; }
        public string YearOfRegistration { get; set; }
        public string MobileNo { get; set; }
    }
}
