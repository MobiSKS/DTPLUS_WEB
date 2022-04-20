using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.ApplicationFormDataEntry;
using HPCL.Common.Models.ResponseModel.Customer;
using HPCL.Common.Models.ViewModel.ApplicationFormDataEntry;
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
    public class ApplicationFormDataEntryService : IApplicationFormDataEntryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;

        public ApplicationFormDataEntryService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }

        public async Task<GetCustomerNameResponse> GetCustomerName(string customerId)
        {
            var searchBody = new GetCustomerName
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = customerId
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCustomerNameUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            GetCustomerNameResponse searchList = obj.ToObject<GetCustomerNameResponse>();

            if (searchList != null && searchList.data.Count > 0)
            {
                if (searchList.data[0].RBEId == "null")
                    searchList.data[0].RBEId = "";
                else if (searchList.data[0].RBEId == null)
                    searchList.data[0].RBEId = "";
                else if (searchList.data[0].RBEId == "0")
                    searchList.data[0].RBEId = "";

                if (searchList.data[0].NoOfCards == "0")
                    searchList.data[0].NoOfCards = "";
                searchList.data[0].RBEName = string.IsNullOrEmpty(searchList.data[0].RBEName) ? "" : searchList.data[0].RBEName;
            }

            return searchList;
        }

        public async Task<List<SuccessResponse>> CheckAddOnForm(string formNumber)
        {
            var searchBody = new CheckAddOnForm
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                FormNumber = formNumber
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.CheckAddOnFormUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var updateRes = obj["Data"].Value<JArray>();
            List<SuccessResponse> updateResponse = updateRes.ToObject<List<SuccessResponse>>();
            return updateResponse;
        }
        public async Task<AddAddOnCard> GetAddOnCardsPartialView(string str)
        {
            JArray objs = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            List<ObjCardDetail> arrs = objs.ToObject<List<ObjCardDetail>>();

            AddAddOnCard addAddOnCard = new AddAddOnCard();
            addAddOnCard.Message = "";
            addAddOnCard.CardPreference = "";
            addAddOnCard.FeePaymentDate = "";
            addAddOnCard.FeePaymentNo = "";

            if (!string.IsNullOrEmpty(arrs[0].Message))
                addAddOnCard.Message = arrs[0].Message;
            if (!string.IsNullOrEmpty(arrs[0].CardPreference))
                addAddOnCard.CardPreference = arrs[0].CardPreference;
            if (!string.IsNullOrEmpty(arrs[0].FeePaymentDate))
                addAddOnCard.FeePaymentDate = arrs[0].FeePaymentDate;
            if (!string.IsNullOrEmpty(arrs[0].FeePaymentNo) && arrs[0].FeePaymentNo != "0")
                addAddOnCard.FeePaymentNo = arrs[0].FeePaymentNo;
            if (arrs[0].FeePaymentsCollectFeeWaiver > 0)
                addAddOnCard.FeePaymentsCollectFeeWaiver = arrs[0].FeePaymentsCollectFeeWaiver;
            addAddOnCard.CustomerTypeId = arrs[0].CustomerTypeId;
            addAddOnCard.VehicleVerifiedManually = arrs[0].VehicleVerifiedManually;
            addAddOnCard.TableStringyfiedData = str;
            if (arrs != null && arrs.Count > 0)
                addAddOnCard.ObjCardDetail = arrs;

            return addAddOnCard;
        }

        public async Task<AddAddOnCard> CustomerAddCardVehicleTbl(string str)
        {
            JArray objs = JArray.Parse(JsonConvert.DeserializeObject(str).ToString());
            List<ObjCardDetail> arrs = objs.ToObject<List<ObjCardDetail>>();
            AddAddOnCard addAddOnCard = new AddAddOnCard();

            if (!string.IsNullOrEmpty(arrs[0].Message))
                addAddOnCard.Message = arrs[0].Message;
            if (!string.IsNullOrEmpty(arrs[0].CardPreference))
                addAddOnCard.CardPreference = arrs[0].CardPreference;
            if (!string.IsNullOrEmpty(arrs[0].FeePaymentDate))
                addAddOnCard.FeePaymentDate = arrs[0].FeePaymentDate;
            if (!string.IsNullOrEmpty(arrs[0].FeePaymentNo) && arrs[0].FeePaymentNo != "0")
                addAddOnCard.FeePaymentNo = arrs[0].FeePaymentNo;
            if (arrs[0].FeePaymentsCollectFeeWaiver > 0)
                addAddOnCard.FeePaymentsCollectFeeWaiver = arrs[0].FeePaymentsCollectFeeWaiver;
            addAddOnCard.CustomerTypeId = arrs[0].CustomerTypeId;
            addAddOnCard.NoOfCards = arrs[0].NoOfCards;
            addAddOnCard.NoofVechileforAllCards = arrs[0].NoofVehicles;
            addAddOnCard.TableStringyfiedData = str;
            addAddOnCard.VehicleVerifiedManually = arrs[0].VehicleVerifiedManually;
            if (arrs != null && arrs.Count > 0 && ((!string.IsNullOrEmpty(arrs[0].CardIdentifier)) || (!string.IsNullOrEmpty(arrs[0].VechileNo))))
                addAddOnCard.ObjCardDetail = arrs;

            if (addAddOnCard.CustomerTypeId == "905" || addAddOnCard.CustomerTypeId == "909")
            {
                addAddOnCard.NoOfGridRows = arrs[0].NoofVehicles;
            }
            else
            {
                addAddOnCard.NoOfGridRows = arrs[0].NoOfCards;
            }

            return addAddOnCard;
        }

        public async Task<AddAddOnCard> AddAddOnCards(AddAddOnCard addAddOnCard)
        {

            string feePaymentDate = "";
            string oldPaymentDate = "";

            oldPaymentDate = addAddOnCard.FeePaymentDate;
            if (!string.IsNullOrEmpty(addAddOnCard.FeePaymentDate))
            {
                feePaymentDate = await _commonActionService.changeDateFormat(addAddOnCard.FeePaymentDate);
            }

            feePaymentDate = (string.IsNullOrEmpty(feePaymentDate) ? "1900-01-01" : feePaymentDate);

            addAddOnCard.FeePaymentDate = feePaymentDate;

            foreach (HPCL.Common.Models.ViewModel.ApplicationFormDataEntry.ObjCardDetail cardDetails in addAddOnCard.ObjCardDetail)
            {
                if (!string.IsNullOrEmpty(cardDetails.VechileNo))
                {
                    cardDetails.VechileNo = cardDetails.VechileNo.ToUpper();
                }
            }

            addAddOnCard.UserAgent = CommonBase.useragent;
            addAddOnCard.UserIp = CommonBase.userip;
            addAddOnCard.UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            addAddOnCard.CreatedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            addAddOnCard.VehicleNoVerifiedManually = (addAddOnCard.VehicleVerifiedManually ? "1" : "0");

            if (addAddOnCard.CardPreference.ToUpper() == "CARDLESS")
            {
                addAddOnCard.FeePaymentsCollectFeeWaiver = 0;
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(addAddOnCard), Encoding.UTF8, "application/json");

            CustomerInserCardResponse customerInserCardResponse;

            var responseCustomer = await _requestService.CommonRequestService(content, WebApiUrl.addAddonCard);

            customerInserCardResponse = JsonConvert.DeserializeObject<CustomerInserCardResponse>(responseCustomer);

            if (customerInserCardResponse.Internel_Status_Code == 1000)
            {
                addAddOnCard.Status = customerInserCardResponse.Data[0].Status;
                addAddOnCard.StatusCode = customerInserCardResponse.Internel_Status_Code;
                addAddOnCard.Message = customerInserCardResponse.Message;
                addAddOnCard.Reason = customerInserCardResponse.Data[0].Reason;
            }
            else
            {
                addAddOnCard.Message = customerInserCardResponse.Message;
                addAddOnCard.StatusCode = customerInserCardResponse.Internel_Status_Code;
                addAddOnCard.FeePaymentDate = oldPaymentDate;
            }

            return addAddOnCard;
        }

    }
}
