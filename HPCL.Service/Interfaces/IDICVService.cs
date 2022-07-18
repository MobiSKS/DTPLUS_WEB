using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ViewModel.DICV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IDICVService
    {
        Task<List<OfficerTypeResponseModal>> GetDICVOfficerTypeList();
        Task<DICVDealerEnrollmentModel> DICVDealerEnrollment();
    }
}