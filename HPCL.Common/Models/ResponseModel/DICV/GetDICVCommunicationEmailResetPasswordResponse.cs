﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ResponseModel.DICV
{
    public class GetDICVCommunicationEmailResetPasswordResponse
    {
        public int Status { get; set; }
        public string Reason { get; set; }
        public int Internel_Status_Code { get; set; }
        public string CommunicationEmailid { get; set; }
    }
}
