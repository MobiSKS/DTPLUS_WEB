using HPCL.Common.Models.ViewModel.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;
using HPCL.Common.Models;
using HPCL.Common.Models.ViewModel.Officers;
using Newtonsoft.Json.Linq;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Customer;
using Microsoft.AspNetCore.Mvc;
using HPCL.Common.Models.RequestModel.Customer;

namespace HPCL.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<UploadDocResponse> UploadDoc(UploadDoc entity);
        Task<string> SaveUploadDoc(UploadDoc entity);
        Task<CustomerModel> OnlineForm();
        Task<CustomerModel> OnlineForm(CustomerModel cust);
        Task<List<CustomerTypeModel>> GetCustomerType();
        Task<List<VehicleTypeModel>> GetVehicleTypeDetails();
        Task<CustomerCardInfo> AddCardDetails(string customerReferenceNo);
        Task<CustomerCardInfo> GetCustomerDetails(string customerReferenceNo);
        Task<CustomerCardInfo> GetCustomerRBEName(string RBEId);
        Task<CustomerCardInfo> AddCardDetails(CustomerCardInfo customerCardInfo);
        Task<CustomerModel> ValidateNewCustomer();
        Task<CustomerValidate> ValidateNewCustomer(CustomerValidate entity);
        Task<List<SearchCustomerResponseGrid>> ReloadUpdatedGrid();
        Task<JObject> ViewCustomerDetails(string FormNumber);
        Task<UpdateKycResponse> AproveCustomer(string CustomerReferenceNo, string Comments, string Approvalstatus);
        Task<List<SalesAreaModel>> GetsalesAreaDetails(int RegionID);
        Task<CustomerBalanceInfoModel> BalanceInfo();
        Task<GetCustomerBalanceResponse> GetCustomerBalanceInfo(string CustomerID);
        Task<GetCustomerCardWiseBalanceResponse> GetCustomerCardWiseBalance(string CustomerID);
        Task<JObject> GetCustomerDetailsByCustomerID(string CustomerID);
        Task<CustomerCCMSBalanceModel> GetCCMSBalanceDetails(string CustomerID);
        Task<string> GenerateFormNumber();
        Task<CustomerModel> UpdateCustomer(string FormNumber);
        Task<CustomerModel> UpdateCustomer(CustomerModel cust);
        Task<UploadDocResponseBody> UploadDoc(string FormNumber);
        Task<CustomerCardInfo> GetCustomerDetailsForAddCard(string customerReferenceNo);
        Task<CustomerCardInfo> GetAddCardPaymentDetailsPartialView([FromBody] List<CardDetails> arrs);
        Task<CustomerCardInfo> GetCustomerAddCardsPartialView([FromBody] List<CardDetails> arrs);
        Task<UpdateCustomerAddress> GetCustomerAddress(string CustomerId);
        Task<UpdateCustomerAddress> UpdateCustomerAddress();
        Task<UpdateCustomerAddress> UpdateCustomerAddress(UpdateCustomerAddress model);
        Task<UpdateContactPersonDetailsModel> UpdateContactPersonDetails();
        Task<UpdateContactPersonResponseDetails> GetUpdateContactPersonDetails(string CustomerId);
        Task<UpdateContactPersonDetailsModel> UpdateContactPersonDetails(UpdateContactPersonDetailsModel model);
        Task<LowCCMSBalanceAlertConfigurationModel> CCMSBalanceAlert();
        Task<LowCCMSBalanceAlertConfigurationModel> GetCCMSBalAlertConfiguration(string CustomerID);
        Task<UpdateKycResponse> UpdateCCMSBalAlertConfiguration(string CustomerID, string Amount, string ActionType);
        Task<CustomerAddressApproveRequestModel> ApprovalUpdateCustomerAddress(CustomerAddressApproveRequestModel model);
        Task<CommonResponseData> ApproveCustomerAddressRequests([FromBody] ApproveCustomerAddressRequest model);
        Task<CustomerAddressApproveRequestModel> ApprovalUpdateCustomerContactPerson(CustomerAddressApproveRequestModel model);
        Task<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse> ApproveCustomerContactPersonRequests([FromBody] ApproveCustomerContactPersonRequest model);
        Task<GetCustomerAddressRequestForApproval> GetCustomerOldAndNewAddressList(string CustomerId);
        Task<GetCustomerContactPersonRequestForApproval> GetCustomerOldAndNewContactPersonList(string CustomerId);
    }
}