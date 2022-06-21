using HPCL.Common.Models.CommonEntity;
using System;

namespace HPCL.Common.Models.ViewModel.HDFCBankCreditPouch
{
    public class SearchEnrollStatus : BaseEntity
    {
        public string CustomerId { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }
        public int ZO { get; set; }
        public int RO { get; set; }
    }

    public class SearchEnrollStatusClone
    {
        public SearchEnrollStatusClone()
        {
            FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
        }

        public string CustomerId { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }
        public string ZO { get; set; }
        public string RO { get; set; }
    }
}
