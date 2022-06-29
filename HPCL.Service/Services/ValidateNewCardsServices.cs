using HPCL.Common.Helper;
using HPCL.Common.Models;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.ValidateNewCard;
using HPCL.Common.Models.ResponseModel.CommonResponse;
using HPCL.Common.Models.ViewModel.ValidateNewCards;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ValidateNewCardsServices : IValidateNewCardsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;

        public ValidateNewCardsServices(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }
        public async Task<CommonResponseData> ActionOnCards([FromBody] ApproveCardDetailsModel approveRejectModel)
        {
            CommonResponseData responseData = new CommonResponseData();
            var actionOnCardsForms = new ApproveCardDetailsModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerReferenceNo = approveRejectModel.CustomerReferenceNo,
                Comments = approveRejectModel.Comments,
                Approvalstatus = approveRejectModel.Approvalstatus,
                ApprovedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                FormNumber= approveRejectModel.FormNumber
            };

            StringContent actionOnCardsContent = new StringContent(JsonConvert.SerializeObject(actionOnCardsForms), Encoding.UTF8, "application/json");
            var actionOnCardsResponse = await _requestService.CommonRequestService(actionOnCardsContent, WebApiUrl.approveRejectCard);
            JObject actionOnCardsObj = JObject.Parse(JsonConvert.DeserializeObject(actionOnCardsResponse).ToString());
            responseData.Internel_Status_Code = Convert.ToInt32(actionOnCardsObj["Internel_Status_Code"].ToString());

            if (actionOnCardsObj["Status_Code"].ToString() == "200")
            {
                var Jarr = actionOnCardsObj["Data"].Value<JArray>();
                List<CommonResponseData> responseLst = Jarr.ToObject<List<CommonResponseData>>();

                if (responseLst != null && responseLst.Count > 0)
                {
                    responseData = responseLst[0];
                }
                responseData.Internel_Status_Code = Convert.ToInt32(actionOnCardsObj["Internel_Status_Code"].ToString());
                if (responseLst != null && responseLst.Count > 0 && responseLst[0].Status != 1)
                {
                    responseData.Internel_Status_Code = responseData.Internel_Status_Code + 1;
                }
            }
            else
            {
                responseData.Internel_Status_Code = Convert.ToInt32(actionOnCardsObj["Internel_Status_Code"].ToString());
                responseData.Status = Convert.ToInt32(actionOnCardsObj["Status_Code"].ToString());
            }
            return responseData;
        }

        public async Task<ValidateNewCardsModel> Details(ValidateNewCardsModel validateNewCardsMdl)
        {
            ValidateNewCardsModel validateNewCardsModel = new ValidateNewCardsModel();
            string fromDate = "", toDate = "";
            if (!string.IsNullOrEmpty(validateNewCardsMdl.FromDate) && !string.IsNullOrEmpty(validateNewCardsMdl.FromDate))
            {
                fromDate = await _commonActionService.changeDateFormat(validateNewCardsMdl.FromDate);
                toDate = await _commonActionService.changeDateFormat(validateNewCardsMdl.ToDate);
            }
           
            validateNewCardsModel.OfficerTypeMdl.AddRange(await _commonActionService.GetOfficerTypeList());

            var validateNewCardsForms = new ValidateNewCardModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                StateId = validateNewCardsMdl.StateId=="0"?null:validateNewCardsMdl.StateId,
                FormNumber = string.IsNullOrEmpty(validateNewCardsMdl.FormNumber) ? "" : validateNewCardsMdl.FormNumber,
                CustomerName = string.IsNullOrEmpty(validateNewCardsMdl.CustomerName) ? "" : validateNewCardsMdl.CustomerName,
                FromDate = fromDate,
                ToDate = toDate,    
                CreatedBy = validateNewCardsMdl.CreatedBy== "0" ? null : validateNewCardsMdl.CreatedBy
            };

            StringContent validateNewCardsContent = new StringContent(JsonConvert.SerializeObject(validateNewCardsForms), Encoding.UTF8, "application/json");

            var validateNewCardsResponse = await _requestService.CommonRequestService(validateNewCardsContent, WebApiUrl.getValidateNewCardLists);

            JObject validateNewCardsObj = JObject.Parse(JsonConvert.DeserializeObject(validateNewCardsResponse).ToString());
            var validateNewCardsJarr = validateNewCardsObj["Data"].Value<JArray>();
            List<NewCardsList> validateNewCardslst = validateNewCardsJarr.ToObject<List<NewCardsList>>();
            validateNewCardsModel.NewCardsLists.AddRange(validateNewCardslst);
            return validateNewCardsModel;
        }

        public async Task<List<VehicleDetailsModel>> GetCardDetailsForApproval(string CustomerRefNo,string FormNumber)
        {
            var getDetailsForApprovalForms = new ValidateNewCardWithReferenceNumberModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FormNumber = FormNumber
            };

            StringContent getDetailsForApprovalContent = new StringContent(JsonConvert.SerializeObject(getDetailsForApprovalForms), Encoding.UTF8, "application/json");

            var getDetailsForApprovalResponse = await _requestService.CommonRequestService(getDetailsForApprovalContent, WebApiUrl.getCardDetailsForCardApproval);

            JObject getDetailsForApprovalObj = JObject.Parse(JsonConvert.DeserializeObject(getDetailsForApprovalResponse).ToString());
            var getDetailsForApprovalJarr = getDetailsForApprovalObj["Data"].Value<JArray>();
            List<VehicleDetailsModel> getDetailsForApprovallst = getDetailsForApprovalJarr.ToObject<List<VehicleDetailsModel>>();
            foreach (VehicleDetailsModel item in getDetailsForApprovallst)
            {
                item.FormNumber = FormNumber;
            }
            return getDetailsForApprovallst;
        }
    }
}
