using HPCL.Common.Models.RequestModel.TerminalManagement;
using HPCL.Common.Models.ResponseModel.TerminalManagementResponse;
using HPCL.Common.Models.ViewModel;
using HPCL.Common.Models.ViewModel.Terminal;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public  interface ITerminalManagement
    {
        Task<TerminalInstallationRequestResponse> TerminalInstallationRequest(TerminalManagement entity);
        Task<string> AddJustification(string objInsertTerminal);
        Task<TerminalManagementRequestViewModel> TerminalInstallationRequestClose(TerminalManagementRequestViewModel terminalReq);
        Task<string> SubmitTerminalRequestClose([FromBody] TerminalManagementRequestModel TerminalManagementRequestModel);
        Task<TerminalManagementRequestViewModel> ViewTerminalInstallationRequestStatus(TerminalManagementRequestViewModel terminalReq);
        Task<TerminalApprovalReqResponse> GetTerminalInstallationReqApproval(TerminalApprovalReq entity);
        Task<string> DoApprovalTerminal(string ObjMerchantTerminalInsertInput, string remark);
        Task<string> DoRejectTerminal(string ObjMerchantTerminalInsertInput, string remark);
        Task<TerminalDeinstallationRequestViewModel> TerminalDeInstallationRequest(TerminalDeinstallationRequestViewModel terminalReq);
        Task<string> SubmitDeinstallRequest([FromBody] TerminalDeinstallationRequestUpdateModel TerminalDeinstallationRequestUpdate);
        Task<TerminalDeinstallationRequestViewModel> TerminalDeInstallationRequestClose(TerminalDeinstallationRequestViewModel terminalReq);
        Task<string> SubmitDeinstallationRequestClose([FromBody] TerminalDeinstallationCloseModel TerminalDeinstallationClose);
        Task<TerminalDeinstallationRequestViewModel> ViewTerminalDeinstallationRequestStatus(TerminalDeinstallationRequestViewModel terminalReq);
        Task<TerminalDeinstallationRequestViewModel> ProblematicDeInstalledToDeInstalled(TerminalDeinstallationRequestViewModel terminalReq);
        Task<string> SubmitProblematicDeinstalltoDeinstall([FromBody] TerminalDeinstallationCloseModel TerminalDeinstallationClose);
        Task<SearchTerminalModel> SearchTerminal();
        Task<List<SearchTerminalDetailsResponseModal>> SearchTerminalDetails(string terminalId, string merchantId, string terminalType, string issueDate);
        Task<ManageTerminalModel> ManageTerminal();
        Task<List<ManageTerminalResponse>> GetAllStatusValue(ManageTerminalRequest entity);
    }
}
