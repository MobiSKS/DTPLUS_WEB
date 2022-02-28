using HPCL.Common.Models.ViewModel.DriverCardCustomer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IDriverCardCustomerService
    {
        Task<RequestForDriverCardModel> RequestForDriverCard();
        Task<RequestForDriverCardModel> RequestForDriverCard(RequestForDriverCardModel requestForDriverCardModel);
        Task<DriverCardCustomerModel> CreateDriverCardCustomer();
    }
}
