using HPCL.Common.Helper;
using HPCL.Common.Models.ResponseModel.ParentCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.ParentCustomer
{
    public class AccountStatementRequestViewModel : CommonResponseBase
    {
        public AccountStatementRequestViewModel()
        {
            
            StatementTypes = new List<GetAccountStatementType>();
            StatementTypes.Add(new GetAccountStatementType
            {
                TypeId = "0",
                TypeName = "--Select--"
            });
        }
        public string CustomerId { get; set; }
        public AccountStatementDetails Data { get; set; }
        public string StatementTypeId { get; set; }
        public List<GetAccountStatementType> StatementTypes { get; set; }
    }
    public class AccountStatementDetails
    {
        public AccountStatementDetails()
        {
            GetAccountStatmentRequest = new List<GetCustomerDetails>();
            GetAccountStatmentRequestDetails = new List<GetStatementDetails>();
        }
            public List<GetCustomerDetails> GetAccountStatmentRequest { get; set; }
        public List<GetStatementDetails> GetAccountStatmentRequestDetails { get; set; }
    }
    public class GetStatementDetails
    {
        public string StatementName { get; set; }
        public string optedDate { get; set; }
      public string statementType { get; set; }
        public string reqestId { get; set; }
        public string actionName { get; set; }

    }
    public class GetCustomerDetails
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ZO { get; set; }
        public string RO { get; set; }
        public string NameOnCard { get; set; }

    }
}

