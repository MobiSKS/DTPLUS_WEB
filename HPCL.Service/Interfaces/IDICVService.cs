using HPCL.Common.Models;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.RequestModel.DICV;
using HPCL.Common.Models.ResponseModel.AshokLayland;
using HPCL.Common.Models.ResponseModel.DICV;
using HPCL.Common.Models.ResponseModel.VolvoEicher;
using HPCL.Common.Models.ViewModel.DICV;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IDICVService
    {
        Task<List<OfficerTypeResponseModal>> GetDICVOfficerTypeList();
        Task<DICVDealerEnrollmentModel> DICVDealerEnrollment();
        Task<InsertResponse> InsertDICVDealerEnrollment(string str);
        Task<SearchAlResult> SearchDICVDealer(string dealerCode, string dtpCode, string OfficerType);
        Task<InsertResponse> DICVDealerEnrollmentUpdate(string getAllData);
        Task<CommonResponseData> CheckDICVDealerCodeExists(string DealerCode);
        Task<DICVOTCCardRequestModel> DICVDealerOTCCardRequest();
        Task<GetDICVBalanceOTCCardResponse> CheckDICVDealerBalanceQty(string DealerCode);
        Task<DICVOTCCardRequestModel> DICVDealerOTCCardRequest(DICVOTCCardRequestModel model);
        Task<DICVCustomerEnrollmentModel> DICVCustomerEnrollment();
        Task<DICVCustomerEnrollmentModel> DICVCustomerEnrollment(DICVCustomerEnrollmentModel customerModel);
        Task<DICVCustomerEnrollmentModel> GetDICVCustomerOTCCardPartialView([FromBody] List<DICVCardEntryDetails> arrs);
        Task<List<DICVOTCCardDetails>> GetAvailableDICVOTCCardForDealer(string DealerCode);
        Task<GetSalesExecutiveEmployeeIDResponse> GetDICVSalesExeEmpId(string dealerCode);
        Task<DICVHotlistorReactivateViewModel> DICVHotlistAndReactivate();
        Task<List<DICVHotlistReason>> GetReasonListForEntities(string EntityTypeId);
        Task<List<string>> ApplyHotlistorReactivate([FromBody] DICVHotlistorReactivateViewModel hotlistorReactivateViewModel);
        Task<GetDICVCommunicationEmailResetPasswordResponse> GetDICVCommunicationEmailResetPassword(string CustomerId);
        Task<InsertResponse> UpdateDICVCommunicationEmailResetPassword(string CustomerId, string AlternateEmailId);
        Task<DICVViewCardSearch> SearchCardMapping(DICVViewCardDetails viewCardDetails);
        Task<List<string>> UpdateCards(DICVUpdateMobileandFastagNoInCard[] limitArray);
        Task<DICVViewCardSearch> AddCardMappingDetails(DICVViewCardDetails viewCardDetails);
    }
}