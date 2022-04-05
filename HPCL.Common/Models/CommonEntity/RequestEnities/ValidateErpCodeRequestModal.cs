using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.CommonEntity.RequestEnities
{
    public class ValidateErpCodeRequestModalInput : BaseEntity
    {
        public string ErpCode { get; set; }
    }

    public class ValidateErpCodeRequestModalOutput : SuccessResponse
    {

    }
}
