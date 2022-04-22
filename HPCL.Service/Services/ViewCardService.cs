using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ResponseModel.ViewCard;
using HPCL.Common.Models.ViewModel.ViewCard;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Service.Services
{
    class ViewCardService : IViewCardService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;

        public ViewCardService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
        }

        public async Task<ViewCardSearch> ViewCardSearch(string CustomerId)
        {
            ViewCardSearch viewCardSearch = new ViewCardSearch(); 

            var searchBody = new ViewCardDetails();

            if (CustomerId != null)
            {
                searchBody = new ViewCardDetails
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    Customerid = CustomerId,
                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new ViewCardDetails
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    Customerid = _httpContextAccessor.HttpContext.Session.GetString("UserId")
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.ViewCardLimitsUrl);

            viewCardSearch = JsonConvert.DeserializeObject<ViewCardSearch>(response);
            return viewCardSearch;
        }


        public async Task<ViewCardSearch> SearchCardMapping(ViewCardDetails viewCardDetails)
        {
            var searchBody = new ViewCardDetails();
            ViewCardSearch viewCardSearch = new ViewCardSearch();
            if (viewCardDetails.Customerid != null)
            {
                searchBody = new ViewCardDetails
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    Customerid = viewCardDetails.Customerid,
                    Cardno= viewCardDetails.CardNo,
                    Vehiclenumber = viewCardDetails.VechileNo,
                    Mobileno= viewCardDetails.MobileNo

                };
            }
            else if (_httpContextAccessor.HttpContext.Session.GetString("LoginType") == "Customer")
            {
                searchBody = new ViewCardDetails
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    Customerid = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    Cardno = viewCardDetails.CardNo,
                    Vehiclenumber = viewCardDetails.VechileNo,
                    Mobileno = viewCardDetails.MobileNo
                };
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.SearchCardMappingUrl);

            
            viewCardSearch = JsonConvert.DeserializeObject<ViewCardSearch>(response);
            return viewCardSearch;
        }
        public async Task<List<string>> UpdateCards(ObjUpdateMobileandFastagNoInCard[] limitArray)
        {
            var updateServiceBody = new UpdateMobileRequest
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                ObjUpdateMobileandFastagNoInCard = limitArray,
                ModifiedBy = _httpContextAccessor.HttpContext.Session.GetString("UserId")
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(updateServiceBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.MobileAddOrEditUrl);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

            var updateRes = obj["Data"].Value<JArray>();
            List<string> messageList = new List<string>();
            List<SuccessResponse> updateResponse = updateRes.ToObject<List<SuccessResponse>>();
            if (updateResponse.Count > 0)
            {
                foreach(var item in updateResponse)
                    messageList.Add(item.Reason);
            }

            return messageList;
        }

        public async Task<ViewCardSearch> AddCardMappingDetails(ViewCardDetails viewCardDetails)    
        {
            var searchBody = new ViewCardDetails();
            ViewCardSearch viewCardSearch = new ViewCardSearch();
            if (viewCardDetails.Customerid != null)
            {
                searchBody = new ViewCardDetails
                {
                    UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                    UserAgent = CommonBase.useragent,
                    UserIp = CommonBase.userip,
                    Customerid = viewCardDetails.Customerid,
                    Cardno = viewCardDetails.CardNo,
                    Vehiclenumber = viewCardDetails.VechileNo,
                    Mobileno = viewCardDetails.MobileNo

                };
            }
           
            StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");
            var response = await _requestService.CommonRequestService(content, WebApiUrl.searchcardmappingdetailswithblankmobile);


            viewCardSearch = JsonConvert.DeserializeObject<ViewCardSearch>(response);
            return viewCardSearch;
        }

    }
}
