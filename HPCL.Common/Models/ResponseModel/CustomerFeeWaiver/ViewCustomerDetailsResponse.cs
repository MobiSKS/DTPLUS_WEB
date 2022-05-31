using HPCL.Common.Models.ResponseModel.CommonResponse;
using HPCL.Common.Models.ResponseModel.Customer;
using System.Collections.Generic;

namespace HPCL.Common.Models.ResponseModel.CustomerFeeWaiver
{
    public class ViewCustomerDetailsResponse : ResponseMsg
    {
        public ViewCustomerDetailsResponseData data { get; set; }
    }

    public class ViewCustomerDetailsResponseData
    {
        public List<CustomerFullDetails> GetCustomerDetails { get; set; }
        public List<UploadDocResponseBody> CustomerKYCDetails { get; set; }
    }
}
