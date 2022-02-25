using HPCL.Common.Models.ViewModel.Officers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Interfaces
{
    public interface IOfficerServices
    {
        Task<List<OfficerListModel>> Details(string officerType, string location);
        Task<OfficerModel> Create();
        Task<string> Create(OfficerModel ofcrMdl);
        Task<OfficerModel> EditOfficer(int officerID);
        Task<string> EditOfficer(OfficerModel ofcrMdl);
        Task<OfficerLocationModel> EditLocation(int officerID);
        Task<Tuple<string, OfficerLocationModel>> EditLocation(OfficerLocationModel ofcrLocationMdl);
        Task<string> Delete(string officerID);
        Task<string> DeleteLocation(string userName, int zonalID, int regionalID);
        Task<OfficerDetailsModel> OfficerDetails();
        Task<List<OfficerDetailsTableModel>> GetOfficerDetailsTable(string zonalOfcID, string regionalOfcID, string stateID, string districtID);
    }
}
