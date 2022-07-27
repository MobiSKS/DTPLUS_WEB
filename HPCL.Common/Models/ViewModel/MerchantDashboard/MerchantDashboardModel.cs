using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.MerchantDashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.MerchantDashboard
{
    public class MerchantDashboardModel : BaseEntity
    {

        public MerchantDashboardModel()
        {
          KeyInformationResponseModels = new List<KeyInformationResponseModel>();
          todaysTransactionResponseModels = new List<TodaysTransactionSummaryResponseModel>();
            LastTrasactionResponseModels = new List<LastTrasactionResponseModel>();
            LastBatchDetailResponseModels = new List<LastBatchDetailResponseModel>();
            LastSaleReloadEarningDetailsResponseModels = new List<LastSaleReloadEarningDetailsResponseModel>();
            keyEVentsAndFigureResponseModels = new List<KeyEventAndFigureResponseModel>();


        }
        public List<KeyInformationResponseModel> KeyInformationResponseModels { get; set; }
        public List<TodaysTransactionSummaryResponseModel> todaysTransactionResponseModels { get; set; }

        public List<LastBatchDetailResponseModel> LastBatchDetailResponseModels { get; set; }   

        public List<LastSaleReloadEarningDetailsResponseModel> LastSaleReloadEarningDetailsResponseModels { get; set; }

     

        public List<LastTrasactionResponseModel> LastTrasactionResponseModels { get; set; }

        public List<KeyEventAndFigureResponseModel> keyEVentsAndFigureResponseModels { get; set; }

     


    }
}
