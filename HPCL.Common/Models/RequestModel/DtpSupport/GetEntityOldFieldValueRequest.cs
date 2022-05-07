using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.RequestModel.DtpSupport
{
    public class GetEntityOldFieldValueRequest : BaseEntity
    {
        public string EntityTypeId { get; set; }
        public string EntityFieldId { get; set; }
        public string CustomerIdOrCardOrMerchantId { get; set; }
    }
}