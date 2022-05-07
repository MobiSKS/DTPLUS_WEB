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
                UserIp = CommonBase.userip,
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
                UserIp = CommonBase.userip,
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
        public async Task<string> GetCardBalanceTransferDetails(string CardNo)
        {
            var reqBody = new CardBalanceTransferViewModel
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CardNo = CardNo
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCardBalanceTransfer);

           
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            var jarr = obj["Data"].Value<JArray>();
            List<SuccessResponse> res = jarr.ToObject<List<SuccessResponse>>();
            return res[0].Reason;
        }
        public async Task<UnblockUserModel> UnblockUser()
        {
            UnblockUserModel model = new UnblockUserModel();
            model.Message = "";
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
                UserIp = CommonBase.userip,
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
            model.UserIp = CommonBase.userip;
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

    }
}
