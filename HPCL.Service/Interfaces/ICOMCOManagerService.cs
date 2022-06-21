using HPCL.Common.Models.ViewModel.COMCOManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ICOMCOManagerService
    {
        Task<COMCOCustomerMappingViewModel> COMCOCustomerMapping(COMCOCustomerMappingViewModel Model);
    }
}