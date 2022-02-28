﻿using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.RequestEnities;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.Officers;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class CommonActionService : ICommonActionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public CommonActionService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<List<DistrictResponseModal>> GetDistrictList(string stateId)
        {
            var officerDistrictForms = new DistrictRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid,
                StateID = stateId
            };

            StringContent officerDistrictContent = new StringContent(JsonConvert.SerializeObject(officerDistrictForms), Encoding.UTF8, "application/json");

            var officerDistrictResponse = await _requestService.CommonRequestService(officerDistrictContent, WebApiUrl.getDistrict);

            JObject officerDistrictObj = JObject.Parse(JsonConvert.DeserializeObject(officerDistrictResponse).ToString());
            var officerDistrictJarr = officerDistrictObj["Data"].Value<JArray>();
            List<DistrictResponseModal> districtLst = officerDistrictJarr.ToObject<List<DistrictResponseModal>>();

            return districtLst;
        }

        public async Task<List<HqResponseModal>> GetHqList()
        {
            var hqForms = new BaseEntity
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid
            };

            StringContent hqContent = new StringContent(JsonConvert.SerializeObject(hqForms), Encoding.UTF8, "application/json");

            var hqResponse = await _requestService.CommonRequestService(hqContent, WebApiUrl.getLocationHq);

            JObject hqObj = JObject.Parse(JsonConvert.DeserializeObject(hqResponse).ToString());
            var hqJarr = hqObj["Data"].Value<JArray>();
            List<HqResponseModal> hqLst = hqJarr.ToObject<List<HqResponseModal>>();

            return hqLst;
        }

        public async Task<List<LimitTypeModal>> GetLimitType()
        {
            var forms = new Dictionary<string, string>
            {
                {"useragent", CommonBase.useragent},
                {"userip", CommonBase.userip},
                {"userid", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
            };


            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetLimitTypeUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<LimitTypeModal> limitType = jarr.ToObject<List<LimitTypeModal>>();
            var sortedtList = limitType.OrderBy(x => x.LimitId).ToList();
            return sortedtList;
        }

        public async Task<List<LocationMappingResponseModal>> GetLocationMappingList(int officerId)
        {
            var locationMappingForms = new LocationMappingRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                OfficerID = officerId
            };

            StringContent locationMappingContent = new StringContent(JsonConvert.SerializeObject(locationMappingForms), Encoding.UTF8, "application/json");

            var locationMappingResponse = await _requestService.CommonRequestService(locationMappingContent, WebApiUrl.getLocationMapping);

            JObject locationMappingObj = JObject.Parse(JsonConvert.DeserializeObject(locationMappingResponse).ToString());
            var locationMappingJarr = locationMappingObj["Data"].Value<JArray>();
            List<LocationMappingResponseModal> locationMappingLst = locationMappingJarr.ToObject<List<LocationMappingResponseModal>>();

            return locationMappingLst;
        }

        public async Task<List<SalesAreaResponseModal>> GetSalesAreaList(string regionId)
        {
            var salesAreaForms = new SalesAreaRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid,
                RegionID = regionId
            };

            StringContent salesAreaContent = new StringContent(JsonConvert.SerializeObject(salesAreaForms), Encoding.UTF8, "application/json");

            var salesAreaResponse = await _requestService.CommonRequestService(salesAreaContent, WebApiUrl.getSalesArea);

            JObject salesAreaObj = JObject.Parse(JsonConvert.DeserializeObject(salesAreaResponse).ToString());
            var salesAreaJarr = salesAreaObj["Data"].Value<JArray>();
            List<SalesAreaResponseModal> salesAreaLst = salesAreaJarr.ToObject<List<SalesAreaResponseModal>>();

            return salesAreaLst;
        }

        public async Task<List<MerchantTypeResponseModal>> GetMerchantTypeList()
        {
            var merchantTypeForms = new BaseEntity
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid
            };

            StringContent merchantTypeContent = new StringContent(JsonConvert.SerializeObject(merchantTypeForms), Encoding.UTF8, "application/json");

            var merchantTypeResponse = await _requestService.CommonRequestService(merchantTypeContent, WebApiUrl.getMerchantType);

            JObject merchantTypeObj = JObject.Parse(JsonConvert.DeserializeObject(merchantTypeResponse).ToString());
            var merchantTypeJarr = merchantTypeObj["Data"].Value<JArray>();
            List<MerchantTypeResponseModal> merchantTypeLst = merchantTypeJarr.ToObject<List<MerchantTypeResponseModal>>();

            return merchantTypeLst;
        }

        public async Task<List<OfficerTypeResponseModal>> GetOfficerTypeList()
        {
            var officerTypeForms = new BaseEntity
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid
            };

            StringContent officerTypeContent = new StringContent(JsonConvert.SerializeObject(officerTypeForms), Encoding.UTF8, "application/json");

            var officerTypeResponse = await _requestService.CommonRequestService(officerTypeContent, WebApiUrl.getOfficerType);

            JObject officerTypeObj = JObject.Parse(JsonConvert.DeserializeObject(officerTypeResponse).ToString());
            var officerTypeJarr = officerTypeObj["Data"].Value<JArray>();
            List<OfficerTypeResponseModal> officerTypeLst = officerTypeJarr.ToObject<List<OfficerTypeResponseModal>>();

            return officerTypeLst;
        }

        public async Task<List<OutletCategoryResponseModal>> GetOutletCategoryList()
        {
            var outletCategoryForms = new BaseEntity
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid
            };

            StringContent outletCategoryContent = new StringContent(JsonConvert.SerializeObject(outletCategoryForms), Encoding.UTF8, "application/json");

            var outletCategoryResponse = await _requestService.CommonRequestService(outletCategoryContent, WebApiUrl.getOutletCategory);

            JObject outletCategoryObj = JObject.Parse(JsonConvert.DeserializeObject(outletCategoryResponse).ToString());
            var outletCategoryJarr = outletCategoryObj["Data"].Value<JArray>();
            List<OutletCategoryResponseModal> outletCategoryLst = outletCategoryJarr.ToObject<List<OutletCategoryResponseModal>>();

            return outletCategoryLst;
        }

        public async Task<List<RegionalOfficeResponseModal>> GetRegionalOfficeList(string zonalOfcID)
        {
            var regionalOfficeForms = new RegionalOfficeRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = zonalOfcID == null ? "0" : zonalOfcID,
                RegionalId = CommonBase.regionalid,
                ZonalID = zonalOfcID == null ? "0" : zonalOfcID
            };

            StringContent regionalOfficeContent = new StringContent(JsonConvert.SerializeObject(regionalOfficeForms), Encoding.UTF8, "application/json");

            var regionalOfficeResponse = await _requestService.CommonRequestService(regionalOfficeContent, WebApiUrl.regionalOffice);

            JObject regionalOfficeObj = JObject.Parse(JsonConvert.DeserializeObject(regionalOfficeResponse).ToString());
            var regionalOfficeJarr = regionalOfficeObj["Data"].Value<JArray>();
            List<RegionalOfficeResponseModal> regionalOfficeLst = regionalOfficeJarr.ToObject<List<RegionalOfficeResponseModal>>();

            return regionalOfficeLst;
        }

        public async Task<List<SbuTypeResponseModal>> GetSbuTypeList()
        {
            var sbuTypeForms = new BaseEntity
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid
            };

            StringContent sbuTypeContent = new StringContent(JsonConvert.SerializeObject(sbuTypeForms), Encoding.UTF8, "application/json");

            var sbuTypeResponse = await _requestService.CommonRequestService(sbuTypeContent, WebApiUrl.getSbu);

            JObject sbuTypeObj = JObject.Parse(JsonConvert.DeserializeObject(sbuTypeResponse).ToString());
            var sbuTypeJarr = sbuTypeObj["Data"].Value<JArray>();
            List<SbuTypeResponseModal> sbuTypeLst = sbuTypeJarr.ToObject<List<SbuTypeResponseModal>>();

            return sbuTypeLst;
        }

        public async Task<List<StateResponseModal>> GetStateList()
        {

            var officerStateForms = new StateRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid,
                CountryID = "0"
            };

            StringContent officeStateContent = new StringContent(JsonConvert.SerializeObject(officerStateForms), Encoding.UTF8, "application/json");

            var officeStateResponse = await _requestService.CommonRequestService(officeStateContent, WebApiUrl.getState);

            JObject officeStateObj = JObject.Parse(JsonConvert.DeserializeObject(officeStateResponse).ToString());
            var officeStateJarr = officeStateObj["Data"].Value<JArray>();
            List<StateResponseModal> stateLst = officeStateJarr.ToObject<List<StateResponseModal>>();

            return stateLst;
        }

        public async Task<List<ZonalOfficeResponseModal>> GetZonalOfficeList()
        {
            var zonalOfficeForms = new BaseEntity
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid
            };

            StringContent zonalOfficeContent = new StringContent(JsonConvert.SerializeObject(zonalOfficeForms), Encoding.UTF8, "application/json");

            var zonalOfficeResponse = await _requestService.CommonRequestService(zonalOfficeContent, WebApiUrl.getZonalOffice);

            JObject zonalOfficeObj = JObject.Parse(JsonConvert.DeserializeObject(zonalOfficeResponse).ToString());
            var zonalOfficeJarr = zonalOfficeObj["Data"].Value<JArray>();
            List<ZonalOfficeResponseModal> zonalOfficeLst = zonalOfficeJarr.ToObject<List<ZonalOfficeResponseModal>>();

            return zonalOfficeLst;
        }

        public async Task<string> ValidateUserName(string userName)
        {
            var validateUserNameForms = new ValidateUserNameRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid,
                UserName = userName
            };

            StringContent validateUserNameContent = new StringContent(JsonConvert.SerializeObject(validateUserNameForms), Encoding.UTF8, "application/json");

            var validateUserNameResponse = await _requestService.CommonRequestService(validateUserNameContent, WebApiUrl.getZonalOffice);

            JObject validateUserNameObj = JObject.Parse(JsonConvert.DeserializeObject(validateUserNameResponse).ToString());
            var validateUserNameJarr = validateUserNameObj["Data"].Value<JArray>();
            List<SuccessResponse> validateUserNameLst = validateUserNameJarr.ToObject<List<SuccessResponse>>();

            return validateUserNameLst.First().Status.ToString();
        }

        public async Task<List<RegionModel>> GetRegionList()
        {
            var CustomerRegion = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
                {"ZoneID",  "0" }
            };

            StringContent customerRegionContent = new StringContent(JsonConvert.SerializeObject(CustomerRegion), Encoding.UTF8, "application/json");

            var customerRegionResponse = await _requestService.CommonRequestService(customerRegionContent, WebApiUrl.getLocationRegion);

            JObject customerRegionObj = JObject.Parse(JsonConvert.DeserializeObject(customerRegionResponse).ToString());
            var customerRegionJarr = customerRegionObj["Data"].Value<JArray>();
            List<RegionModel> customerRegionLst = customerRegionJarr.ToObject<List<RegionModel>>();
                        
            return customerRegionLst;
        }

        public async Task<List<CustomerStateModel>> GetCustStateList()
        {
            var CustomerStateRequest = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"Country", "0"}
            };


            StringContent Statecontent = new StringContent(JsonConvert.SerializeObject(CustomerStateRequest), Encoding.UTF8, "application/json");
            var responseState = await _requestService.CommonRequestService(Statecontent, WebApiUrl.getState);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseState).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerStateModel> stateLst = jarr.ToObject<List<CustomerStateModel>>();

            var sortedtList = stateLst.OrderBy(x => x.StateName).ToList();

            return sortedtList;

        }

        public async Task<CustomerInserCardResponseData> CheckformNumberDuplication(string FormNumber)
        {
            var CustomerFormNumber = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"FormNumber", FormNumber }
            };


            StringContent cusFormcontent = new StringContent(JsonConvert.SerializeObject(CustomerFormNumber), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(cusFormcontent, WebApiUrl.checkformnumberDuplication);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerInserCardResponseData> lst = jarr.ToObject<List<CustomerInserCardResponseData>>();
            return lst[0];
        }

        public async Task<List<OfficerDistrictModel>> GetDistrictDetails(string Stateid)
        {

            var requestBody = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"StateID", Stateid.ToString()}
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getDistrict);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<OfficerDistrictModel> lstOfficerDistrictModel = jarr.ToObject<List<OfficerDistrictModel>>();

            var SortedtList = lstOfficerDistrictModel.OrderBy(x => x.districtName).ToList();
            SortedtList.Insert(0, new OfficerDistrictModel
            {
                stateID = 0,
                districtID = 0,
                districtName = "Select District"
            });

            return SortedtList;
        }

        public async Task<CustomerInserCardResponseData> CheckPanNoDuplication(string PanNo)
        {
            var CustomerPanInfo = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"ZonalId", "0"},
                {"RegionalId", "0"},
                {"IncomeTaxPan", PanNo }
            };


            StringContent cusPancontent = new StringContent(JsonConvert.SerializeObject(CustomerPanInfo), Encoding.UTF8, "application/json");

            var responseCustomer = await _requestService.CommonRequestService(cusPancontent, WebApiUrl.checkPanNoDuplication);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseCustomer).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerInserCardResponseData> lst = jarr.ToObject<List<CustomerInserCardResponseData>>();
            return lst[0];
        }

        public async Task<CustomerInserCardResponseData> CheckMobilNoDuplication(string MobileNo)
        {
            //fetching Customer info
            var CustomerFormNumber = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"CommunicationMobileNo", MobileNo }
            };


            StringContent cusFormcontent = new StringContent(JsonConvert.SerializeObject(CustomerFormNumber), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(cusFormcontent, WebApiUrl.checkmobileNoDuplication);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerInserCardResponseData> lst = jarr.ToObject<List<CustomerInserCardResponseData>>();
            return lst[0];
        }
        public async Task<CustomerInserCardResponseData> CheckEmailDuplication(string Emailid)
        {
            //fetching Customer info
            var CustomerFormNumber = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"CommunicationEmailid", Emailid }
            };


            StringContent cusFormcontent = new StringContent(JsonConvert.SerializeObject(CustomerFormNumber), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(cusFormcontent, WebApiUrl.checkemailidDuplication);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerInserCardResponseData> lst = jarr.ToObject<List<CustomerInserCardResponseData>>();
            return lst[0];
        }
        public async Task<string> PANValidation(string PANNumber)
        {
            string apiUrl = "v2/pan";

            var input = new
            {
                consent = "Y",
                pan = PANNumber
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

            var response = await _requestService.PANValidationService(content, apiUrl);

            return response;

        }

    }
}