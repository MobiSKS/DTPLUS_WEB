using HPCL.Common.Models.ResponseModel.CommonResponse;
using HPCL.Common.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPCL.Common.Models.ResponseModel.Locations
{
    public class HeadOfficeDetailsResponse : ResponseMsg
    {
        public HeadOfficeDetailsResponse()
        {
            data = new List<HODResData>();
        }

       public virtual List<HODResData> data { get; set; }
    }

    public class HODResData
    {
        public int HQID { get; set; }
        [Required(ErrorMessage = "Please Enter Head Office Code")]
        [RegularExpression(FieldValidation.ValidHeadOfcCode, ErrorMessage = FieldValidation.ValidHeadOfcCodeErrMsg)]
        public string HQCode { get; set; }
        [Required(ErrorMessage = "Please Enter Head Office Name")]
        public string HQName { get; set; }
        [Required(ErrorMessage = "Please Enter Head Office Short Name")]
        public string HQShortName { get; set; }
    }
}
