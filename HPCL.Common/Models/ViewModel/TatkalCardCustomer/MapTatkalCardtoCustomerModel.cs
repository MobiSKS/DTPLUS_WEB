using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HPCL.Common.Models.ViewModel.TatkalCardCustomer
{
    public class MapTatkalCardtoCustomerModel:BaseEntity
    {
       
        public string CustomerId { get; set; }
        public string FileId { get; set; }
    }
}
