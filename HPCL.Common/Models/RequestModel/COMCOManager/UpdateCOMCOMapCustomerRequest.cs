using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.COMCOManager
{
    public class UpdateCOMCOMapCustomerRequest : BaseEntity
    {
        public string MerchantID { get; set; }
        public List<TypeCOMCOMapCustomer> TypeCOMCOMapCustomer { get; set; }
        public UpdateCOMCOMapCustomerRequest()
        {
            TypeCOMCOMapCustomer = new List<TypeCOMCOMapCustomer>();
        }
    }
    public class TypeCOMCOMapCustomer
    {
        public String CustomerID { get; set; }
    }
}