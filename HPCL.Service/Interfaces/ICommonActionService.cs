using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ViewModel.Merchant;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.Officers;
using System.Collections.Generic;
using System.Threading.Tasks;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models;

namespace HPCL.Service.Interfaces
{
    public interface ICommonActionService
    {
        Task<List<LimitTypeModal>> GetLimitType();
        Task<List<OfficerTypeResponseModal>> GetOfficerTypeList();
        Task<List<StateResponseModal>> GetStateList();
        Task<List<DistrictResponseModal>> GetDistrictList(string stateId);
        Task<List<ZonalOfficeResponseModal>> GetZonalOfficeList();
        Task<List<RegionalOfficeResponseModal>> GetRegionalOfficeList(string zonalOfcID);
        Task<List<HqResponseModal>> GetHqList();
        Task<List<LocationMappingResponseModal>> GetLocationMappingList(int officerId);
        Task<List<MerchantTypeResponseModal>> GetMerchantTypeList();
        Task<List<OutletCategoryResponseModal>> GetOutletCategoryList();
        Task<List<SbuTypeResponseModal>> GetSbuTypeList();
        Task<List<SalesAreaResponseModal>> GetSalesAreaList(string regionId);
        Task<string> ValidateUserName(string userName);
        Task<List<RegionModel>> GetRegionList();
        Task<List<CustomerStateModel>> GetCustStateList();
        Task<CustomerInserCardResponseData> CheckformNumberDuplication(string FormNumber);
        Task<List<OfficerDistrictModel>> GetDistrictDetails(string Stateid);
        Task<CustomerInserCardResponseData> CheckPanNoDuplication(string PanNo);
        Task<CustomerInserCardResponseData> CheckMobilNoDuplication(string MobileNo);
        Task<CustomerInserCardResponseData> CheckEmailDuplication(string Emailid);
        Task<List<StatusResponseModal>> GetStatusType(string status);
        Task<string> PANValidation(string PANNumber);
        Task<List<CustomerRegionModel>> GetregionalOfficeList();
        Task<List<TerminalManagementCloseReasonModel>> GetTerminalRequestCloseReason();
        Task<string> CheckVehicleRegistrationValid(string RegistrationNumber);
        Task<MerchantDetailsResponseOTCCardCustomer> GetMerchantDetailsByMerchantId(string MerchantID);
        Task<List<ProofType>> GetAddressProofList();
        Task<CommonResponseData> VerifyMerchantByMerchantidAndRegionalid(string RegionalId, string MerchantID);
        Task<List<ProofType>> ProofType();
        Task<List<CustomerZonalOfficeModel>> GetZonalOfficeListForDropdown();
        Task<List<CustomerSecretQueModel>> GetCustomerSecretQuestionListForDropdown();
    }
}
