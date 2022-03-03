using HPCL.Common.Models.RequestModel.TerminalManagement;
using HPCL.Common.Models.ResponseModel.TerminalManagementResponse;
using HPCL.Common.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
  public  interface ITerminalManagement
    {
       Task<Tuple<List<ObjMerchantDetail>, List<ObjTerminalDetail>>> TerminalInstallationRequest(TerminalManagement entity);
        Task<string> AddJustification(TerminalManagement entity);
        Task<TerminalManagementRequestViewModel> TerminalInstallationRequestClose(TerminalManagementRequestViewModel terminalReq);
        Task<string> SubmitTerminalRequestClose([FromBody] TerminalManagementRequestModel TerminalManagementRequestModel);
        Task<TerminalManagementRequestViewModel> ViewTerminalInstallationRequestStatus(TerminalManagementRequestViewModel terminalReq);
    }
}
