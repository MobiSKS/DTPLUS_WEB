using HPCL.Common.Helper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.EFT
{
    public class CCMSRechargethroughEFTModel:CommonResponseBase
    {
        public string Status { get; set; }
        public string Reason{ get; set; }
        public string FileName { get; set; }
        public IFormFile TransactionDetailFile { get; set; }
        public string TransactionDetailFileId { get; set; }
        public CCMSRechargeSummaryViewModel UploadEFTSummaryData { get; set; }
    }
}
