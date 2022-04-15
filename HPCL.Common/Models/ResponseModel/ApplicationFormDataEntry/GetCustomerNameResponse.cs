using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.ApplicationFormDataEntry
{
    public class GetCustomerNameResponse : ResponseMsg
    {
        public List<GetCustomerNameResponsedata> data { get; set; }
    }

    public class GetCustomerNameResponsedata
    {
        public string CustomerName { get; set; }
        public string FormNumber { get; set; }
        public string CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; }
        public string NoOfCards { get; set; }
        public string PaymentType { get; set; }
        public string PaymentReceivedDate { get; set; }
        public decimal ReceivedAmount { get; set; }
        public string RBEId { get; set; }
        public string RBEName { get; set; }
    }
}
