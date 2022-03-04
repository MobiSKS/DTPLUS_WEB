using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.TatkalCardCustomer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ITatkalCardCustomerService
    {
        Task<TatkalCustomerCardRequestInfo> RequestForTatkalCard();
        Task<TatkalCustomerCardRequestInfo> RequestForTatkalCard(TatkalCustomerCardRequestInfo tatkalCustomerCardRequestInfo);
        Task<TatkalCardCustomerModel> CreateTatkalCustomer(TatkalCardCustomerModel custModel);
    }
}
