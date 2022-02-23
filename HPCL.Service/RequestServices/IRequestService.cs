using System.Net.Http;
using System.Threading.Tasks;

namespace HPCL.Service
{
    public interface IRequestService
    {
        Task<string> CommonRequestService(StringContent content, string requestUrl);
    }
}
