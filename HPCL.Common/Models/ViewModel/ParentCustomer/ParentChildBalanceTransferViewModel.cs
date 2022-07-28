using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.ParentCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.ParentCustomer
{
    public class ParentChildBalanceTransferViewModel : CommonResponseBase
    {
        public ParentChildBalanceTransferViewModel()
        {
            Data = new ParentChildTransferDetails();
        }
        public string ParentCustomerID { get; set; }
        public string ChildCustomerId { get; set; }

        public ParentChildTransferDetails Data { get; set; }

    }
    public class ParentChildTransferDetails
    {
        public ParentChildTransferDetails()
        {
            GetParentCustomer = new List<ParentTransferDetails>();
            GetChildCustomer = new List<ChildTransferDetails>();

        }
        public List<ParentTransferDetails> GetParentCustomer { get; set; }
        public List<ChildTransferDetails> GetChildCustomer { get; set; }
    }
    public class ParentTransferDetails
    {
        public string AvailableCCMSBalance { get; set; }
        public string AvailableDriveStars { get; set; }
    }
    public class ChildTransferDetails
    {
        public ChildTransferDetails()
        {
            BalanceTransferTypes = new List<GetTransactionType>();
        }
        public string ChildId { get; set; }
        public string CardNo { get; set; }
        public string NameOnCard { get; set; }
        public string CCMSBalance { get; set; }
        public string Drivestars { get; set; }
        public string RegionalOffice { get; set; }
        public string StatusName { get; set; }
        public List<GetTransactionType> BalanceTransferTypes { get; set; }
        public string BalanceTransferTypeId { get; set; }
        public string AmounttoTransfer { get; set; }
    }
}
