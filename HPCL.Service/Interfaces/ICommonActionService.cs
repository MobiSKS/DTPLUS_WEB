using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ICommonActionService
    {
        Task<List<LimitTypeModal>> GetLimitType();
        Task<List<OfficerTypeResponseModal>> GetOfficerTypeList();
        Task<List<StateResponseModal>> GetStateList();
        Task<List<DistrictResponseModal>> GetDistrictList(string stateId);
        Task<List<ZonalOfficeResponseModal>> GetZonalOfficeList();
        Task<List<RegionalOfficeResponseModal>> GetRegionalOfficeList(string zonalOfcID);
        Task<List<HqResponseModal>> GetHqList();
        Task<List<LocationMappingResponseModal>> GetLocationMappingList(int officerId);
        Task<string> ValidateUserName(string userName);
    }
}
