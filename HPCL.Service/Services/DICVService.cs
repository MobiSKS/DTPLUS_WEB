using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.CommonEntity.ResponseEnities;
using HPCL.Common.Models.ViewModel.DICV;
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
    public class DICVService: IDICVService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestService _requestService;
        private readonly ICommonActionService _commonActionService;
        private readonly IConfiguration _configuration;

        public DICVService(IHttpContextAccessor httpContextAccessor, IRequestService requestServices, ICommonActionService commonActionService, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _requestService = requestServices;
            _commonActionService = commonActionService;
            _configuration = configuration;
        }
        public async Task<List<OfficerTypeResponseModal>> GetDICVOfficerTypeList()
        {
            var officerTypeForms = new BaseEntity
            {
                UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId"),
                UserAgent = CommonBase.useragent,
                UserIp = CommonBase.userip
            };

            StringContent officerTypeContent = new StringContent(JsonConvert.SerializeObject(officerTypeForms), Encoding.UTF8, "application/json");

            var officerTypeResponse = await _requestService.CommonRequestService(officerTypeContent, WebApiUrl.getDicvOfficerType);

            JObject officerTypeObj = JObject.Parse(JsonConvert.DeserializeObject(officerTypeResponse).ToString());
            var officerTypeJarr = officerTypeObj["Data"].Value<JArray>();
            List<OfficerTypeResponseModal> officerTypeLst = officerTypeJarr.ToObject<List<OfficerTypeResponseModal>>();

            return officerTypeLst;
        }
        public async Task<DICVDealerEnrollmentModel> DICVDealerEnrollment()
        {
            DICVDealerEnrollmentModel model = new DICVDealerEnrollmentModel();
            List<OfficerTypeResponseModal> officerTypeLst = new List<OfficerTypeResponseModal>();
            officerTypeLst = await GetDICVOfficerTypeList();
            officerTypeLst.Insert(0, new OfficerTypeResponseModal
            {
                OfficerTypeID = 0,
                OfficerTypeName = "--Select--"
            });

            model.OfficerTypes.AddRange(officerTypeLst);

            return model;
        }

    }
}