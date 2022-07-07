﻿using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.RequestModel.Dealer;
using HPCL.Common.Models.ViewModel.Dealer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IDealerService
    {
        Task<DealerCreditMappingViewModel> GetDealerCreditMappingDetails(string CustomerID);
        Task<List<string>> SaveDealerForCreditSale([FromBody] DealerForCreditSaleViewModel dealerForCreditSaleViewModel);
        Task<List<SuccessResponse>> MerchantMapEnableorDisable(string customerID, string merchantID, string action);
        Task<List<UpdateMerchantSuccessResponse>> UpdateDealerCreditmapping([FromBody] DealerRequestModel DealerRequestModel);
        Task<DealerCreditSaleStatement> GetCreditSaleStatement(string CustomerID, string MerchantID, string FromDate, string ToDate);
        Task<List<UpdateMerchantSuccessResponse>> UpdateDealerCreditPaymentBulk([FromBody] DealerRequestModel DealerRequestModel);
        Task<DealerCreditPaymentinBulk> GetDealerCreditPaymentBulk(string CustomerId);
        Task<DealerCreditSaleViewModel> GetDealerCreditSaleView(string CustomerID, string MerchantID, string FromDate, string ToDate);
        Task<DealerCreditSaleViewModel> GetMerchantDealerCreditSaleView(string CustomerID, string MerchantID, string FromDate, string ToDate);
    }
}
