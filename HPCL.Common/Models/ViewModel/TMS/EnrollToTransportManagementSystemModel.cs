﻿using HPCL.Common.Models.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.TMS
{
    public class EnrollToTransportManagementSystemModel : BaseEntity
    {
        public string CustomerId { get; set; }
        public string Message { get; set; }
    }
}