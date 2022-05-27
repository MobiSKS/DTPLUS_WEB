using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ViewModel.Merchant;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.Officers;
using System.Collections.Generic;
using System.Threading.Tasks;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models;
using HPCL.Common.Models.CommonEntity.RequestEnities;

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
        Task<List<CustomerRegionModel>> GetRegionalDetailsDropdown(int ZonalOfficeID);
        Task<List<TerminalStatusResponseModal>> GetMerchantStatus();
        Task<CommonResponseData> CheckDealerCodeIsValid(string DealerCode);
        Task<List<VehicleTypeModel>> GetVehicleTypeDropdown();
        Task<CommonResponseData> CheckPanCardDuplicationByDistrictid(string DistrictId, string IncomeTaxPan);
        Task<List<CustomerTypeModel>> GetCustomerTypeListDropdown();
        Task<List<CustomerTbentityModel>> GetCustomerTbentityListDropdown();
        Task<List<CustomerTypeOfFleetModel>> GetCustomerTypeOfFleetDropdown();
        Task<List<CustomerSubTypeModel>> GetCustomerSubTypeDropdown();
        Task<List<SalesAreaModel>> GetSalesAreaDropdown(string RegionID);
        Task<List<CustomerSubTypeModel>> GetCustomerSubTypeDropdown(int CustomerTypeID);
        Task<List<TransactionTypeResponse>> GetTransactionTypeDropdown();
        Task<CommonResponseData> CheckPanCardDuplicationByDistrictidForCustomerUpdate(string DistrictId, string IncomeTaxPan, string CustomerReferenceNo);
        Task<List<GetCountryRegionResponse>> GetCountryRegion();
        Task<List<GetCityResponse>> GetCity();
        Task<CustomerInserCardResponseData> CheckVechileNoUsed(string VechileNo);
        Task<List<HotlistStatus>> GetActionList(string EntityTypeId);
        Task<List<HotlistEntity>> GetEntityTypeList();
        Task<List<HotlistReason>> GetReasonListForEntities(string EntityTypeId, string Actionid);
        Task<List<RecordTypeModel>> GetRecordType();
        Task<string> changeDateFormat(string date);
        Task<string> ValidateErpCode(string erpCode);
        Task<string> ValidateMappedMerchantId(string mappedMerchantId);
        Task<List<RbeMappingStatusResponse>> RbeMappingStatusService();
        Task<List<StatusModal>> GetStatusTypeforTerminal();
        Task<List<StatusResponseModal>> GetVehicleEnrollmentStatusList();
        Task<List<GetEntityModel>> GetEntityModelList();
        Task<List<GetEntityFieldModel>> GetEntityFieldModelList(string EntityTypeId);
        Task<List<StatusResponseModal>> GetStatusType(int EntityTypeId);
        Task<List<CheckPancardbyDistrictIdResponse>> CheckPanCardDuplicationByDistrictidWithListOfCustomers(string DistrictId, string IncomeTaxPan);
    }
}