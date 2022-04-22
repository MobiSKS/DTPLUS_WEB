using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.RequestModel.ValidateNewCard;
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
        public async Task<string> ActionOnCards([FromBody] ApproveCardDetailsModel approveRejectModel)
        {
            var actionOnCardsForms = new ApproveCardDetailsModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerReferenceNo = approveRejectModel.CustomerReferenceNo,
                Comments = approveRejectModel.Comments,
                Approvalstatus = approveRejectModel.Approvalstatus,
                ApprovedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent actionOnCardsContent = new StringContent(JsonConvert.SerializeObject(actionOnCardsForms), Encoding.UTF8, "application/json");
            var actionOnCardsResponse = await _requestService.CommonRequestService(actionOnCardsContent, WebApiUrl.approveRejectCard);
            JObject actionOnCardsObj = JObject.Parse(JsonConvert.DeserializeObject(actionOnCardsResponse).ToString());

            if (actionOnCardsObj["Status_Code"].ToString() == "200")
            {
                var actionOnCardsJarr = actionOnCardsObj["Data"].Value<JArray>();
                List<SuccessResponse> actionOnCardsLst = actionOnCardsJarr.ToObject<List<SuccessResponse>>();
                return actionOnCardsLst.First().Reason.ToString();
            }
            else
            {
                return actionOnCardsObj["Message"].ToString();
            }
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

        public async Task<List<VehicleDetailsModel>> GetCardDetailsForApproval(string CustomerRefNo)
        {
            var getDetailsForApprovalForms = new ValidateNewCardWithReferenceNumberModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerReferenceNo = CustomerRefNo
            };

            StringContent getDetailsForApprovalContent = new StringContent(JsonConvert.SerializeObject(getDetailsForApprovalForms), Encoding.UTF8, "application/json");

            var getDetailsForApprovalResponse = await _requestService.CommonRequestService(getDetailsForApprovalContent, WebApiUrl.getCardDetailsForCardApproval);

            JObject getDetailsForApprovalObj = JObject.Parse(JsonConvert.DeserializeObject(getDetailsForApprovalResponse).ToString());
            var getDetailsForApprovalJarr = getDetailsForApprovalObj["Data"].Value<JArray>();
            List<VehicleDetailsModel> getDetailsForApprovallst = getDetailsForApprovalJarr.ToObject<List<VehicleDetailsModel>>();
            
            return getDetailsForApprovallst;
        }
    }
}
