﻿using HPCL.Common.Models.ResponseModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel
{
    public class RegenerateIACModel
    {
        public RegenerateIACModel()
        {
            RegenerateIACResponseModel = new List<RegenerateIACResponseModel>();
         
        }
        public List<RegenerateIACResponseModel> RegenerateIACResponseModel { get; set; }
        
    }
}