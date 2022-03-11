using HPCL.Common.Models.ResponseModel.AshokLayland;
using HPCL.Common.Models.ViewModel.AshokLeyLand;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IAshokLeyLandService
    {
        Task<InsertResponse> InsertAlEnroll(string str);
        Task<SearchAlResult> SearchDealer(string dealerCode, string dtpCode);
        Task<InsertResponse> AlEnrollUpdate(string getAllData);
        Task<ALOTCCardRequestModel> DealerOTCCardRequest();
        Task<ALOTCCardRequestModel> DealerOTCCardRequest(ALOTCCardRequestModel alOTCCardRequestModel);
    }
}
