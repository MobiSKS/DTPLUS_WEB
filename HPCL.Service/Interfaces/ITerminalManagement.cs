using HPCL.Common.Models.RequestModel.TerminalManagement;
using HPCL.Common.Models.ResponseModel.TerminalManagementResponse;
using HPCL.Common.Models.ViewModel;
using HPCL.Common.Models.ViewModel.Merchant;
using HPCL.Common.Models.ViewModel.Terminal;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public  interface ITerminalManagement
    {
        Task<TerminalInstallationRequestResponse> TerminalInstallationRequest([FromBody] TerminalManagement entity);
        Task<List<string>> AddJustification(string objInsertTerminal);
        Task<TerminalManagementRequestViewModel> TerminalInstallationRequestClose(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId, string SBUTypeId);
        Task<List<string>> SubmitTerminalRequestClose([FromBody] TerminalManagementRequestModel TerminalManagementRequestModel);
        Task<TerminalManagementRequestViewModel> ViewTerminalInstallationRequestStatus(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId, string SBUTypeId);
        Task<TerminalApprovalReqResponse> GetTerminalInstallationReqApproval(TerminalApprovalReq entity);
        Task<List<string>> DoApprovalTerminal(string ObjMerchantTerminalInsertInput, string remark);
        Task<List<string>> DoRejectTerminal(string ObjMerchantTerminalInsertInput, string remark);
        Task<TerminalDeinstallationRequestViewModel> TerminalDeInstallationRequest(TerminalDeinstallationRequestViewModel terminalReq);
        Task<List<string>> SubmitDeinstallRequest([FromBody] TerminalDeinstallationRequestUpdateModel TerminalDeinstallationRequestUpdate);
        Task<TerminalDeinstallationRequestViewModel> TerminalDeInstallationRequestClose(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId, string SBUTypeId);
        Task<List<string>> SubmitDeinstallationRequestClose([FromBody] TerminalDeinstallationCloseModel TerminalDeinstallationClose);
        Task<TerminalDeinstallationRequestViewModel> ViewTerminalDeinstallationRequestStatus(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId, string SBUTypeId);
        Task<TerminalDeinstallationRequestViewModel> ProblematicDeInstalledToDeInstalled(string ZonalOfficeId, string RegionalOfficeId, string FromDate, string ToDate, string MerchantId, string TerminalId, string SBUTypeId);
        Task<List<string>> SubmitProblematicDeinstalltoDeinstall([FromBody] TerminalDeinstallationCloseModel TerminalDeinstallationClose);
        Task<SearchTerminalModel> SearchTerminal();
        Task<List<SearchTerminalDetailsResponseModal>> SearchTerminalDetails(string terminalId, string merchantId, string terminalType, string issueDate);
        Task<ManageTerminalModel> ManageTerminal();
        Task<ManageTerminalResponse> GetAllStatusValue(string MerchantId, string TerminalId, string Status);
        Task<MerchantModel> GetMerchantSummaryData(string ERPCode);
    }
}
