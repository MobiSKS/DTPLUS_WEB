using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.Cards
{
    public class EnableDisableProductsAndTransactionsRequest : BaseEntity
    {
        public string CustomerId { get; set; }
        public string CardNo { get; set; }
        public string MobileNo { get; set; }
        public string Remarks { get; set; }
        public ProductTypeDetails[] ObjProducts { get; set; }
        public TransactionTypeDetails[] ObjTransactions { get; set; }
    }
    public class ProductTypeDetails
    {
        public int productID { get; set; }
        public int statusFlag { get; set; }
    }
    public class TransactionTypeDetails
    {
        public int TransactionID { get; set; }
        public int StatusFlag { get; set; }
    }
}