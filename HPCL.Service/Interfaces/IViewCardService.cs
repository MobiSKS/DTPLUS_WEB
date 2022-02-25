using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.ViewCard;
using HPCL.Common.Models.ViewModel.ViewCard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
   public interface IViewCardService
    {
        Task<List<ViewCardSearchResult>> ViewCardSearch(ViewCardDetails entity);
        Task<List<ViewCardSearchResult>> SearchCardMapping(string Customerid);
        Task<string> UpdateCards(ObjUpdateMobileandFastagNoInCard[] limitArray);

    }
}
