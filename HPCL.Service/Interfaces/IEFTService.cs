using HPCL.Common.Models.ViewModel.EFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IEFTService
    {
        Task<CCMSRechargeSummaryViewModel> CCMSRechargethroughEFT(CCMSRechargethroughEFTModel entity);
    }
}
