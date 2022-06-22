using HPCL.Common.Helper;
using HPCL.Common.Models.RequestModel.VolvoEicher;
using HPCL.Common.Models.ResponseModel.CustomerManage;
using HPCL.Common.Models.ViewModel.VolvoEicher;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
    public class VolvoEicherService:IVolvoEicherService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        private readonly IConfiguration _configuration;

        public VolvoEicherService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
            _configuration = configuration;
        }
        public async Task<VEDealerOTCCardStatusViewModel> GetVEDealerCardStatus(string DealerCode)
        {
          
            var reqForm = new VEDealerOTCRequestModel
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip,
                DealerCode = string.IsNullOrEmpty(DealerCode) ? "" : DealerCode,
            };

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(reqForm), Encoding.UTF8, "application/json");

            var responseContent = await _requestService.CommonRequestService(stringContent, WebApiUrl.viewvolvoeicherdealerotccardstatus);

            JObject obj = JObject.Parse(JsonConvert.DeserializeObject(responseContent).ToString());
            VEDealerOTCCardStatusViewModel getVEDealerOTCCardStatusViewModel = obj.ToObject<VEDealerOTCCardStatusViewModel>();

            return getVEDealerOTCCardStatusViewModel;

        }
        public async Task<List<VECustomerProfileResponse>> BindCustomerDetails(string CustomerId, string NameOnCard)
        {
            using (HttpClient client = new HelperAPI().GetApiBaseUrlString())
            {

                var searchBody = new CustomerProfileModel
                    {
                        UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                        UserAgent = CommonBase.useragent,
                        UserIp = CommonBase.userip,
                        CustomerId = string.IsNullOrEmpty(CustomerId) ? "" : CustomerId,
                        NameOnCard = string.IsNullOrEmpty(NameOnCard) ? "" : NameOnCard
                    };
                


                StringContent content = new StringContent(JsonConvert.SerializeObject(searchBody), Encoding.UTF8, "application/json");

                var contentString = await _requestService.CommonRequestService(content, WebApiUrl.getvolvoeichercustomerdetail);

                JObject customerResponse = JObject.Parse(JsonConvert.DeserializeObject(contentString).ToString());

                var jarr = customerResponse["Data"].Value<JObject>();

                var customerResult = jarr["GetCustomerDetails"].Value<JArray>();
                List<VECustomerProfileResponse> customerProfileResponse = customerResult.ToObject<List<VECustomerProfileResponse>>();

                if (customerProfileResponse != null && customerProfileResponse.Count > 0)
                {
                    foreach (VECustomerProfileResponse response in customerProfileResponse)
                    {
                        if (string.IsNullOrEmpty(response.AreaOfOperation))
                        {
                            response.AreaOfOperation = "";
                        }
                        if (!string.IsNullOrEmpty(response.CommunicationPhoneNo))
                        {
                            string[] subs = response.CommunicationPhoneNo.Split("-");

                            if (subs.Count() > 1)
                            {
                                response.CommunicationDialCode = subs[0].ToString();
                                response.CommunicationPhoneNo = subs[1].ToString();
                            }
                            else
                            {
                                response.CommunicationDialCode = "";
                                response.CommunicationPhoneNo = "";
                            }
                        }

                        if (!string.IsNullOrEmpty(response.CommunicationFax))
                        {
                            string[] subs = response.CommunicationFax.Split("-");

                            if (subs.Count() > 1)
                            {
                                response.CommunicationFaxCode = subs[0].ToString();
                                response.CommunicationFax = subs[1].ToString();
                            }
                            else
                            {
                                response.CommunicationFaxCode = "";
                                response.CommunicationFax = "";
                            }
                        }

                        if (!string.IsNullOrEmpty(response.PermanentPhoneNo))
                        {
                            string[] subs = response.PermanentPhoneNo.Split("-");

                            if (subs.Count() > 1)
                            {
                                response.PerOrRegAddressDialCode = subs[0].ToString();
                                response.PermanentPhoneNo = subs[1].ToString();
                            }
                            else
                            {
                                response.PerOrRegAddressDialCode = "";
                                response.PermanentPhoneNo = "";
                            }
                        }

                        if (!string.IsNullOrEmpty(response.PermanentFax))
                        {
                            string[] subs = response.PermanentFax.Split("-");

                            if (subs.Count() > 1)
                            {
                                response.PermanentFaxCode = subs[0].ToString();
                                response.PermanentFax = subs[1].ToString();
                            }
                            else
                            {
                                response.PermanentFaxCode = "";
                                response.PermanentFax = "";
                            }
                        }

                        if (!string.IsNullOrEmpty(response.KeyOfficialFax))
                        {
                            string[] subs = response.KeyOfficialFax.Split("-");

                            if (subs.Count() > 1)
                            {
                                response.KeyOffFaxCode = subs[0].ToString();
                                response.KeyOffFax = subs[1].ToString();
                            }
                            else
                            {
                                response.KeyOffFaxCode = "";
                                response.KeyOffFax = "";
                            }
                        }

                        if (!string.IsNullOrEmpty(response.KeyOfficialPhoneNo))
                        {
                            string[] subs = response.KeyOfficialPhoneNo.Split("-");

                            if (subs.Count() > 1)
                            {
                                response.KeyOffDialCode = subs[0].ToString();
                                response.KeyOfficialPhoneNo = subs[1].ToString();
                            }
                            else
                            {
                                response.KeyOffDialCode = "";
                                response.KeyOfficialPhoneNo = "";
                            }
                        }

                        if (response.FleetSizeNoOfVechileOwnedHCV == "0")
                            response.FleetSizeNoOfVechileOwnedHCV = "";
                        response.FleetSizeNoOfVechileOwnedLCV = (string.IsNullOrEmpty(response.FleetSizeNoOfVechileOwnedLCV) ? "" : response.FleetSizeNoOfVechileOwnedLCV);
                        if (response.FleetSizeNoOfVechileOwnedLCV == "0")
                            response.FleetSizeNoOfVechileOwnedLCV = "";
                        response.FleetSizeNoOfVechileOwnedMUVSUV = (string.IsNullOrEmpty(response.FleetSizeNoOfVechileOwnedMUVSUV) ? "" : response.FleetSizeNoOfVechileOwnedMUVSUV);
                        if (response.FleetSizeNoOfVechileOwnedMUVSUV == "0")
                            response.FleetSizeNoOfVechileOwnedMUVSUV = "";
                        response.FleetSizeNoOfVechileOwnedCarJeep = (string.IsNullOrEmpty(response.FleetSizeNoOfVechileOwnedCarJeep) ? "" : response.FleetSizeNoOfVechileOwnedCarJeep);
                        if (response.FleetSizeNoOfVechileOwnedCarJeep == "0")
                            response.FleetSizeNoOfVechileOwnedCarJeep = "";

                        if (!string.IsNullOrEmpty(response.KeyOfficialDOA))
                        {
                            if (response.KeyOfficialDOA.Contains("1900"))
                            {
                                response.KeyOfficialDOA = "";
                            }
                        }

                        if (!string.IsNullOrEmpty(response.KeyOfficialDOB))
                        {
                            if (response.KeyOfficialDOB.Contains("1900"))
                            {
                                response.KeyOfficialDOB = "";
                            }
                        }
                        if (string.IsNullOrEmpty(response.NameOnCard))
                        {
                            response.NameOnCard = "";
                        }
                        if (!string.IsNullOrEmpty(response.DateOfApplication))
                        {
                            response.CustomerApplicationDate = response.DateOfApplication;
                        }
                        if (string.IsNullOrEmpty(response.RegionalOfficeName))
                        {
                            response.RegionalOfficeName = "";
                        }
                        if (response.FormNumber == "0")
                        {
                            response.FormNumber = "";
                        }
                    }
                }

                return customerProfileResponse;
            }
        }
    }
}
