using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.MyHpOTCCardCustomer
{
    public class VehicleRegistrationValidateRequestModel
    {
        public string registrationNumber { get; set; }
        public string consent { get; set; }
        public string version { get; set; }
    }
}