using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.CommonEntity.RequestEnities
{
    public class ValidateMappedMerchantIdModalInput : BaseEntity
    {
        public string MappedMerchantID { get; set; }
    }

    public class ValidateMappedMerchantIdModalOutput : SuccessResponse
    {

    }
}
