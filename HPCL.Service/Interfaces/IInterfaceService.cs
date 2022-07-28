using HPCL.Common.Models.ResponseModel.Interface;
using HPCL.Common.Models.ViewModel.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IInterfaceService
    {
        Task<GetCustomerandCardFormResponse> GetCustomerandCardFormDetails(string EntityId, string FormNumber, string StateID, string CityName);
        Task<List<RegenerateIACResponseModel>> RegenerateIAC(string TerminalID);
    }
}
