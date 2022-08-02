using HPCL.Common.Models.ResponseModel.Customer;
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
        Task<UpdateKycResponse> AproveCustomer(string CustomerReferenceNo, string Comments, string Approvalstatus, string formNumber);
        Task<ManageAggregatorViewModel> UpdateAggregatorCustomer(string FormNumber);
        Task<ManageAggregatorViewModel> UpdateAggregatorCustomer(ManageAggregatorViewModel cust);
        Task<UploadDocResponseBody> UploadDoc(string FormNumber);
        Task<UploadDocResponse> UploadDoc(UploadDoc entity);
        Task<string> SaveUploadDoc(UploadDoc entity);
    }
}

