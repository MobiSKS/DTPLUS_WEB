using System.Net.Http;
using System.Threading.Tasks;

namespace HPCL.Web.RequestServices
{
    public interface IRequestService
    {
        Task<string> CommonRequestService(StringContent content, string requestUrl);
    }
}
