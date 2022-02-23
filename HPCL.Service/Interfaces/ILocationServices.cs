using HPCL.Common.Models.ViewModel.Locations;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ILocationServices
    {
        Task<string> HeadOfficeDetails(HeadOfficeDetails headOfficeDetails);
    }
}
