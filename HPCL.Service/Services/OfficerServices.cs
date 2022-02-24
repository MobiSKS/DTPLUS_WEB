using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.CommonEntity.RequestEnities;
using HPCL.Common.Models.ViewModel.Officers;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HPCL.Common.Models.RequestModel.Officer;
using System.Linq;

namespace HPCL.Service.Services
{
    class OfficerServices : IOfficerServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;

        public OfficerServices(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }

        public async Task<OfficerModel> Create()
        {
            OfficerModel ofcrMdl = new OfficerModel();
            ofcrMdl.OfficerTypeMdl.AddRange((IEnumerable<OfficerTypeResponseModal>)_commonActionService.GetOfficerTypeList());
            ofcrMdl.OfficerStateMdl.AddRange((IEnumerable<StateResponseModal>)_commonActionService.GetStateList());
            return ofcrMdl;
        }

        public async Task<string> Create(OfficerModel ofcrMdl)
        {
            var officerInsertForms = new OfficerInsertRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid,
                FirstName = ofcrMdl.FirstName,
                LastName = ofcrMdl.LastName,
                UserName = ofcrMdl.UserName,
                OfficerType = ofcrMdl.OfficerTypeID,
                LocationId = ofcrMdl.LocationID,
                Address1 = ofcrMdl.Address1,
                Address2 = string.IsNullOrEmpty(ofcrMdl.Address2) ? "" : ofcrMdl.Address2,
                Address3 = string.IsNullOrEmpty(ofcrMdl.Address3) ? "" : ofcrMdl.Address3,
                StateId = ofcrMdl.State,
                CityName = string.IsNullOrEmpty(ofcrMdl.City) ? "" : ofcrMdl.City,
                DistrictId = string.IsNullOrEmpty(ofcrMdl.DistrictID) ? "0" : ofcrMdl.DistrictID,
                Pin = string.IsNullOrEmpty(ofcrMdl.Pin) ? "" : ofcrMdl.Pin,
                MobileNo = string.IsNullOrEmpty(ofcrMdl.Mobile) ? "" : ofcrMdl.Mobile,
                PhoneNo = string.IsNullOrEmpty(ofcrMdl.Phone) ? "" : ofcrMdl.Phone,
                EmailId = string.IsNullOrEmpty(ofcrMdl.Email) ? "" : ofcrMdl.Email,
                Fax = string.IsNullOrEmpty(ofcrMdl.Fax) ? "" : ofcrMdl.Fax,
                Createdby = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };

            StringContent officerInsertContent = new StringContent(JsonConvert.SerializeObject(officerInsertForms), Encoding.UTF8, "application/json");

            string url;

            if (ofcrMdl.OfficerTypeName == "RBE")
            {
                url = WebApiUrl.insertRbeOfficer;
            }
            else
            {
                url = WebApiUrl.insertOfficer;
            }

            var officerInsertResponse = await _requestService.CommonRequestService(officerInsertContent, url);
            JObject officerInsertObj = JObject.Parse(JsonConvert.DeserializeObject(officerInsertResponse).ToString());

            if (officerInsertObj["Status_Code"].ToString() == "200")
            {
                var officerInsertJarr = officerInsertObj["Data"].Value<JArray>();
                List<SuccessResponse> officerInsertLst = officerInsertJarr.ToObject<List<SuccessResponse>>();
                return officerInsertLst.First().Reason.ToString();
            }
            else
            {
                return officerInsertObj["Message"].ToString();
            }
        }
        public async Task<string> Delete(string officerID)
        {
            var officerDeleteForms = new DeleteOfficerRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                OfficerID = officerID,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };

            StringContent officerDeleteContent = new StringContent(JsonConvert.SerializeObject(officerDeleteForms), Encoding.UTF8, "application/json");

            var officerDeleteResponse = await _requestService.CommonRequestService(officerDeleteContent, WebApiUrl.deleteOfficer);

            JObject officerDeleteObj = JObject.Parse(JsonConvert.DeserializeObject(officerDeleteResponse).ToString());

            if (officerDeleteObj["Status_Code"].ToString() == "200")
            {
                var officerDeleteJarr = officerDeleteObj["Data"].Value<JArray>();
                List<SuccessResponse> officerDeleteLst = officerDeleteJarr.ToObject<List<SuccessResponse>>();
                return officerDeleteLst.First().Reason.ToString();
            }
            else
            {
                return officerDeleteObj["Message"].ToString();
            }
        }

        public async Task<string> DeleteLocation(string userName, int zonalID, int regionalID)
        {
            var officerDeleteLocationForms = new DeleteOfficerLocationRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserName = userName,
                ZO = zonalID,
                RO = regionalID,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };

            StringContent officerDeleteLocationContent = new StringContent(JsonConvert.SerializeObject(officerDeleteLocationForms), Encoding.UTF8, "application/json");

            var officerDeleteLocationResponse = await _requestService.CommonRequestService(officerDeleteLocationContent, WebApiUrl.deleteOfficerLocationMapping);

            JObject officerDeleteLocationObj = JObject.Parse(JsonConvert.DeserializeObject(officerDeleteLocationResponse).ToString());
            
            if (officerDeleteLocationObj["Status_Code"].ToString() == "200")
            {
                var officerDeleteLocationJarr = officerDeleteLocationObj["Data"].Value<JArray>();
                List<SuccessResponse> officerDeleteLocationLst = officerDeleteLocationJarr.ToObject<List<SuccessResponse>>();
                return officerDeleteLocationLst.First().Reason.ToString();
            }
            else
            {
                return officerDeleteLocationObj["Message"].ToString();
            }
        }

        public async Task<List<OfficerListModel>> Details(string officerType, string location)
        {
            List<OfficerListModel> ofcLstMdl = new List<OfficerListModel>();

            var officerGetDetailsForms = new OfficerDetailsRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid,
                OfficerType = String.IsNullOrEmpty(officerType) ? "0" : officerType,
                Location = String.IsNullOrEmpty(location) ? "0" : location
            };

            StringContent officerGetDetailsContent = new StringContent(JsonConvert.SerializeObject(officerGetDetailsForms), Encoding.UTF8, "application/json");

            var officerGetDetailsResponse = await _requestService.CommonRequestService(officerGetDetailsContent, WebApiUrl.GetStatusTypeUrl);

            JObject officerGetDetailsObj = JObject.Parse(JsonConvert.DeserializeObject(officerGetDetailsResponse).ToString());
            var officerGetDetailsJarr = officerGetDetailsObj["Data"].Value<JArray>();
            List<OfficerListModel> officerGetDetailsLst = officerGetDetailsJarr.ToObject<List<OfficerListModel>>();
            ofcLstMdl.AddRange(officerGetDetailsLst);

            return ofcLstMdl;
        }

        public async Task<OfficerLocationModel> EditLocation(int officerID)
        {
            OfficerLocationModel ofcrLocMdl = new OfficerLocationModel();
            ofcrLocMdl.ZoneOffices.AddRange((IEnumerable<ZonalOfficeResponseModal>)_commonActionService.GetZonalOfficeList());
            ofcrLocMdl.LocationMappings.AddRange((IEnumerable<LocationMappingResponseModal>)_commonActionService.GetLocationMappingList(officerID));
            return ofcrLocMdl;
        }
        public async Task<Tuple<string, OfficerLocationModel>> EditLocation(OfficerLocationModel ofcrLocationMdl)
        {
            var officerLocationMappingInsertForms = new InsertOfficerLocationMappingRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                OfficerId = ofcrLocationMdl.OfficerID,
                UserName = ofcrLocationMdl.UserName,
                ZO = ofcrLocationMdl.ZoneOfcID,
                RO = ofcrLocationMdl.RegionalOfcID,
                Createdby = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };

            StringContent officerLocationMappingInsertContent = new StringContent(JsonConvert.SerializeObject(officerLocationMappingInsertForms), Encoding.UTF8, "application/json");

            var officerLocationMappingInsertResponse = await _requestService.CommonRequestService(officerLocationMappingInsertContent, WebApiUrl.insertOfficerLocationMapping);
            JObject officerLocationMappingInsertObj = JObject.Parse(JsonConvert.DeserializeObject(officerLocationMappingInsertResponse).ToString());

            ofcrLocationMdl.ZoneOffices.AddRange((IEnumerable<ZonalOfficeResponseModal>)_commonActionService.GetZonalOfficeList());
            ofcrLocationMdl.LocationMappings.AddRange((IEnumerable<LocationMappingResponseModal>)_commonActionService.GetLocationMappingList(ofcrLocationMdl.OfficerID));

            if (officerLocationMappingInsertObj["Status_Code"].ToString() == "200")
            {
                var officerLocationMappingInsertJarr = officerLocationMappingInsertObj["Data"].Value<JArray>();
                List<SuccessResponse> officerLocationMappingInsertLst = officerLocationMappingInsertJarr.ToObject<List<SuccessResponse>>();
                return Tuple.Create(officerLocationMappingInsertLst.First().Reason.ToString(), ofcrLocationMdl);
            }
            else
            {
                return Tuple.Create(officerLocationMappingInsertObj["Message"].ToString(), ofcrLocationMdl);
            }
        }

        public async Task<OfficerModel> EditOfficer(int officerID)
        {
            OfficerModel ofcrEditMdl = new OfficerModel();
            ofcrEditMdl.OfficerID = officerID;

            var officerEditForms = new OfficerEditRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid,
                OfficerID = officerID
            };

            StringContent officerEditContent = new StringContent(JsonConvert.SerializeObject(officerEditForms), Encoding.UTF8, "application/json");

            var officerEditResponse = await _requestService.CommonRequestService(officerEditContent, WebApiUrl.GetStatusTypeUrl);

            JObject officerEditObj = JObject.Parse(JsonConvert.DeserializeObject(officerEditResponse).ToString());
            var officerEditJarr = officerEditObj["Data"].Value<JArray>();
            List<OfficerModel> ofcrEditLst = officerEditJarr.ToObject<List<OfficerModel>>();
            ofcrEditMdl = ofcrEditLst.First();
            ofcrEditMdl.State = ofcrEditMdl.StateId;
            ofcrEditMdl.Mobile = ofcrEditMdl.MobileNo;
            ofcrEditMdl.Email = ofcrEditMdl.EmailId;

            ofcrEditMdl.OfficerTypeMdl.AddRange((IEnumerable<OfficerTypeResponseModal>)_commonActionService.GetOfficerTypeList());
            ofcrEditMdl.OfficerStateMdl.AddRange((IEnumerable<StateResponseModal>)_commonActionService.GetStateList());
            ofcrEditMdl.OfficerDistrictMdl.AddRange((IEnumerable<DistrictResponseModal>)_commonActionService.GetDistrictList(""));

            return ofcrEditMdl;
        }

        public async Task<string> EditOfficer(OfficerModel ofcrEditUpdateMdl)
        {
            var editOfficerUpdateForms = new OfficerEditUpdateRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid,
                FirstName = ofcrEditUpdateMdl.FirstName,
                LastName = ofcrEditUpdateMdl.LastName,
                Address1 = ofcrEditUpdateMdl.Address1,
                Address2 = ofcrEditUpdateMdl.Address2,
                Address3 = ofcrEditUpdateMdl.Address3,
                StateId = ofcrEditUpdateMdl.State.ToString(),
                CityName = ofcrEditUpdateMdl.City,
                DistrictId = ofcrEditUpdateMdl.DistrictID.ToString(),
                Pin = ofcrEditUpdateMdl.Pin.ToString(),
                MobileNo = ofcrEditUpdateMdl.Mobile,
                PhoneNo = ofcrEditUpdateMdl.Phone,
                EmailId = ofcrEditUpdateMdl.Email,
                Fax = ofcrEditUpdateMdl.Fax,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                OfficerId = ofcrEditUpdateMdl.OfficerID
            };

            StringContent editOfficerUpdateContent = new StringContent(JsonConvert.SerializeObject(editOfficerUpdateForms), Encoding.UTF8, "application/json");

            var editOfficerUpdateResponse = await _requestService.CommonRequestService(editOfficerUpdateContent, WebApiUrl.updateOfficer);

            JObject editOfficerUpdateObj = JObject.Parse(JsonConvert.DeserializeObject(editOfficerUpdateResponse).ToString());

            if (editOfficerUpdateObj["Status_Code"].ToString() == "200")
            {
                var editOfficerUpdateJarr = editOfficerUpdateObj["Data"].Value<JArray>();
                List<SuccessResponse> editOfficerUpdateLst = editOfficerUpdateJarr.ToObject<List<SuccessResponse>>();
                return editOfficerUpdateLst.First().Reason.ToString();
            }
            else
            {
                return editOfficerUpdateObj["Message"].ToString();
            }
        }

        public Task<string> GetDistrictDetails(string stateid)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetLocationDetails(string ofcrType)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OfficerDetailsTableModel>> GetOfficerDetailsTable(string zonalOfcID, string regionalOfcID, string stateID, string districtID)
        {
            var officerDetailsTableDataForms = new GetOfficerDetailsRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid,
                ZO = zonalOfcID,
                RO = regionalOfcID,
                StateId = stateID,
                DistrictId = districtID
            };

            StringContent officerDetailsTableDataContent = new StringContent(JsonConvert.SerializeObject(officerDetailsTableDataForms), Encoding.UTF8, "application/json");

            var officerDetailsTableDataResponse = await _requestService.CommonRequestService(officerDetailsTableDataContent, WebApiUrl.GetStatusTypeUrl);

            JObject officerDetailsTableDataObj = JObject.Parse(JsonConvert.DeserializeObject(officerDetailsTableDataResponse).ToString());
            var officerDetailsTableDataJarr = officerDetailsTableDataObj["Data"].Value<JArray>();
            List<OfficerDetailsTableModel> officerDetailsTableDataLst = officerDetailsTableDataJarr.ToObject<List<OfficerDetailsTableModel>>();

            return officerDetailsTableDataLst;
        }

        public Task<string> GetOfficerTypeDetails()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRegionalOfcDetails(string zonalOfcID)
        {
            throw new NotImplementedException();
        }

        public async Task<OfficerDetailsModel> OfficerDetails()
        {
            OfficerDetailsModel ofcLstMdl = new OfficerDetailsModel();

            ofcLstMdl.OfficerStates.AddRange((IEnumerable<StateResponseModal>)_commonActionService.GetStateList());
            ofcLstMdl.OfficerZones.AddRange((IEnumerable<ZonalOfficeResponseModal>)_commonActionService.GetZonalOfficeList());
            
            return ofcLstMdl;
        }
        public Task<string> ValidateUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
