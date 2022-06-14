using HPCL.Common.Models;
using HPCL.Common.Models.ViewModel.ValidateAddOnCards;
using HPCL.Common.Models.ViewModel.ValidateNewCards;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IValidateAddOnCardsService
    {
        Task<ValidateAddOnCardsModel> Details(ValidateAddOnCardsModel validateNewCardsMdl);
        Task<List<VehicleDetailsModel>> GetCardDetailsForApproval(string CustomerRefNo);
        Task<CommonResponseData> ActionOnCards([FromBody] ApproveCardDetailsModel approveRejectModel);
    }
}