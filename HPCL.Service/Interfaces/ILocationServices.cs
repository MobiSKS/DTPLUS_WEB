using HPCL.Common.Models.ResponseModel.Locations;
using HPCL.Common.Models.ViewModel.Locations;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ILocationServices
    {
        Task<HeadOfficeDetailsResponse> HeadOfficeDetails();
        Task<string> UpdateHod(HeadOfficeDetailsResponse entity);
        Task<string> DeleteCity(int cityId);
        Task<string> DeleteZonalOffice(int zonalOfficeID);
        Task<string> DeleteRegionalOffice(int regionalOfficeID);
        Task<string> DeleteCountryRegion(int regionID);
        Task<string> DeleteState(int stateID);
        Task<string> DeleteDistrict(int districtID);
    }
}
