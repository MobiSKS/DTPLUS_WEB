using HPCL.Common.Models.ResponseModel.Locations;
using HPCL.Common.Models.ViewModel.Locations;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ILocationServices
    {
        Task<HeadOfficeDetailsResponse> HeadOfficeDetails();
        Task<string> UpdateHod(HeadOfficeDetailsResponse entity);
    }
}
