using HPCL.Common.Models.ResponseModel.Cards;
using HPCL.Common.Models.ViewModel.Cards;
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
        Task<string> CardlessMapping(UpdateMobileModal entity);
        Task<SearchCardsResponse> AcDcCardSearch(SearchCards entity);
        Task<string> UpdateStatus(string cardNo, int Statusflag);
        Task<GetCardLimitResponse> SetSaleLimit(GetCardLimit entity);
        Task<string> UpdateCards(ObjCardLimits[] limitArray);
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
    }
}
