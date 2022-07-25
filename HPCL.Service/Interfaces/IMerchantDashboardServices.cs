using HPCL.Common.Models.RequestModel.MerchantDashboard;
using HPCL.Common.Models.ResponseModel.MerchantDashboard;
using HPCL.Common.Models.ViewModel.Merchant;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IMerchantDashboardServices
    {

        Task<List<KeyInformationResponseModel>> keyInformation(string MerchantId);
        Task<List<TodaysTransactionSummaryResponseModel>> TodaysTransactionSummaries(string MerchantId);

        Task<List<LastTrasactionResponseModel>> lastTrasaction(string MerchantId);
        Task<List<LastBatchDetailResponseModel>> lastBatches(string MerchantId);

        Task<List<LastSaleReloadEarningDetailsResponseModel>> lastSaleReloadEarningDetails(string MerchantId);  
        Task<List<KeyEventAndFigureResponseModel>> lastKeyEventAndFigure(string MerchantId);
        //Task<SearchMerchantModel> SearchMerchant();
        //Task KeyInformation(string merchantId);
        //Task SearchMerchantDetails(string merchantId, string erpCode, string retailOutletName, string city, string highwayNo, string status);
    }
}
