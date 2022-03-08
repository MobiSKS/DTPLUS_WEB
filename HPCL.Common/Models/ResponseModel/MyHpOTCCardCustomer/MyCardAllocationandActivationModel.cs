using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer
{
    public class MyCardAllocationandActivationModel : CommonResponseData
    {
        public MyCardAllocationandActivationModel()
        {
            MyCardAllocationandActivationDetails = new List<MyCardAllocationandActivationDetails>();
        }
        public List<MyCardAllocationandActivationDetails> MyCardAllocationandActivationDetails { get; set; }
    }
    public class MyCardAllocationandActivationDetails
    {

        public string ZonalOfficeName { get; set; }
        public string RegionalOfficeName { get; set; }
        public string FormNumber { get; set; }
        public string CardNo { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MappingDate { get; set; }
        public string AllocatedMID { get; set; }
        public string ZO { get; set; }
        public string RO { get; set; }
        public string RetailOutletName { get; set; }
        public string AllocationDate { get; set; }
        public string FirstTransactionDate { get; set; }
    }
}
