using HPCL.Common.Models.ResponseModel.CommonResponse;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.ManageRbe
{
    public class ChangeRbeMappingResponse : ResponseMsg
    {
        public List<ChangeRbeMappingResponseData> data { get; set; }
    }

    public class ChangeRbeMappingResponseData 
    {
        public string RBEID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Action { get; set; }
    }
}
