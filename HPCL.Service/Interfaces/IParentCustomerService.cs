using HPCL.Common.Models.RequestModel.Customer;
using HPCL.Common.Models.ViewModel.ParentCustomer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IParentCustomerService
    {
        Task<ManageProfileViewModel> ManageProfile();
        Task<ManageProfileViewModel> ManageProfile(ManageProfileViewModel cust);
        Task<ParentCustomerApprovalModel> ApproveParentCustomer(ParentCustomerApprovalModel ApprovalMdl);
        Task<List<string>> ActionParentCustomerApproval([FromBody] ApproveParentCustomer approveParentCustomer);
        Task<ParentCustomerApprovalModel> AuthorizeParentCustomer(ParentCustomerApprovalModel ApprovalMdl);
        Task<List<string>> ActionParentCustomerAuthorize([FromBody] ApproveParentCustomer approveParentCustomer);
    }
}
