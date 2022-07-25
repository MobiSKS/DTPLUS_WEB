using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.CustomerDashboard
{
    public class LastestDrivestarsTransactionResponseModel
    {
        public string TransactionType { get; set; }
        public string Description { get; set; }
        public string Period { get; set; }
        public string TransactionDate { get; set; }
        public string DrivestarsAwarded { get; set; }
        public string ReferenceNumber { get; set; }
        public string ItemName { get; set; }
        public string RequestedDate { get; set; }
        public string RedeemedPoints { get; set; }
        public string Status { get; set; }

    }
}
