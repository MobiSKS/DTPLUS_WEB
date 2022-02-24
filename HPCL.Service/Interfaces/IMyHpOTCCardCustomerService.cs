using HPCL.Common.Models.ViewModel.MyHpOTCCardCustomer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IMyHpOTCCardCustomerService
    {
        Task<RequestForOTCCardModel> RequestForOTCCard();
        Task<RequestForOTCCardModel> RequestForOTCCard(RequestForOTCCardModel requestForDriverCardModel);
    }
}
