using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.RequestModel.DtpSupport;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ResponseModel.DtpSupport;
using HPCL.Common.Models.ViewModel.DtpSupport;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    public class DtpSupportService : IDtpSupportService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;

        public DtpSupportService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }

        public async Task<GetBlockUnblockCustomerCcmsAccountByCustomeridResponse> GetBlockUnblockCustomerCcmsAccount(string customerId)
        {
            var reqBody = new GetBlockUnblockCustomerCcmsAccountByCustomerid
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerId = customerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetBlockUnblockCustomerCcmsAccountByCustomeridUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetBlockUnblockCustomerCcmsAccountByCustomeridResponse res = obj.ToObject<GetBlockUnblockCustomerCcmsAccountByCustomeridResponse>();
            return res;
        }

        public async Task<string> UpdateCustomerCcmsAccountStatus(BlockUnblockCustomerCcmsAccount entity)
        {
            var reqBody = new BlockUnblockCustomerCcmsAccount
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = entity.CustomerID,
                CCMSBalanceStatus = entity.CCMSBalanceStatus,
                CCMSBalanceRemarks = entity.CCMSBalanceRemarks,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.BlockUnblockCustomerCcmsAccountUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res[0].Reason;
        }
        public async Task<List<SuccessResponse>> GetCardBalanceTransferDetails(string CardNo,string CardStatus)
        {
            var reqBody = new CardBalanceTransferViewModel
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CardNo = CardNo,
                CardStatus= string.IsNullOrEmpty(CardStatus)?1: Convert.ToInt32(CardStatus)
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCardBalanceTransfer);

           
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res;
        }
        public async Task<UnblockUserModel> UnblockUser()
        {
            UnblockUserModel model = new UnblockUserModel();
            model.Message = "";
            model.Success = "";
            return model;
        }
        public async Task<GeneralUpdatesModel> GeneralUpdates()
        {
            GeneralUpdatesModel model = new GeneralUpdatesModel();
            List<GetEntityModel> list = new List<GetEntityModel>();
            List<GetEntityModel> newList = new List<GetEntityModel>();
            list = await _commonActionService.GetEntityModelList();
            foreach (GetEntityModel m in list)
            {
                if (m.EntityId == 1 || m.EntityId == 2 || m.EntityId == 3)
                {
                    newList.Add(m);
                }
            }
            model.EntityMdl.AddRange(newList);
            model.MerchantTypes.AddRange(await _commonActionService.GetMerchantTypeList());
            model.Message = "";
            model.UpdateStatus = "";
            return model;
        }
        public async Task<GetEntityOldFieldValueResponse> GetEntityOldFieldValue(string EntityTypeId, string EntityFieldId, string CustomerIdOrCardOrMerchantId)
        {
            var searchBody = new GetEntityOldFieldValueRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                EntityTypeId = EntityTypeId,
                EntityFieldId = EntityFieldId,
                CustomerIdOrCardOrMerchantId= CustomerIdOrCardOrMerchantId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetEntityOldFieldValue);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            GetEntityOldFieldValueResponse searchList = obj.ToObject<GetEntityOldFieldValueResponse>();
            
            return searchList;
        }
        public async Task<GeneralUpdatesModel> GeneralUpdates(GeneralUpdatesModel model)
        {
            model.UserAgent = CommonBase.useragent;
            model.UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress");
            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            CustomerInserCardResponse updateResponse;

            var responseCustomer = await _requestService.CommonRequestService(content, WebApiUrl.UpdateGeneralEntityField);

            updateResponse = JsonConvert.DeserializeObject<CustomerInserCardResponse>(responseCustomer);

            List<GetEntityModel> list = new List<GetEntityModel>();
            List<GetEntityModel> newList = new List<GetEntityModel>();
            list = await _commonActionService.GetEntityModelList();
            foreach (GetEntityModel m in list)
            {
                if (m.EntityId == 1 || m.EntityId == 2 || m.EntityId == 3)
                {
                    newList.Add(m);
                }
            }
            model.EntityMdl.AddRange(newList);
            model.EntityFieldMdl.AddRange(await _commonActionService.GetEntityFieldModelList(model.EntityTypeId.ToString()));
            model.MerchantTypes.AddRange(await _commonActionService.GetMerchantTypeList());
            model.UpdateStatus = "";
            if (updateResponse.Internel_Status_Code == 1000)
            {
                model.StatusCode = updateResponse.Internel_Status_Code;
                model.Message = updateResponse.Message;
                if (updateResponse != null && updateResponse.Data != null && updateResponse.Data.Count > 0)
                {
                    model.Status = updateResponse.Data[0].Status;
                    model.Message = updateResponse.Data[0].Reason;

                    if (model.Status == 1)
                    {
                        model.UpdateStatus = "UPDATED";
                    }
                }
            }
            else
            {
                model.Message = updateResponse.Message;
                model.StatusCode = updateResponse.Internel_Status_Code;
                if (updateResponse != null && updateResponse.Data != null && updateResponse.Data.Count > 0)
                {
                    model.Status = updateResponse.Data[0].Status;
                    model.Message = updateResponse.Data[0].Reason;
                }
            }

            return model;
        }
        public async Task<TeamMappingViewModel> TeamMappingSearch(TeamMappingViewModel teamMappingViewModel)
        {
            var reqBody = new TeamMappingSearchRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ZBMID = teamMappingViewModel.ZBMID,
                ZBMName = teamMappingViewModel.ZBMName,
                RSMID = teamMappingViewModel.RSMID,
                RSMName = teamMappingViewModel.RSMName,
                RBEID = teamMappingViewModel.RBEID,
                RBEName = teamMappingViewModel.RBEName,
                Location = teamMappingViewModel.Location,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetTeamMapping);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            TeamMappingViewModel res = obj.ToObject<TeamMappingViewModel>();
            return res;
        }
        public async Task<List<SuccessResponse>> AddTeamMapping(TeamMappingViewModel teamMappingViewModel)
        {
            var reqBody = new TeamMappingSearchRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ZBMID = teamMappingViewModel.ZBMID,
                ZBMName = teamMappingViewModel.ZBMName,
                RSMID = teamMappingViewModel.RSMID,
                RSMName = teamMappingViewModel.RSMName,
                RBEID = teamMappingViewModel.RBEID,
                RBEName = teamMappingViewModel.RBEName,
                Location = teamMappingViewModel.Location,
                CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.InsertTeamMapping);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();

            return res;
        }
        public async Task<List<SuccessResponse>> DeleteTeamMapping(string teamMappingId)
        {
            var reqBody = new TeamMappingSearchRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                TeamMappingId=teamMappingId,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.DeleteTeamMapping);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res;
        }
        public async Task<List<SuccessResponse>> UpdateTeamMapping(TeamMappingViewModel teamMappingViewModel)
        {
            var reqBody = new TeamMappingSearchRequest
            {
                UserAgent = CommonBase.useragent,
                UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                ZBMID = teamMappingViewModel.ZBMID,
                ZBMName = teamMappingViewModel.ZBMName,
                RSMID = teamMappingViewModel.RSMID,
                RSMName = teamMappingViewModel.RSMName,
                RBEID = teamMappingViewModel.RBEID,
                RBEName = teamMappingViewModel.RBEName,
                Location = teamMappingViewModel.Location,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                TeamMappingId=teamMappingViewModel.TeamMappingId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.updateteammapping);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();

            return res;
        }

        public async Task<GetDetailForUserUnblockResponse> GetDetailForUserUnblock(string CustomerId, string UserName)
        {
            var searchBody = new GetDetailForUserUnblockRequest();
            string id = CustomerId;
            if (string.IsNullOrEmpty(id))
            {
                id = UserName;
            }
            if (!string.IsNullOrEmpty(CustomerId) && !string.IsNullOrEmpty(UserName))
            {
                searchBody = new GetDetailForUserUnblockRequest
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CustomerId = CustomerId,
                    UserName = UserName
                };
            }
            else
            {
                searchBody = new GetDetailForUserUnblockRequest
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress"),
                    CustomerId = string.IsNullOrEmpty(id) ? "" : id,
                    UserName = ""
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.getDetailForUserUnblockByCustomeridOrUsername);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            GetDetailForUserUnblockResponse searchList = obj.ToObject<GetDetailForUserUnblockResponse>();

            return searchList;
        }
        public async Task<UnblockUserModel> UnblockUser(UnblockUserModel model)
        {
            model.UserAgent = CommonBase.useragent;
            model.UserIp = _httpContextAccessor.HttpContext.Session.GetString("IpAddress");
            model.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            model.BloackUnblockStatus = 0;

            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            CustomerInserCardResponse updateResponse;

            var responseCustomer = await _requestService.CommonRequestService(content, WebApiUrl.userUnblock);

            updateResponse = JsonConvert.DeserializeObject<CustomerInserCardResponse>(responseCustomer);

            model.UpdateStatus = "";
            model.Success = "";
            model.Message = "";
            if (updateResponse.Internel_Status_Code == 1000)
            {
                model.StatusCode = updateResponse.Internel_Status_Code;
                model.Message = updateResponse.Message;
                if (updateResponse != null && updateResponse.Data != null && updateResponse.Data.Count > 0)
                {
                    model.Status = updateResponse.Data[0].Status;
                    model.Message = updateResponse.Data[0].Reason;

                    if (model.Status == 1)
                    {
                        model.UpdateStatus = "UPDATED";
                        model.Success = updateResponse.Data[0].Reason;
                        model.Message = "";
                    }
                    else
                    {
                        model.StatusCode = updateResponse.Internel_Status_Code + 1;
                    }
                }
            }
            else
            {
                model.Message = updateResponse.Message;
                model.StatusCode = updateResponse.Internel_Status_Code;
                if (updateResponse != null && updateResponse.Data != null && updateResponse.Data.Count > 0)
                {
                    model.Status = updateResponse.Data[0].Status;
                    model.Message = updateResponse.Data[0].Reason;
                    model.Success = "";
                }
            }

            return model;
        }

    }
}
