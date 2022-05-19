using HPCL.Common.Models.RequestModel.Configure;
using HPCL.Common.Models.ViewModel.Configure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IConfigureService
    {
        Task<SMSAlertstoIndividualCardUsersModel> ConfCardPhone(SMSAlertstoIndividualCardUsersModel Model);
        Task<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse> DeleteSMSAlertsToIndividualCardUsers(string customerID, string cardNo, string mobileNo);
        Task<HPCL.Common.Models.ViewModel.Customer.UpdateKycResponse> SubmitSMSAlertstoIndividualCardUsers([FromBody] UpdateSMSAlertstoIndividualCardUsersRequest model);
    }
}
