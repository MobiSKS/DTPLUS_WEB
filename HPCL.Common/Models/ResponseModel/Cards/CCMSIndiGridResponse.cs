using System;

namespace HPCL.Common.Models.ResponseModel.Cards
{
    public class ViewGird
    {
        public string Cardno { get; set; }
        public string vehicleNo { get; set; }
        public string issueDate { get; set; }
        public string expiryDate { get; set; }
        public string status { get; set; }
        public string Mobileno { get; set; }
        public int Limittype { get; set; }
        public string limitTypeText { get; set; }
        public Decimal Amount { get; set; }
    }
}
