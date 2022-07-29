using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.ParentCustomer
{
    public class UpdatePCBalanceTransferRequest:BaseEntity
    {
        public string ParentCustomerId { get; set; }
        public List<UpdatePCBalanceTransferDetails> TypeUpdateParenttoChildandChildParentFund { get; set; }
    }
    public class UpdatePCBalanceTransferDetails
    {
        public string ChildCustomerId { get; set; }
        public string ControlCardNumber { get; set; }
        public string CCMSBalance { get; set; }
        public string Drivestars { get; set; }
        public decimal Amount { get; set; }
        public string BalanceTransferType { get; set; }
       
    }
}
