using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.AshokLeyLand
{
    public class VerifyCustomerDocumentsRequest: BaseEntity
    {
        public string customerID { get; set; }
        public string customerStatus { get; set; }
        public string remarks { get; set; }        
    }
}