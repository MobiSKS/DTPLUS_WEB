using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.JCB
{
    public class ViewJCBUnmappedOTCCardsModel
    {
        public string DealerCode { get; set; }
        public string CardNumber { get; set; }
        public string Remarks { get; set; }
        public bool ShowUnmappedCard { get; set; }
    }
}