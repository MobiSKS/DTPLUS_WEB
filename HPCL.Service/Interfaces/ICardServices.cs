using HPCL.Common.Models;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.Cards;
using HPCL.Common.Models.ResponseModel.Cards;
using HPCL.Common.Models.ViewModel.Cards;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ICardServices
    {
        Task<SearchManageCards> ManageCards(CustomerCards entity, string editFlag);
        Task<SearchDetailsByCardId> ViewCardDetails(string CardId);
        Task<string> UpdateService(string serviceId, bool flag);
        Task<UpdateMobileModal> CardlessMapping(string cardNumber, string mobileNumber, string LimitTypeName, string CCMSReloadSaleLimitValue);
        Task<List<SuccessResponse>> CardlessMappingUpdate(string mobNoNew, string crdNo);
        Task<SearchCardsResponse> AcDcCardSearch(SearchCards entity);
        Task<string> UpdateStatus(string cardNo, int Statusflag);
        Task<GetCardLimitResponse> SetSaleLimit(GetCardLimit entity);
        Task<SetSaleLimitUpdateRes> UpdateCards(ObjCardLimits[] limitArray);
        Task<SearchCcmsLimitAllResponse> SearchCcmsLimitForAllCards(GetCcmsLimitAll entity);
        Task<string> UpdateCcmsLimitAllCards(int limitype, int amountVal);
        Task<IndividualCardResponse> SetCcmsForIndCards(SetIndividualLimit entity);
        Task<string> UpdateCcmsIndividualCard(string objCCMSLimits, string viewGirds);
        Task<List<ViewGird>> ViewUpdatedGrid();
        Task<List<CardListResponse>> GetCardList();
        Task<CustomerSearchDetails> ManageMapping(GetCustomerDetailsMapMerchant entity);
        Task<FetchMerchant> GetMerchantForMapping(GetCustomerDetailsMapMerchant entity);
        Task<string> SaveCustomerMappingMerchant(string objCardMerchantMaps, string status);
        Task<SearchAllowedMerchantResponse> SearchAllowedMerchant(SearchAllowedMerchant entity);
        Task<LimitUpdateForSingleRechargeCardsRes> LimitUpdateForSingleRechargeCardsService(GetLimitUpdateForSingleRechargeCards entity);
        Task<List<SuccessResponse>> LimitUpdateForSingleRecharge(string objCCMSLimits);
        Task<GetEmergencyAddOnCardResponse> EmergencyAddOnCard(GetEmergencyAddOnCard entity);
        Task<List<SuccessResponse>> MapEmergencyAddOnCard(string objCards);
        Task<EnableCustomerServicesModel> EnableCustomerServices();
        Task<GetDetailForEnableDisableProductsAndTransactions> GetDetailForEnableDisableProductsAndTransactions(string CustomerId, string CardNo, string MobileNo);
        Task<CommonResponseData> EnableDisableProductsAndTransaction(string ObjProductsInput, string ObjTransactionsInput, string CustomerId, string CardNo, string MobileNo);
        Task<CorporateMultiRechargeLimitModel> GetCustomerRechargeLimitConfig(string CustomerId);
        Task<List<SuccessResponse>> ConfigureLimits([FromBody] CorporateMultiRechargeLimitRequest reqModel);
        Task<GetGenericAttachedVehicleRes> GetGenericAttachedVehicle(GetGenericAttachedVehicleReq entity);
        Task<GetCardWiseLimitRes> CardWiseLimit(GetCardWiseLimit entity);
    }
}
