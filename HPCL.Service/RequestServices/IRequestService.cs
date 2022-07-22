using System.Net.Http;
using System.Threading.Tasks;

namespace HPCL.Service
{
    public interface IRequestService
    {
        Task<string> CommonRequestService(StringContent content, string requestUrl);
        Task<string> PANValidationService(StringContent content, string requestUrl);
        Task<string> VehicleRegistrationValidCheckService(StringContent content, string requestUrl);
        Task<string> FormDataRequestService(MultipartFormDataContent content, string requestUrl);
        Task<string> RechargeRequestService(StringContent content, string requestUrl, string reqIp);
        Task CommonRequestService(object regionInformationTableContent, string regionInformation);
    }
}
