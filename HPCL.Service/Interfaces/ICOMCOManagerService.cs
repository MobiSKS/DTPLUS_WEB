using HPCL.Common.Models;
using HPCL.Common.Models.RequestModel.COMCOManager;
using HPCL.Common.Models.ViewModel.COMCOManager;
using Microsoft.AspNetCore.Mvc;
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
        Task<CommonResponseData> UpdateCOMCOMapCustomer([FromBody] UpdateCOMCOMapCustomerRequest model);
        Task<ViewMappedCreditCustomersModel> ViewMappedCreditCustomers(ViewMappedCreditCustomersModel model);
        Task<RequestToSetCreditLimitModel> RequestToSetCreditLimit();
        Task<RequestToSetCreditLimitModel> RequestToSetCreditLimit(RequestToSetCreditLimitModel model);
        Task<RequestToSetCreditLimitModel> GetSetCreditLimitChequeDetailsPartialView([FromBody] List<ChequeDetails> arrs);
    }
}