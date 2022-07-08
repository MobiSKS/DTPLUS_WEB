using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.AshokLeyLand
{
    public class PendingKYCCustomerDetailsModel : CommonResponseBase
    {
        public string CustomerID { get; set; }
        public List<PendingKYCCustomerData> Data { get; set; }
        public PendingKYCCustomerDetailsModel()
        {
            Data = new List<PendingKYCCustomerData>();
        }
    }
    public class PendingKYCCustomerData
    {
        public string CustomerID { get; set; }
        public string FormNumber { get; set; }
        public string IndividualOrgName { get; set; }
        public string Address { get; set; }
        public string CommunicationPhoneNo { get; set; }
        public string CommunicationMobileNo { get; set; }
        public string Reason { get; set; }
        public int Status { get; set; }
    }
}