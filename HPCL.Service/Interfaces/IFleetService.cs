using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.Aggregator;
using HPCL.Common.Models.ViewModel.Customer;
using Microsoft.AspNetCore.Mvc;
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
        Task<CustomerCardInfo> AddCardDetails( CustomerCardInfo customerCardInfo);
        Task<CustomerCardInfo> AddCardDetails(string customerReferenceNo);
        Task<CustomerCardInfo> GetCustomerDetailsForAddCard(string customerReferenceNo);
        Task<CustomerCardInfo> GetCustomerAddCardsPartialView([FromBody] List<CardDetails> arrs);
    }
}
