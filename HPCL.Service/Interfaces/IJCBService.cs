using HPCL.Common.Models;
using HPCL.Common.Models.ResponseModel.AshokLayland;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IJCBService
    {
        Task<CommonResponseData> CheckJCBDealerCodeExists(string DealerCode);
        Task<InsertResponse> InsertJCBDealerEnrollment(string str);
        Task<InsertResponse> JCBDealerEnrollmentUpdate(string getAllData);
        Task<SearchAlResult> SearchJCBDealer(string dealerCode, string dtpCode, string OfficerType);
    }
}