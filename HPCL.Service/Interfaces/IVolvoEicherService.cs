using HPCL.Common.Models.ResponseModel.CustomerManage;
using HPCL.Common.Models.ViewModel.VolvoEicher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IVolvoEicherService
    {
        Task<VEDealerOTCCardStatusViewModel> GetVEDealerCardStatus(string DealerCode);
        Task<List<VECustomerProfileResponse>> BindCustomerDetails(string CustomerId, string NameOnCard);
    }
}
