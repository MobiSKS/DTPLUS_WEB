using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class CustomerDriveStarsDetailsModel:CommonResponseBase
    {
        public CustomerDriveStarsDetailsModel()
        {
            Data = new List<CustomerDriveStarsDetails>();
        }
        public List<CustomerDriveStarsDetails> Data { get; set; }
    }
    public class CustomerDriveStarsDetails
    {
        public string Mode { get; set; }
        public string Description { get; set; }
        public string TransactionDate { get; set; }
        public string OpeningBalance { get; set; }
        public string PostingMethod { get; set; }
        public string Drivestars { get; set; }
        public string ClosingBalance { get; set; }
    }
}
