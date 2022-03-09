using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ViewModel.Terminal
{
    public class SearchTerminalModel : BaseEntity
    {
        public SearchTerminalModel()
        {
            TerminalTypeResponseModals = new List<TerminalTypeResponseModal>();
            TerminalTypeResponseModals.Add(new TerminalTypeResponseModal
            {
                id = 0,
                TerminalIssuanceType = "-ALL-"
            });
        }
        public string TerminalId { get; set; }
        public string MerchantId { get; set; }
        public string TerminalType { get; set; }
        public string IssueDate { get; set; }
        public virtual List<TerminalTypeResponseModal> TerminalTypeResponseModals { get; set; }
    }
}
