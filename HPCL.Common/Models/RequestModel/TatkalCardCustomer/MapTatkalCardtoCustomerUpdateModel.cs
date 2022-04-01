using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.TatkalCardCustomer
{
    public class MapTatkalCardtoCustomerUpdateModel:BaseEntity
    {
        public MapTatkalCardtoCustomerUpdateModel()
        {
            ObjCardMap = new List<MapTatkalCardtoCustomerUpdateDetails>();
        }
        public string CustomerId { get; set; }
        
        public List<MapTatkalCardtoCustomerUpdateDetails> ObjCardMap { get; set; }
    }
    public class MapTatkalCardtoCustomerUpdateDetails
    {
        public string CardNo { get; set; }
    }

}
