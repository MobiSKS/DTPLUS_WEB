using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.ViewCard;
using HPCL.Common.Models.ViewModel.ViewCard;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
   public interface IViewCardService
    {
        Task<ViewCardSearch> ViewCardSearch(string CustomerId);
        Task<ViewCardSearch> SearchCardMapping(ViewCardDetails viewCardDetails);
        Task<string> UpdateCards(ObjUpdateMobileandFastagNoInCard[] limitArray);
        Task<ViewCardSearch> AddCardMappingDetails(ViewCardDetails viewCardDetails);

    }
}
