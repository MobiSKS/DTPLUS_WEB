using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.Cards
{
    public class GetDetailForEnableDisableProductsAndTransactions : CommonResponseBase
    {
        public ProductsAndTransactions Data { get; set; }
        public GetDetailForEnableDisableProductsAndTransactions()
        {
            Data = new ProductsAndTransactions();
        }
    }
    public class ProductsAndTransactions
    {
        public ProductsAndTransactions()
        {
            GetProductsTransactionsStatus = new List<GetProductsTransactionsStatusDetails>();
            GetProductsType = new List<GetProductsTypeDetails>();
            GetTransactionsType = new List<GetTransactionsTypeDetails>();
        }
        public List<GetProductsTransactionsStatusDetails> GetProductsTransactionsStatus { get; set; }
        public List<GetProductsTypeDetails> GetProductsType { get; set; }
        public List<GetTransactionsTypeDetails> GetTransactionsType { get; set; }
    }
    public class GetProductsTransactionsStatusDetails
    {
        public int Status { get; set; }
        public string Reason { get; set; }
    }
    public class GetProductsTypeDetails
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int StatusFlag { get; set; }
    }
    public class GetTransactionsTypeDetails
    {
        public int TransactionID { get; set; }
        public string TransactionType { get; set; }
        public int StatusFlag { get; set; }
    }
}