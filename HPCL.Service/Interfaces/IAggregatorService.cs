using HPCL.Common.Models.ViewModel.Aggregator;
using HPCL.Common.Models.ViewModel.Customer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IAggregatorService
    {

        Task<ManageAggregatorViewModel> ManageAggregator(string FromDate, string ToDate);
        Task<ManageAggregatorViewModel> ManageAggregator(ManageAggregatorViewModel cust);
        Task<ValidateAggregatorCustomerModel> ValidateAggregatorCustomer(ValidateAggregatorCustomerModel entity);
        Task<JObject> ViewCustomerDetails(string FormNumber);
        Task<UpdateKycResponse> AproveCustomer(string CustomerReferenceNo, string Comments, string Approvalstatus);
    }
}

