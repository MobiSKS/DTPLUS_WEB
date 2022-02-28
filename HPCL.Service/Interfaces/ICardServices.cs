﻿using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Cards;
using HPCL.Common.Models.ViewModel.Cards;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ICardServices
    {
        Task<CustomerCards> ManageCards();
        Task<List<SearchGridResponse>> ManageCards(CustomerCards entity, string editFlag);
        Task<Tuple<List<SearchCardResult>, List<LimitResponse>, List<ServicesResponse>>> ViewCardDetails(string CardId);
        Task<string> UpdateService(string serviceId, bool flag);
        Task<UpdateMobileModal> CardlessMapping(string cardNumber, string mobileNumber, string LimitTypeName, string CCMSReloadSaleLimitValue);
        Task<string> CardlessMapping(UpdateMobileModal entity);
        Task<List<SearchCardsResponse>> AcDcCardSearch(SearchCards entity);
        Task<string> UpdateStatus(string cardNo, int Statusflag);
        Task<List<SearchCardsResponse>> RefreshGrid();
        Task<GetCardLimit> SetSaleLimit();
        Task<List<GetCardLimitResponse>> SetSaleLimit(GetCardLimit entity);
        Task<string> UpdateCards(ObjCardLimits[] limitArray);
        Task<GetCcmsLimitAll> SetCcmsLimitForAllCards();
        Task<List<SearchCcmsLimitAllResponse>> SearchCcmsLimitForAllCards(GetCcmsLimitAll entity);
        Task<string> UpdateCcmsLimitAllCards(GetCcmsLimitAll entity);
        Task<Tuple<List<SearchIndividualCardsResponse>, List<CCMSBalanceDetail>>> SetCcmsForIndCards(SetIndividualLimit entity);
        Task<string> UpdateCcmsIndividualCard(string objCCMSLimits, string viewGirds);
        Task<List<ViewGird>> ViewUpdatedGrid();
    }
}