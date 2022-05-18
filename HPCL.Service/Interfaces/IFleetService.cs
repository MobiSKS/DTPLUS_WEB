using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.Aggregator;
using HPCL.Common.Models.ViewModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IFleetService
    {
        Task<ManageAggregatorViewModel> ManageFleetCustomer(string FromDate, string ToDate);
        Task<ManageAggregatorViewModel> ManageFleetCustomer(ManageAggregatorViewModel cust);
        Task<UploadDocResponseBody> UploadDoc(string FormNumber);
        Task<UploadDocResponse> UploadDoc(UploadDoc entity);
        Task<string> SaveUploadDoc(UploadDoc entity);
    }
}
