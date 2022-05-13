using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.TMS
{
    public class NavigateToTransportManagementSystemModel: BaseEntity
    {
        public string CustomerId { get; set; }
        public string Message { get; set; }
        public string url { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string tmsUserId { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
        public int StatusCode { get; set; }
        public string Success { get; set; }
    }
}