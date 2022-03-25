using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.RequestEnities;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.RequestModel.Customer;
using HPCL.Common.Models.RequestModel.Merchant;
using HPCL.Common.Models.RequestModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ResponseModel.MyHpOTCCardCustomer;
using HPCL.Common.Models.ViewModel.Locations;
using HPCL.Common.Models.ViewModel.Officers;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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
            var forms = new BaseEntity()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ZonalId = CommonBase.zonalid,
                RegionalId = CommonBase.regionalid
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

            var validateUserNameResponse = await _requestService.CommonRequestService(validateUserNameContent, WebApiUrl.validateUserName);

            JObject validateUserNameObj = JObject.Parse(JsonConvert.DeserializeObject(validateUserNameResponse).ToString());
            var validateUserNameJarr = validateUserNameObj["Data"].Value<JArray>();
            List<SuccessResponse> validateUserNameLst = validateUserNameJarr.ToObject<List<SuccessResponse>>();

            return validateUserNameLst.First().Status.ToString();
        }

        //public async Task<List<RegionModel>> GetRegionList()
        //{
        //    var CustomerRegion = new Dictionary<string, string>
        //    {
        //        {"Useragent", CommonBase.useragent},
        //        {"Userip", CommonBase.userip},
        //        {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
        //        {"ZonalID",  "0" }
        //    };

        //    StringContent customerRegionContent = new StringContent(JsonConvert.SerializeObject(CustomerRegion), Encoding.UTF8, "application/json");

        //    var customerRegionResponse = await _requestService.CommonRequestService(customerRegionContent, WebApiUrl.regionalOffice);

        //    JObject customerRegionObj = JObject.Parse(JsonConvert.DeserializeObject(customerRegionResponse).ToString());
        //    var customerRegionJarr = customerRegionObj["Data"].Value<JArray>();
        //    List<RegionModel> customerRegionLst = customerRegionJarr.ToObject<List<RegionModel>>();

        //    return customerRegionLst;
        //}

        public async Task<List<CustomerStateModel>> GetCustStateList()
        {
            var requestData = new GetStatesRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                CountryID = "0"
            };

            StringContent Statecontent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var responseState = await _requestService.CommonRequestService(Statecontent, WebApiUrl.getState);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseState).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerStateModel> stateLst = jarr.ToObject<List<CustomerStateModel>>();

            var sortedtList = stateLst.OrderBy(x => x.StateName).ToList();

            return sortedtList;
        }

        public async Task<CustomerInserCardResponseData> CheckformNumberDuplication(string FormNumber)
        {
            var request = new CheckformNumberDuplicationRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                FormNumber = FormNumber
            };

            StringContent cusFormcontent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(cusFormcontent, WebApiUrl.checkformnumberDuplication);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerInserCardResponseData> lst = jarr.ToObject<List<CustomerInserCardResponseData>>();
            return lst[0];
        }

        public async Task<List<OfficerDistrictModel>> GetDistrictDetails(string Stateid)
        {
            var request = new GetDistrictListRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                StateID = Stateid.ToString()
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

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
            //Request info
            var requestInfo = new MobilNoDuplicationCheckRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                CommunicationMobileNo = MobileNo
            };


            StringContent cusFormcontent = new StringContent(JsonConvert.SerializeObject(requestInfo), Encoding.UTF8, "application/json");

            var ResponseContent = await _requestService.CommonRequestService(cusFormcontent, WebApiUrl.checkmobileNoDuplication);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(ResponseContent).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerInserCardResponseData> lst = jarr.ToObject<List<CustomerInserCardResponseData>>();
            return lst[0];
        }
        public async Task<CustomerInserCardResponseData> CheckEmailDuplication(string Emailid)
        {
            //Request info
            var requestInfo = new EmailDuplicationCheckRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                CommunicationEmailid = Emailid
            };


            StringContent cusFormcontent = new StringContent(JsonConvert.SerializeObject(requestInfo), Encoding.UTF8, "application/json");

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
        public async Task<List<CustomerRegionModel>> GetregionalOfficeList()
        {
            CustomerRegionRequestModel customerRegion = new CustomerRegionRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ZonalId = "0"
            };

            StringContent customerRegionContent = new StringContent(JsonConvert.SerializeObject(customerRegion), Encoding.UTF8, "application/json");

            var customerRegionResponse = await _requestService.CommonRequestService(customerRegionContent, WebApiUrl.getRegionalOffice);

            JObject customerRegionObj = JObject.Parse(JsonConvert.DeserializeObject(customerRegionResponse).ToString());
            var customerRegionJarr = customerRegionObj["Data"].Value<JArray>();
            List<CustomerRegionModel> customerregionalOfficeLst = customerRegionJarr.ToObject<List<CustomerRegionModel>>();

            return customerregionalOfficeLst;
        }

        public async Task<List<StatusResponseModal>> GetStatusType(string status)
        {
            var statusType = new StatusRequestModal
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                EntityTypeId = 3
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(statusType), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetStatusTypeUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<StatusResponseModal> lst = jarr.ToObject<List<StatusResponseModal>>();

            List<StatusResponseModal> lsts = new List<StatusResponseModal>();
            lsts.Add(new StatusResponseModal { StatusId = -1, StatusName = "All" });

            if (status == "ManageCard")
            {
                foreach (var item in lst)
                {
                    if (item.StatusId == 4 || item.StatusId == 6 || item.StatusId == 7)
                    {
                        lsts.Add(item);
                    }
                }
            }
            else if (status == "SaleLimit")
            {
                foreach (var item in lst)
                {
                    if (item.StatusId == 4 || item.StatusId == 1)
                    {
                        lsts.Add(item);
                    }
                }
            }
            else if (status == "AllCardLimit")
            {
                foreach (var item in lst)
                {
                    if (item.StatusId == 4)
                    {
                        lsts.Add(item);
                    }
                }
            }
            return lsts;
        }

        public async Task<List<TerminalManagementCloseReasonModel>> GetTerminalRequestCloseReason()
        {
            var forms = new Dictionary<string, string>
            {
                {"useragent", CommonBase.useragent},
                {"userip", CommonBase.userip},
                {"userid", _httpContextAccessor.HttpContext.Session.GetString("UserId")},
            };


            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getreasonlist);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<TerminalManagementCloseReasonModel> ReasonType = jarr.ToObject<List<TerminalManagementCloseReasonModel>>();
            var sortedtList = ReasonType.OrderBy(x => x.ReasonId).ToList();
            return sortedtList;
        }

        public async Task<string> CheckVehicleRegistrationValid(string RegistrationNumber)
        {
            string apiUrl = "v3/rc-advanced";

            var input = new VehicleRegistrationValidateRequestModel()
            {
                registrationNumber = RegistrationNumber,
                consent = "Y",
                version = "3.1"
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

            var response = await _requestService.VehicleRegistrationValidCheckService(content, apiUrl);

            return response;
        }

        public async Task<MerchantDetailsResponseOTCCardCustomer> GetMerchantDetailsByMerchantId(string MerchantID)
        {
            MerchantDetailsResponseOTCCardCustomer merchantDetails = new MerchantDetailsResponseOTCCardCustomer();

            var requestinfo = new Dictionary<string, string>
            {
                {"Useragent", CommonBase.useragent},
                {"Userip", CommonBase.userip},
                {"Userid", _httpContextAccessor.HttpContext.Session.GetString("UserName")},
                {"MerchantId", MerchantID}
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.searchMerchantForCardCreation);

            MerchantResponseOTCCardCustomer merchant = JsonConvert.DeserializeObject<MerchantResponseOTCCardCustomer>(response);


            if (merchant.Internel_Status_Code == 1000 && merchant.Data != null && merchant.Data.Count > 0)
            {
                merchantDetails.RegionalOfficeName = merchant.Data[0].RegionalOfficeName;
                merchantDetails.RetailOutletName = merchant.Data[0].RetailOutletName;
                merchantDetails.SalesAreaName = merchant.Data[0].SalesAreaName;
                merchantDetails.ZonalOfficeName = merchant.Data[0].ZonalOfficeName;
                merchantDetails.RegionalOfficeId = merchant.Data[0].RegionalOfficeId;
                merchantDetails.Internel_Status_Code = merchant.Internel_Status_Code;
                merchantDetails.Status_Code = merchant.Status_Code;
            }

            return merchantDetails;

        }
        public async Task<List<ProofType>> GetAddressProofList()
        {
            var forms = new BaseEntity()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetProofTyleUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<ProofType> SortedtList = jarr.ToObject<List<ProofType>>();

            return SortedtList;
        }

        public async Task<CommonResponseData> VerifyMerchantByMerchantidAndRegionalid(string RegionalId, string MerchantID)
        {
            CommonResponseData responseData = new CommonResponseData();

            VerifyMerchantRequestModel requestinfo = new VerifyMerchantRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                RegionalOfficeId = RegionalId,
                MerchantId = MerchantID
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.verifyMerchantByMerchantidAndRegionalid);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CommonResponseData> searchList = jarr.ToObject<List<CommonResponseData>>();
            responseData = searchList[0];

            return responseData;
        }
   
        public async Task<List<CustomerZonalOfficeModel>> GetZonalOfficeListForDropdown()
        {
            var requestData = new BaseEntity()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getZonalOffice);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerZonalOfficeModel> SortedtList = jarr.ToObject<List<CustomerZonalOfficeModel>>();

            return SortedtList;
        }

        public async Task<List<CustomerSecretQueModel>> GetCustomerSecretQuestionListForDropdown()
        {
            var requestData = new BaseEntity()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };
            StringContent SecretQuecontent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(SecretQuecontent, WebApiUrl.getSecretQuestion);


            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerSecretQueModel> SortedtList = jarr.ToObject<List<CustomerSecretQueModel>>();

            return SortedtList;
        }

        public async Task<List<ProofType>> ProofType()
        {
            var forms = new BaseEntity
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetProofTyleUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<ProofType> proofTypeList = jarr.ToObject<List<ProofType>>();
            return proofTypeList;
        }
        public async Task<List<CustomerRegionModel>> GetRegionalDetailsDropdown(int ZonalOfficeID)
        {
            CustomerRegionRequestModel customerZone = new CustomerRegionRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ZonalId = ZonalOfficeID.ToString()
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(customerZone), Encoding.UTF8, "application/json");

            var responseRegionalOffice = await _requestService.CommonRequestService(content, WebApiUrl.getRegionalOffice);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseRegionalOffice).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerRegionModel> lstCustomerRegionModel = jarr.ToObject<List<CustomerRegionModel>>();
            lstCustomerRegionModel.Add(new CustomerRegionModel
            {
                RegionalOfficeID = 0,
                RegionalOfficeName = "--Select--",

            });
            var SortedtList = lstCustomerRegionModel.OrderBy(x => x.RegionalOfficeID).ToList();
            return SortedtList;
        }

        public async Task<List<TerminalStatusResponseModal>> GetMerchantStatus()
        {
            var forms = new BaseEntity
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(forms), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.getMerchantStatusList);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<TerminalStatusResponseModal> merchantStatusList = jarr.ToObject<List<TerminalStatusResponseModal>>();
            return merchantStatusList;
        }

        public async Task<CommonResponseData> CheckDealerCodeIsValid(string DealerCode)
        {
            CommonResponseData responseData = new CommonResponseData();

            VerifyDealerRequestModel requestinfo = new VerifyDealerRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                DealerCode = DealerCode
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.checkDealerCode);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CommonResponseData> searchList = jarr.ToObject<List<CommonResponseData>>();
            responseData = searchList[0];
            responseData.Internel_Status_Code = Convert.ToInt32(obj["Internel_Status_Code"].ToString());

            return responseData;
        }
        public async Task<List<VehicleTypeModel>> GetVehicleTypeDropdown()
        {
            var requestData = new BaseEntity()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };

            StringContent contentforms = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(contentforms, WebApiUrl.getVehicleTpe);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<VehicleTypeModel> lstVehicleTpe = jarr.ToObject<List<VehicleTypeModel>>();

            var SortedtList = lstVehicleTpe.OrderBy(x => x.VehicleTypeName).ToList();
            return SortedtList;
        }
        public async Task<CommonResponseData> CheckPanCardDuplicationByDistrictid(string DistrictId, string IncomeTaxPan)
        {
            CommonResponseData responseData = new CommonResponseData();

            var requestinfo = new CheckPancardbyDistrictIdRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                DistrictId = DistrictId,
                IncomeTaxPan = IncomeTaxPan
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.checkPanCardByDistrictid);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CommonResponseData> searchList = jarr.ToObject<List<CommonResponseData>>();
            responseData = searchList[0];
            responseData.Internel_Status_Code = Convert.ToInt32(obj["Internel_Status_Code"].ToString());

            return responseData;
        }

        public async Task<List<CustomerTypeModel>> GetCustomerTypeListDropdown()
        {
            var request = new GetCustomerTypeRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                CTFlag = "1"
            };


            StringContent custTypeContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(custTypeContent, WebApiUrl.getCustomerType);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerTypeModel> SortedtList = jarr.ToObject<List<CustomerTypeModel>>();

            return SortedtList;
        }
        public async Task<List<CustomerTbentityModel>> GetCustomerTbentityListDropdown()
        {
            var request = new BaseEntity()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };


            StringContent custTypeContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(custTypeContent, WebApiUrl.getTbentityName);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerTbentityModel> SortedtList = jarr.ToObject<List<CustomerTbentityModel>>();

            return SortedtList;
        }
        public async Task<List<CustomerTypeOfFleetModel>> GetCustomerTypeOfFleetDropdown()
        {
            var request = new BaseEntity()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };


            StringContent custTypeContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(custTypeContent, WebApiUrl.getTypeOfFleet);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerTypeOfFleetModel> SortedtList = jarr.ToObject<List<CustomerTypeOfFleetModel>>();

            return SortedtList;
        }
        public async Task<List<CustomerSubTypeModel>> GetCustomerSubTypeDropdown()
        {
            var request = new BaseEntity()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };


            StringContent custTypeContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(custTypeContent, WebApiUrl.getCustomerSubType);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerSubTypeModel> lstCustomerSubTypeModel = jarr.ToObject<List<CustomerSubTypeModel>>();

            lstCustomerSubTypeModel.Add(new CustomerSubTypeModel
            {
                CustomerSubTypeID = 0,
                CustomerSubTypeName = "Select Customer Sub type"
            });
            var SortedtList = lstCustomerSubTypeModel.OrderBy(x => x.CustomerSubTypeID).ToList();

            return SortedtList;
        }
        public async Task<List<SalesAreaModel>> GetSalesAreaDropdown(string RegionID)
        {
            var request = new GetSalesAreaDropdownRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                RegionID = RegionID
            };


            StringContent custTypeContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(custTypeContent, WebApiUrl.getSalesArea);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SalesAreaModel> lstSalesAreaModel = jarr.ToObject<List<SalesAreaModel>>();

            //lstSalesAreaModel.Add(new SalesAreaModel
            //{
            //    SalesAreaID = 0,
            //    SalesAreaName = "Select Sales Area"
            //});
            var SortedtList = lstSalesAreaModel.OrderBy(x => x.SalesAreaID).ToList();

            return SortedtList;
        }
        public async Task<List<CustomerSubTypeModel>> GetCustomerSubTypeDropdown(int CustomerTypeID)
        {
            var customerType = new GetCustomerSubTypeDropDownRequest()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                CustomerTypeId = CustomerTypeID.ToString()
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(customerType), Encoding.UTF8, "application/json");


            var responseCustomerSubType = await _requestService.CommonRequestService(content, WebApiUrl.getCustomerSubType);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseCustomerSubType).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerSubTypeModel> lstCustomerSubType = jarr.ToObject<List<CustomerSubTypeModel>>();
            lstCustomerSubType.Add(new CustomerSubTypeModel
            {
                CustomerSubTypeID = 0,
                CustomerSubTypeName = "Select Customer Sub Type"
            });
            var SortedtList = lstCustomerSubType.OrderBy(x => x.CustomerSubTypeID).ToList();

            return SortedtList;
        }

        public async Task<List<TransactionTypeResponse>> GetTransactionTypeDropdown()
        {
            var requestData = new BaseEntity()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetTransationTypeUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<TransactionTypeResponse> SortedtList = jarr.ToObject<List<TransactionTypeResponse>>();
            return SortedtList;
        }
        public async Task<CommonResponseData> CheckPanCardDuplicationByDistrictidForCustomerUpdate(string DistrictId, string IncomeTaxPan, string CustomerReferenceNo)
        {
            CommonResponseData responseData = new CommonResponseData();

            var requestinfo = new CheckPancardbyDistrictIdRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                DistrictId = DistrictId,
                IncomeTaxPan = IncomeTaxPan,
                CustomerReferenceNo = CustomerReferenceNo
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestinfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.checkPanCardByDistrictIdAndCustomerReferenceno);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CommonResponseData> searchList = jarr.ToObject<List<CommonResponseData>>();
            responseData = searchList[0];
            responseData.Internel_Status_Code = Convert.ToInt32(obj["Internel_Status_Code"].ToString());

            return responseData;
        }

        public async Task<List<GetCountryRegionResponse>> GetCountryRegion()
        {
            var requestData = new BaseEntity()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                ZonalId = "0"
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCountryRegionUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<GetCountryRegionResponse> countryRegionList = jarr.ToObject<List<GetCountryRegionResponse>>();
            return countryRegionList;
        }

        public async Task<List<GetCityResponse>> GetCity()
        {
            var requestData = new BaseEntity()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCityUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<GetCityResponse> cityList = jarr.ToObject<List<GetCityResponse>>();
            return cityList;
        }
        public async Task<CustomerInserCardResponseData> CheckVechileNoUsed(string VechileNo)
        {
            //Request info
            var requestInfo = new VehicleDuplicationCheckRequestModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                VechileNo = VechileNo
            };


            StringContent content = new StringContent(JsonConvert.SerializeObject(requestInfo), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.checkVechileNo);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<CustomerInserCardResponseData> lst = jarr.ToObject<List<CustomerInserCardResponseData>>();
            return lst[0];
        }
    }
}