using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Cards;
using HPCL.Common.Models.ViewModel.Cards;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace HPCL.Service.Interfaces
{
    public interface IControlCardSearch
    {
        Task<List<ControlCardSearchResponse>> ControlCardSearch(ControlCardRequest entity);
    }
}
