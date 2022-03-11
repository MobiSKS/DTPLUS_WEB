using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ViewModel.Terminal;
using System.Collections.Generic;

namespace HPCL.Common.Models.ViewModel
{
    public class TerminalManagementRequestViewModel:CommonResponseBase
    {
        public TerminalManagementRequestViewModel()
        {
            ZonalOffices = new List<ZonalOfficeResponseModal>();
            ZonalOffices.Add(new ZonalOfficeResponseModal
            {
                ZonalOfficeID = 0,
                ZonalOfficeName = "--All--"
            });
            
            Reasons = new List<TerminalManagementCloseReasonModel>();
            Reasons.Add(new TerminalManagementCloseReasonModel
            {
                ReasonId = 0,
                ReasonName="--Select--"
            }) ;
            TerminalManagementRequestDetails = new List<TerminalManagementRequestDetailsModel>();
        }

        public string MerchantId { get; set; }
        public string TerminalId { get; set; }
        public string ZonalOfficeId { get; set; }
        public string RegionalOfficeId { get; set; }
        public string RegionalOfcIdVal { get; set; }
        public string CloseRequestStatus { get; set; }
        public string Reason { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public virtual List<TerminalManagementCloseReasonModel> Reasons { get; set; }
        public virtual List<ZonalOfficeResponseModal> ZonalOffices { get; set; }
        
        public virtual List<TerminalManagementRequestDetailsModel> TerminalManagementRequestDetails { get; set; }

    }
}
