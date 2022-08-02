using HPCL.Common.Models.CommonEntity;
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
        Task<List<SuccessResponse>> CCMSRechargethroughEFT(CCMSRechargethroughEFTModel entity);
        Task<CCMSRechargeSummaryViewModel> EFTRechargeDetailValidation(CCMSRechargethroughEFTModel entity);
    }
}
