using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.Customer
{
    public class CustomerResponseByReferenceNo
    {
        public int Status_Code { get; set; }
        public int Internel_Status_Code { get; set; }
        public string Message { get; set; }

        public Boolean Success { get; set; }

        public List<CustomerResponseDataByReferenceNo> Data { get; set; }
    }

    public class CustomerResponseDataByReferenceNo
    {
        public string Title { get; set; }

        public string KeyInitials { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FormNumber { get; set; }
        public string CustomerTypeName { get; set; }

        public string CustomerTypeId { get; set; }
        public string PaymentType { get; set; }
        public string PaymentReceivedDate { get; set; }
        public string NoOfCards { get; set; }
        public string ReceivedAmount { get; set; }
        public string RBEId { get; set; }
        public string RBEName { get; set; }
        public string CustomerName { get; set; }
        public int Status { get; set; }
    }
}
