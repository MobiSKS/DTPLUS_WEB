using System.Net.Http;
using System.Threading.Tasks;

namespace HPCL.Service
{
    public interface IRequestService
    {
        Task<string> CommonRequestService(StringContent content, string requestUrl);
        Task<string> PANValidationService(StringContent content, string requestUrl);
        Task<string> VehicleRegistrationValidCheckService(StringContent content, string requestUrl);
        
    }
}
