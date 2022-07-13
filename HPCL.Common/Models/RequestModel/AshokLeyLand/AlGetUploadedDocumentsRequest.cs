using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.AshokLeyLand
{
    public class AlGetUploadedDocumentsRequest : BaseEntity
    {
        public string CustomerID { get; set; }
    }
}
