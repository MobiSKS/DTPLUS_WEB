using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.Security
{
    public class UserCreationRequestViewModel : CommonResponseBase
    {
        public UserCreationRequestViewModel()
        {
            StatusResponseMdl = new List<StatusResponseModal>();
            Data = new List<UserCreationRequestDetails>();

            FromDate = DateTime.Now.AddMonths(-1).ToString("dd-MM-yyyy");
            ToDate = DateTime.Now.ToString("dd-MM-yyyy");
            Status = -1;
        }
        public virtual List<StatusResponseModal> StatusResponseMdl { get; set; }
        public string UserName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int Status { get; set; }
        public virtual List<UserCreationRequestDetails> Data { get; set; }
    }
    public class UserCreationRequestDetails
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string MakerComment { get; set; }
        public string CheckerComment { get; set; }
        public string RequestedOn { get; set; }
        public string RequestedBy { get; set; }
        public string ApprovedOn { get; set; }
        public string ApprovedBy { get; set; }
        public string IsApproved { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}