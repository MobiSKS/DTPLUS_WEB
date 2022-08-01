using HPCL.Common.Models.CommonEntity;
using System;

namespace HPCL.Common.Models.ViewModel.CustomerRequest
{
    public class ApproveCardRenwalRequestReq : BaseEntity
    {
        public ApproveCardRenwalRequestReq()
        {
            FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
        }
        public string CardNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
