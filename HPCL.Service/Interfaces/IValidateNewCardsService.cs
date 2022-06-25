using HPCL.Common.Models;
using HPCL.Common.Models.ViewModel.ValidateNewCards;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IValidateNewCardsService
    {
        Task<ValidateNewCardsModel> Details(ValidateNewCardsModel validateNewCardsMdl);
        Task<List<VehicleDetailsModel>> GetCardDetailsForApproval(string CustomerRefNo, string FormNumber);
        Task<CommonResponseData> ActionOnCards([FromBody] ApproveCardDetailsModel approveRejectModel);
    }
}