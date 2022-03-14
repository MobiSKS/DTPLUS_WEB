using HPCL.Common.Models.ViewModel.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;
using HPCL.Common.Models;
using HPCL.Common.Models.ViewModel.Officers;
using Newtonsoft.Json.Linq;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.Customer;

namespace HPCL.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<List<UploadDocResponse>> UploadDoc(UploadDoc entity);
        Task<string> SaveUploadDoc(UploadDoc entity);
        Task<CustomerModel> OnlineForm();
        Task<CustomerModel> OnlineForm(CustomerModel cust);
        Task<List<CustomerTypeModel>> GetCustomerType();
        Task<List<CustomerSubTypeModel>> GetCustomerSubType(int CustomerTypeID);
        Task<List<OfficerDistrictModel>> GetDistrictDetails(string Stateid);
        Task<List<VehicleTypeModel>> GetVehicleTypeDetails();
        Task<CustomerCardInfo> AddCardDetails(string customerReferenceNo);
        Task<CustomerCardInfo> GetCustomerDetails(string customerReferenceNo);
        Task<CustomerCardInfo> GetCustomerRBEName(string RBEId);
        Task<CustomerCardInfo> AddCardDetails(CustomerCardInfo customerCardInfo);
        Task<CustomerModel> ValidateNewCustomer();
        Task<List<SearchCustomerResponseGrid>> ValidateNewCustomer(CustomerModel entity);
        Task<List<SearchCustomerResponseGrid>> ReloadUpdatedGrid();
        Task<JObject> ViewCustomerDetails(string FormNumber);
        Task<UpdateKycResponse> AproveCustomer(string CustomerReferenceNo, string Comments, string Approvalstatus);
        Task<List<SalesAreaModel>> GetsalesAreaDetails(int RegionID);
        Task<CustomerInserCardResponseData> CheckformnumberDuplication(string FormNumber);
        Task<CustomerInserCardResponseData> CheckMobilNoDuplication(string MobileNo);
        Task<CustomerBalanceInfoModel> BalanceInfo();
        Task<GetCustomerBalanceResponse> GetCustomerBalanceInfo(string CustomerID);
        Task<GetCustomerCardWiseBalanceResponse> GetCustomerCardWiseBalance(string CustomerID);
        Task<JObject> GetCustomerDetailsByCustomerID(string CustomerID);
        Task<CustomerCCMSBalanceModel> GetCCMSBalanceDetails(string CustomerID);
    }
}