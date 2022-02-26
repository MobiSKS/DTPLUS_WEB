using HPCL.Common.Models.CommonEntity;
using HPCL_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface ICustomerManageService
    {
        Task<List<CustomerTypeModel>> GetCustomerType();
        Task<List<CustomerZonalOfficeModel>> GetZonalOffice();
        Task<List<CustomerTbentityModel>> GetCustomerTbentityModel();
        Task<List<CustomerStateModel>> GetCustomerState();
        Task<List<CustomerSecretQueModel>> GetCustomerSecretQue();
        Task<List<CustomerTypeOfFleetModel>> GetCustomerTypeofFlee();

        Task<List<CustomerProfileResponse>> BindCustomerDetails(String CustomerId);
        Task<List<SearchGridResponse>> CardDetails(String CustomerId);

    }
}
