using HPCL.Common.Models.CommonEntity;
using System.Collections.Generic;

namespace HPCL.Common.Models.ViewModel.CustomerRequest
{
    public class UpdateApproveCardRenwalRequestReq : BaseEntity
    {
        public string ActionType { get; set; }
        public TypeApproveCardRenewalRequestsList[] TypeApproveCardRenewalRequests { get; set; }
    }

    public class TypeApproveCardRenewalRequestsList
    {
        public string CardNo { get; set; }
        public string RenewalRemark { get; set; }
    }
}
