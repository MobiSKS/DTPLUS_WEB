﻿using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPCL.Common.Models.RequestModel.Customer
{
    public class CheckPancardbyDistrictIdRequestModel : BaseEntity
    {
        public string IncomeTaxPan { get; set; }
        public string DistrictId { get; set; }
    }
}