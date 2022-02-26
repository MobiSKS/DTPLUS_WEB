using HPCL.Common.Models.RequestModel.Merchant;
using HPCL.Common.Models.ViewModel.Merchant;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IMerchantServices
    {
        Task<MerchantGetDetailsModel> CreateMerchant(string merchantIdValue, string fromDate, string toDate, string category);
        Task<Tuple<string, string>> CreateMerchant(MerchantGetDetailsModel merchantMdl);
        Task<MerchantApprovalModel> VerifyMerchant(MerchantApprovalModel merchaApprovalMdl);
        Task<string> ActionOnMerchantID([FromBody] ApproveRejectListRequestModal erpnmodel);
    }
}
