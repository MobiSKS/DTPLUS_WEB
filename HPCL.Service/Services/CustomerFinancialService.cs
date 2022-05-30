using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.CustomerFinancial;
using HPCL.Common.Models.ViewModel.CustomerFinancial;
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
    public class CustomerFinancialService : ICustomerFinancialService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        public CustomerFinancialService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
        }

        public async Task<CardToCCMSBalanceTransferSearchResponse> SearchCardToCCMSTransfer(BalanceTransferSearchModel entity)
        {
            var reqBody = new BalanceTransferSearchModel();

            if (entity.CustomerID != null)
            {
                reqBody = new BalanceTransferSearchModel
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerID = entity.CustomerID,
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                reqBody = new BalanceTransferSearchModel
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerID = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCardToCCMSTransferUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            CardToCCMSBalanceTransferSearchResponse searchList = obj.ToObject<CardToCCMSBalanceTransferSearchResponse>();
            return searchList;
        }

        public async Task<CCMSToCardBalanceTransferSearchResponse> SearchCCMSToCardTransfer(BalanceTransferSearchModel entity)
        {
            var reqBody = new BalanceTransferSearchModel();

            if (entity.CustomerID != null)
            {
                reqBody = new BalanceTransferSearchModel
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerID = entity.CustomerID,
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                reqBody = new BalanceTransferSearchModel
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerID = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCCMSToCardTransferUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            CCMSToCardBalanceTransferSearchResponse searchList = obj.ToObject<CCMSToCardBalanceTransferSearchResponse>();
            return searchList;
        }

        public async Task<CardToCardBalanceTransferSearchResponse> SearchCardToCardTransfer(BalanceTransferSearchModel entity)
        {
            var reqBody = new BalanceTransferSearchModel();

            if (entity.CustomerID != null)
            {
                reqBody = new BalanceTransferSearchModel
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerID = entity.CustomerID,
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                reqBody = new BalanceTransferSearchModel
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerID = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    CardNo = entity.CardNo,
                    MobileNo = entity.MobileNo
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetCardToCardTransferUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            CardToCardBalanceTransferSearchResponse searchList = obj.ToObject<CardToCardBalanceTransferSearchResponse>();
            return searchList;
        }
        public async Task<CustomerTransactionResponseModel> GetCustomerTransactionDetails(string CustomerID,string CardNo,string MobileNo,string FromDate,string ToDate)
        {
            CustomerTransactionResponseModel transactionResponse = new CustomerTransactionResponseModel();
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(FromDate))
            {
                FromDate = await _commonActionService.changeDateFormat(FromDate);
                ToDate = await _commonActionService.changeDateFormat(ToDate);
            }
            else
            {
                FromDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            var Request = new CustomerTransactionViewModel()
            {
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                CustomerID = CustomerID,
                CardNo=CardNo,
                MobileNo=MobileNo,
                FromDate= FromDate,
                ToDate=ToDate
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");

            var response = await _requestService.CommonRequestService(content, WebApiUrl.gettransactionssummary);
            JObject jObj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            transactionResponse = JsonConvert.DeserializeObject<CustomerTransactionResponseModel>(response);
            var transactionjObj = jObj["Data"].Value<JObject>();
            var summaryListjArr = transactionjObj["GetTransactionsSaleSummary"].Value<JArray>();
            var detailListjArr = transactionjObj["GetTransactionsDetailSummary"].Value<JArray>();

            List<CustomerTransactionSummary> CustomerTransactionSummary =
                summaryListjArr.ToObject<List<CustomerTransactionSummary>>();

            List<CustomerTransactionDetails> CustomerTransactionDetails =
                detailListjArr.ToObject<List<CustomerTransactionDetails>>();
           
            transactionResponse.GetTransactionsSaleSummary.AddRange(CustomerTransactionSummary);
            transactionResponse.GetTransactionsDetailSummary.AddRange(CustomerTransactionDetails);
            return transactionResponse;
        }

        public async Task<GetViewAccountStatementResponse> ViewAccountStatement(GetViewAccountStatement entity)
        {
            var reqBody = new GetViewAccountStatement();
            if (!string.IsNullOrEmpty(entity.FromDate) && !string.IsNullOrEmpty(entity.FromDate))
            {
                entity.FromDate = await _commonActionService.changeDateFormat(entity.FromDate);
                entity.ToDate = await _commonActionService.changeDateFormat(entity.ToDate);
            }
            else
            {
                entity.FromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                entity.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (entity.CustomerID != null)
            {
                reqBody = new GetViewAccountStatement
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerID = entity.CustomerID,
                    FromDate = entity.FromDate,
                    ToDate = entity.ToDate
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                reqBody = new GetViewAccountStatement
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    CustomerID = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    FromDate = entity.FromDate,
                    ToDate = entity.ToDate
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.GetViewAccountStatementUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            GetViewAccountStatementResponse searchList = obj.ToObject<GetViewAccountStatementResponse>();
            return searchList;
        }

        public async Task<CCMSToCardAmtTransferResponse> CCMSToCardAmtTransfer(string customerId, string ccmsToCardTransfer)
        {
            ccmsToCardTransfer[] arrs = JsonConvert.DeserializeObject<ccmsToCardTransfer[]>(ccmsToCardTransfer);

            var reqBody = new CCMSToCardsAmountTransfer
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                customerId = customerId,
                ccmsToCardTransfer = arrs
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.CCMSToCardsAmtTransferUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            CCMSToCardAmtTransferResponse reasonList = obj.ToObject<CCMSToCardAmtTransferResponse>();
            return reasonList;
        }

        public async Task<CardToCardAmountTransferRes> CardToCardAmtTransfer(string customerId, string cardToCardTransfer)
        {
            cardToCardTransfer[] arrs = JsonConvert.DeserializeObject<cardToCardTransfer[]>(cardToCardTransfer);

            var reqBody = new CardToCardAmountTransfer
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                customerId = customerId,
                cardToCardTransfer = arrs
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.CardToCardsAmtTransferUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            CardToCardAmountTransferRes reasonList = obj.ToObject<CardToCardAmountTransferRes>();
            return reasonList;
        }

        public async Task<CardToCCMSAmtTransferResponse> CardToCCMSAmtTransfer(string customerId, string cardToCCMSTransfer)
        {
            cardToCCMSTransfer[] arrs = JsonConvert.DeserializeObject<cardToCCMSTransfer[]>(cardToCCMSTransfer);

            var reqBody = new CardToCCMSAmtTransfer
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                CustomerId = customerId,
                cardToCCMSTransfer = arrs
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(reqBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.CardToCCMSsAmtTransferUrl);
            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
            CardToCCMSAmtTransferResponse reasonList = obj.ToObject<CardToCCMSAmtTransferResponse>();
            return reasonList;
        }
    }
}
