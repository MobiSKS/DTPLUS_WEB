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
        Task<MerchantGetDetailsModel> CreateMerchant(string merchantIdValue, string fromDate, string toDate, string category,string ERPCode, string actionFlow);
        Task<Tuple<string, string>> CreateMerchant(MerchantGetDetailsModel merchantMdl);
        Task<MerchantApprovalModel> VerifyMerchant(MerchantApprovalModel merchaApprovalMdl);
        Task<string> ActionOnMerchantID([FromBody] ApproveRejectListRequestModal erpnmodel);
        Task<MerchantRejectedModel> RejectedMerchant(string FromDate, string ToDate);
        Task<MerchantModel> MerchantSummary(string ERPCode, string fromDate, string toDate);
        Task<SearchMerchantModel> SearchMerchant();
        Task<List<SearchDetailsTableModel>> SearchMerchantDetails(string merchantId, string erpCode, string retailOutletName, string city, string highwayNo, string status);
    }
}
