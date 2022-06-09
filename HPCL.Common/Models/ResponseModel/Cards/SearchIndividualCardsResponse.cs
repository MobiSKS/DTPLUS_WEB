using Castle.MicroKernel.SubSystems.Conversion;
using HPCL.Common.Models.ResponseModel.CommonResponse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPCL.Common.Models.ResponseModel.Cards
{
    public class IndividualCardResponse : ResponseMsg
    {
        public DataObject data { get; set; }
    }

    public class DataObject
    {
        public List<SearchIndividualCardsResponse> CCMSBasicDetail { get; set; }
        public List<CCMSBalanceDetail> CCMSBalanceDetail { get; set; }
    }

    public class SearchIndividualCardsResponse
    {
        public int SrNumber { get; set; }
        public string CardNumber { get; set; }
        public string VechileNo { get; set; }
        public string VehicleType { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Status { get; set; }
        public int YearOfRegistration { get; set; }
        public string MobileNumber { get; set; }
        public string VehicleMake { get; set; }
        public int CCMSLimitOption { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "money")]
        public Decimal CCMSReloadSaleLimitValue { get; set; }
    }

    public class CCMSBalanceDetail
    {
        public Decimal ActualCCMSBalance { get; set; }
        public Decimal UnallocatedCCMSBalance { get; set; }
    }
}
