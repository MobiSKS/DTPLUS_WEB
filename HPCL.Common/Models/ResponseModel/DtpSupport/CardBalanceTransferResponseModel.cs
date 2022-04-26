using HPCL.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.DtpSupport
{
    public class CardBalanceTransferResponseModel:CommonResponseBase
    {
        public virtual List<CardBalanceTransferResponseDetails> Data { get; set; }
    }
    public class CardBalanceTransferResponseDetails
    {

    }
}
